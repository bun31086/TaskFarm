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
    private GameObject _uITargetProductImageObj = default;
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
    /// 現在の求める製品のインターフェースリスト
    /// </summary>
    private List<ITargetProduct> _iTargetProductList = new List<ITargetProduct> { };
    /// <summary>
    /// 使用中のイメージオブジェクトのリスト
    /// </summary>
    private List<Image> _displayImageList = new List<Image> { };
    /// <summary>
    /// 未使用のイメージオブジェクトのリスト
    /// </summary>
    private List<Image> _notDisplayImageList = new List<Image> { };
    /// <summary>
    /// 現在使用中のイメージオブジェクトのスライダーのリスト
    /// </summary>
    private List<Slider> _nowSliderList = new List<Slider> { };

    #endregion
    #region メソッド  

    private void Update()
    {

        //スライダーリストを見ていくループ
        for (int i = 0; _nowSliderList.Count - 1 >= i; i++)
        {

            //残り時間を取得
            float remainingTimeTime = _iTargetProductList[i].SubmissionTimeLimit;
            //残り時間をスライダーに反映
            _nowSliderList[i].value = remainingTimeTime;
        
        }

    }

    /// <summary>
    /// リスト内の要素追加時にオブジェクトのリスト更新
    /// </summary>
    /// <param name="addList">追加された後のリスト</param>
    public void ListAdd(List<ITargetProduct> addList)
    {

        //インターフェースのリストの更新
        _iTargetProductList = addList;
        //インターフェースのリストを見ていくループ
        for (int i = 0; addList.Count - 1 >= i; i++)
        {

            //表示するスプライト
            Sprite displaySprite = default;
            //自分の求めている製品で処理分岐
            switch (addList[i].ProductState)
            {

                case ProductState.Egg:

                    //卵のスプライトにする
                    displaySprite = _eggSprite;

                    break;
                case ProductState.Milk:

                    //牛乳のスプライトにする
                    displaySprite = _milkSprite;

                    break;
                case ProductState.Wool:

                    //毛のスプライトにする
                    displaySprite = _woolSprite;

                    break;

            }
            //表示の位置取得
            Vector3 displayPos = Vector3.right * (_displayPos.x + _displaySpace * i) +
                                 Vector3.up * _displayPos.y;
            //指定したいインデックスがあるとき
            if (_displayImageList.Count - 1 >= i)
            {

                //イメージにスプライトを設定
                _displayImageList[i].sprite = displaySprite;
                //イメージの位置を変更
                _displayImageList[i].rectTransform.position = displayPos;

            }
            //指定したいインデックスがない
            else
            {

                //イメージオブジェクト非表示リストの余りがないとき
                if (_notDisplayImageList.Count <= 0)
                {

                    //ゲームオブジェクトを生成しイメージコンポーネントを取得
                    GameObject product = Instantiate(_uITargetProductImageObj);
                    //イメージコンポーネント取得
                    Image productImage = product.GetComponent<Image>();
                    //親オブジェクトを設定
                    productImage.transform.SetParent(_parentPanel.transform);
                    //イメージにスプライトを設定
                    productImage.sprite = displaySprite;
                    //イメージの位置を変更
                    productImage.rectTransform.position = displayPos;
                    //表示リストに追加
                    _displayImageList.Add(productImage);
                    continue;

                }
                //非表示リストの最初のインデックスを取得
                Image outImage = _notDisplayImageList[0];
                //取り出した要素を削除
                _notDisplayImageList.RemoveAt(0);
                //イメージにスプライトを設定
                outImage.sprite = displaySprite;
                //イメージの位置を変更
                outImage.rectTransform.position = displayPos;
                //取り出したイメージを表示リストに追加
                _displayImageList.Add(outImage);
                //アクティブ化
                outImage.gameObject.SetActive(true);

            }

        }
        //スライダーリスト更新
        INSliderList(addList);

    }

    /// <summary>
    /// リスト内の要素削除時にオブジェクトのリスト更新
    /// </summary>
    /// <param name="reMoveList">削除された後のリスト</param>
    public void ListReMove(List<ITargetProduct> reMoveList)
    {

        //インターフェースのリストの更新
        _iTargetProductList = reMoveList;
        /*
        * イメージ表示リストの未使用部分を非表示リストに入れる
        */
        //表示しているリストからインタフェースが入ったリストのインデックスを引き差を取得
        int offset = (_displayImageList.Count - 1) - (reMoveList.Count - 1);
        //使用部分までのループ
        for (int i = 0; offset > i; ++i)
        {

            //表示リストからイメージを取り出す
            Image outImage = _displayImageList[0];
            //非表示にした要素を削除
            _displayImageList.RemoveAt(0);
            //非表示リストに取り出したイメージを追加
            _notDisplayImageList.Add(outImage);
            //取り出したイメージを非表示
            outImage.gameObject.SetActive(false);

        }
        //インターフェースを見ていくループ
        for (int i = 0; reMoveList.Count - 1 >= i; i++)
        {

            //表示の位置取得
            Vector3 displayPos = Vector3.right * (_displayPos.x + _displaySpace * i) +
                                 Vector3.up * _displayPos.y;
            //位置を変更
            _displayImageList[i].rectTransform.position = displayPos;

        }
            //スライダーリスト更新
            INSliderList(reMoveList);

    }

    /// <summary>
    /// スライダーリストの更新処理
    /// </summary>
    ///  /// <param name="interfaceList">インターフェースを格納したリスト</param>
    private void INSliderList(List<ITargetProduct> interfaceList)
    {
        //初期化
        _nowSliderList.Clear();
        //使用中のイメージリストを見ていくループ
        for(int i = 0; _displayImageList.Count -1 >= i; i++)
        {

            //スライダーコンポーネントを取得
            Slider slider = _displayImageList[i].gameObject.GetComponent<Slider>();
            //sliderに最大値を設定
            slider.maxValue = interfaceList[i].MaxSubmissionTimeLimit;
            //リストに追加
            _nowSliderList.Add(slider);
        
        }
    
    }
    #endregion

}
