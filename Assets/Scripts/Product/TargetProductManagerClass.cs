// ---------------------------------------------------------  
// TargetProductManager.cs  
//   求める製品の生成と管理
// 作成日:  4/4
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;


public class TargetProductManagerClass : MonoBehaviour
{

    #region プロパティ  

    public IReadOnlyReactiveProperty<List<ProductState>> TargetProductList => _targetProductsList;
    public IReadOnlyReactiveProperty<int> ChainBonus => _chainBonus;

    #endregion
    #region 変数  
    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("ターゲットプロダクトのデータ")]
    private TargetProductManagerData _targetProductManagerData = default;
    [Header("スクリプト")]
    [SerializeField, Tooltip("提出された製品を知るために取得")]
    private Iteble _tableInterFace = default;
    [SerializeField, Tooltip("お金を増やすために取得")]
    private GameManagerClass _gameManagerClass = default;

    /// <summary>
    /// 求める製品が入るリスト
    /// </summary>
    private ReactiveProperty<List<ProductState>> _targetProductsList = new ReactiveProperty<List<ProductState>>(new List<ProductState> { });
    /// <summary>
    /// 連鎖ボーナス
    /// </summary>
    private ReactiveProperty<int> _chainBonus = new ReactiveProperty<int>(default);
    /// <summary>
    /// 残り時間が０になった時に消してもらうために取得
    /// </summary>
    private ProductsClass _productsClass  = default;
    /// <summary>
    /// 累計連鎖ボーナス
    /// </summary>
    private int _sumChainBonus = default;
    /// <summary>
    /// 連鎖数を数える
    /// </summary>
    private int _chainCount = default;

    #endregion
    #region メソッド  

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {

        //求める製品を追加
        //AddTargetProduct();
        //非同期で提出されているオブジェクトが変わった時に実行
        _tableInterFace.CollisionObj
            .Subscribe
            (

                collisionObj =>
                {

                            //中身がないとき
                            if (collisionObj == null)
                    {

                        return;

                    }
                    MatchCheck(collisionObj);

                }

            ).AddTo(this);

    }

    /// <summary>
    /// 農産物を置いたときに提出してほしい
    /// 農産物とあっていれば
    /// 所持金を増やすメソッドを呼びリストから削除
    /// </summary>
    /// <param name="collisionObj">提出されたオブジェクト</param>
    private void MatchCheck(GameObject collisionObj)
    {

        //現在求めている製品をリストから取得
        ProductState targetProduct = _targetProductsList.Value[0];
        //求めている製品と提出されたオブジェクトがあった時
        if (targetProduct.ToString() == collisionObj.name)
        {

            //価格が入る
            int price = default;
            //提出されたオブジェクトにより処理分岐
            switch (collisionObj.name)
            {

                case "Egg":

                    //卵の価格にする
                    price = _targetProductManagerData.EggPrice;

                    break;
                case "Milk":

                    //牛乳の価格にする
                    price = _targetProductManagerData.MilkPrice;

                    break;
                case "Wool":

                    //ウールの価格にする
                    price = _targetProductManagerData.WoolPrice;

                    break;

            }
            //比べた求めている製品を削除
            _targetProductsList.Value.Remove(targetProduct);
            //求めている製品が１以下になった時
            if (_targetProductsList.Value.Count <= 1)
            {

                //求めている製品の追加
                //AddTargetProduct();

            }
            ///今の金額段階を調べる
            int bonusStep = _chainCount / _targetProductManagerData.UpBonusLine;
            //今の段階分を足す
            _chainBonus.Value += bonusStep;

        }
        //合わなかったとき
        else
        {

            //累計に足す
            _sumChainBonus = _chainBonus.Value;
            //初期化
            _chainCount = 0;
            _chainBonus.Value = 0;
            return;

        }


    }

    /// <summary>
    /// 求める製品をリストに追加
    /// </summary>
    private void AddTargetProduct(ProductState chooseProduct)
    {

        //取得したステートをリストに格納
        _targetProductsList.Value.Add(chooseProduct);

        //_productsClass = ProductsClass();

    }

    /// <summary>
    /// 求める製品リストの先頭削除
    /// 生成したプロダクトクラスに呼び出しを委譲
    /// </summary>
    public void DeleteTargetProduct()
    {

        //インデックス0番を消す
        _targetProductsList.Value.RemoveAt(0);

    }



    #endregion
}
