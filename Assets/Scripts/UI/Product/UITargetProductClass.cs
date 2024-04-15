// ---------------------------------------------------------  
// UITargetProductManager.cs  
//   目的の製品を画面に表示
// 作成日:  4/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 目的の製品を画面に表示
/// </summary>
public class UITargetProductClass : MonoBehaviour
{

    #region 変数  

    [Header("オブジェクト")]
    [SerializeField, Tooltip("求めている製品のオブジェクトとなる")]
    private GameObject _targetProductObj = default;
    [Header("スプライト")]
    [SerializeField, Tooltip("卵のイメージ")]
    private Sprite _eggImage = default;
    [SerializeField, Tooltip("牛乳のイメージ")]
    private Sprite _milkImage = default;
    [SerializeField, Tooltip("羊の毛のイメージ")]
    private Sprite _woolImage = default;
    [Header("データ")]
    [SerializeField, Tooltip("表示の間隔")]
    private float _displaySpace = 50;
    [SerializeField, Tooltip("表示の開始位置（一番左）")]
    private Vector2 _displayPos = new Vector2(0, 0);
    /// <summary>
    /// 求める製品のインタフェースのリスト
    /// </summary>
    private List<ITargetProduct> _iTargetProductList = new List<ITargetProduct> { };
    /// <summary>
    /// 生成したオブジェクトのリスト
    /// </summary>
    private List<GameObject> _displayImageList = new List<GameObject> { };

    #endregion
    #region メソッド  

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

      

    }

    /// <summary>
    /// 情報の受け取り
    /// </summary>
    /// <param name="iTargetProductList">求める製品のインタフェース</param>

    public void ViewTargetProduct(IObservable<CollectionReplaceEvent<ITargetProduct>> iTargetProductList)
    {

        Debug.LogError("変更");
        //新しいインターフェースリスト取得
        this._iTargetProductList = (List<ITargetProduct>)iTargetProductList;
        //インターフェースを見ていくループ
        for (int i = 0; _iTargetProductList.Count - 1 < i; i++)
        {

            //表示するスプライト
            Sprite displaySprite = default;
            //自分の求めている製品で処理分岐
            switch (_iTargetProductList[i].ProductState)
            {

                case ProductState.Egg:

                    //卵のイメージにする
                    displaySprite = _eggImage;

                    break;
                case ProductState.Milk:

                    //牛乳のイメージにする
                    displaySprite = _milkImage;

                    break;
                case ProductState.Wool:

                    //毛のイメージにする
                    displaySprite = _woolImage;

                    break;

            }

            //表示の位置取得
            //_displaySpace - 1は0から始めるため
            Vector3 displayPos = Vector3.right * ( _displayPos.x + (_displaySpace - 1) * i) +
                                 Vector3.up * _displayPos.y;

            //前のイメージのリストの中身があった時
            if (_displayImageList.Count -1 > i)
            {

                Debug.LogError("呼び出す側");
                //初期化
                _displayImageList[i].GetComponent<ITargetProduct>().Initialization();
                //アクティブ化
                _displayImageList[i].SetActive(true);
                //スプライトを変える
                _displayImageList[i].GetComponent<Image>().sprite = displaySprite;
                //位置を変える
                _displayImageList[i].transform.position = displayPos;

            } 
            else
            {

                //スプライトを変える
                _targetProductObj.GetComponent<Image>().sprite = displaySprite;
                //生成
                Instantiate(_targetProductObj, displayPos, Quaternion.identity);

            }

        }

        //表示しなくてもいい量を取得
        int differenceIndex = (_displayImageList.Count - 1) - (_iTargetProductList.Count - 1);
        //_displayImageListの表示したい箇所までのインデックス数
        int displayIndex = _displayImageList.Count - 1 - differenceIndex;
        //アクティブ化しなくていい部分を見ていくループ
        for (int i = _displayImageList.Count - 1; differenceIndex > i; --i)
        {

            //表示しなくていい部分を非アクティブ化
            _displayImageList[i].SetActive(false);
        
        }


    }
    #endregion

}
