// ---------------------------------------------------------  
// UITargetProductManager.cs  
//   目的の製品を画面に表示
// 作成日:  4/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 目的の製品を画面に表示
/// </summary>
public class UITargetProductClass : MonoBehaviour
{

    #region 変数  

    [Header("オブジェクト")]
    [SerializeField, Tooltip("求めている製品のUIオブジェクトとなる")]
    private GameObject _uITargetProductImage = default;
    [SerializeField, Tooltip("求めているの製品のUI親オブジェクトになるパネル")]
    private GameObject _parentPanel = default;
    [Header("スプライト")]
    [SerializeField, Tooltip("卵のスプライト")]
    private Sprite _eggSprite = default;
    [SerializeField, Tooltip("牛乳のスプライト")]
    private Sprite _milkSprite = default;
    [SerializeField, Tooltip("羊の毛のスプライト")]
    private Sprite _woolSprite = default;
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
    /// 使用中のオブジェクトのリスト
    /// </summary>
    private List<Image> _displayImageList = new List<Image> { };
    /// <summary>
    /// 未使用のオブジェクトのリスト
    /// </summary>
    private List<Image> _notDisplayObjList = new List<Image> { };

    #endregion
    #region メソッド  

    /// <summary>
    /// 情報の受け取り
    /// </summary>
    /// <param name="iTargetProductList">求める製品のインタフェースが入ったリスト</param>
    private void ViewTargetProduct(List<ITargetProduct> iTargetProductList)
    {

        //新しいインターフェースリスト取得
        this._iTargetProductList = iTargetProductList;
        //インターフェースを見ていくループ
        for (int i = 0; _iTargetProductList.Count - 1 >= i; i++)
        {

            //表示するスプライト
            Sprite displaySprite = default;
            //自分の求めている製品で処理分岐
            switch (_iTargetProductList[i].ProductState)
            {

                case ProductState.Egg:

                    //卵のイメージにする
                    displaySprite = _eggSprite;

                    break;
                case ProductState.Milk:

                    //牛乳のイメージにする
                    displaySprite = _milkSprite;

                    break;
                case ProductState.Wool:

                    //毛のイメージにする
                    displaySprite = _woolSprite;

                    break;

            }
            //表示の位置取得
            //_displaySpace - 1は0から始めるため
            Vector3 displayPos = Vector3.right * (_displayPos.x + _displaySpace * i) +
                                 Vector3.up * _displayPos.y;
            //イメージ表示リストの要素数で足りるとき
            if (_displayImageList.Count - 1 >= i)
            {

                //イメージにスプライトを設定
                _displayImageList[i].sprite = displaySprite;
                //イメージの位置を変更
                _displayImageList[i].rectTransform.position = displayPos;

            }
            //イメージ表示リストの要素数で足らないとき
            else
            {

                //イメージ非表示リストの余りがないとき
                if (_notDisplayObjList.Count - 1 <= 0)
                {

                    //ゲームオブジェクトを生成しイメージコンポーネントを取得
                    Image createImage = Instantiate(_uITargetProductImage.GetComponent<Image>());
                    //親オブジェクトを設定
                    createImage.transform.SetParent(_parentPanel.transform);
                    //イメージにスプライトを設定
                    createImage.sprite = displaySprite;
                    //イメージの位置を変更
                    createImage.rectTransform.position = displayPos;
                    //表示リストに追加
                    _displayImageList.Add(createImage);
                    continue;

                }
                //非表示リストの最初のインデックスを取得
                Image outImage = _notDisplayObjList[0];
                //取り出した要素を削除
                _notDisplayObjList.RemoveAt(0);
                //イメージにスプライトを設定
                outImage.sprite = displaySprite;
                //イメージの位置を変更
                outImage.rectTransform.position = displayPos;
                //取り出したイメージを表示リストに追加
                _notDisplayObjList.Add(outImage);

            }

        }

        /*
         * イメージ表示リストの使っていない部分を非表示リストに入れる
         */
        //要素数ではなくインデックス数を取得
        int listCount = _displayImageList.Count - 1;
        //表示しているリストから求める製品のインタフェースが入ったリストを引き差を取得
        int offset = listCount - (_iTargetProductList.Count - 1);
        //表示しているリストから先ほど求めた差を引き必要な個所までのインデックスを求める
        int needIndex = listCount - offset;
        //必要な個所までのループ
        for (int i = listCount; needIndex > listCount; --listCount)
        {

            //表示リストからイメージを取り出す
            Image outImage = _displayImageList[listCount];
            //非表示にした要素を削除
            _displayImageList.RemoveAt(listCount);
            //非表示リストに取り出したイメージを追加
            _notDisplayObjList.Add(outImage);
            //非表示
            _displayImageList[listCount].gameObject.SetActive(false);

        }

    }

    /// <summary>
    /// リスト内の要素追加時にViewTargetProductメソッドを呼ぶ
    /// </summary>
    /// <param name="addList">追加された後のリスト</param>
    public void ListAdd(List<ITargetProduct> addList)
    {

        //リスト情報を渡す
        ViewTargetProduct(addList);

    }

    /// <summary>
    /// リスト内の要素削除時にViewTargetProductメソッドを呼ぶ
    /// </summary>
    /// <param name="reMoveList">削除された後のリスト</param>
    public void ListReMove(List<ITargetProduct> reMoveList)
    {

        //リスト情報を渡す
        ViewTargetProduct(reMoveList);

    }
    #endregion

}
