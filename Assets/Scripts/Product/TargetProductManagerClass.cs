// ---------------------------------------------------------  
// TargetProductManager.cs  
//   求める製品の生成と管理
// 作成日:  4/4
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System.Collections;
using System;
using UniRx;
using UnityEngine;

/// <summary>
///  求める製品の管理
/// </summary>
public class TargetProductManagerClass : MonoBehaviour
{

    #region プロパティ  

    public IObservable<CollectionReplaceEvent<ITargetProduct>> TargetProductList => _targetProductsList.ObserveReplace();
    public IReadOnlyReactiveProperty<int> ChainBonus => _chainBonus;
    public IReadOnlyReactiveProperty<int> ChaiCount => _chainCount;

    #endregion
    #region 変数  

    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("ターゲットプロダクトのデータ")]
    private TargetProductManagerData _targetProductManagerData = default;
    [Header("オブジェクト")]
    [SerializeField,Tooltip("目的の製品オブジェクト")]
    private GameObject _targetProductPrefab = default;
    [SerializeField, Tooltip("目的の製品の親オブジェクトになるパネル")]
    private GameObject _parentPanel = default;
    [Header("スクリプト")]
    [SerializeField, Tooltip("お金を増やすために取得")]
    private GameManagerClass _gameManagerClass = default;

    /// <summary>
    /// 求める製品が入るリスト
    /// </summary>
    private ReactiveCollection<ITargetProduct> _targetProductsList = new ReactiveCollection<ITargetProduct>();
    /// <summary>
    /// 連鎖ボーナス
    /// </summary>
    private ReactiveProperty<int> _chainBonus = new ReactiveProperty<int>(default);
    /// <summary>
    /// 連鎖数を数える
    /// </summary>
    private ReactiveProperty<int> _chainCount = new ReactiveProperty<int>(default);

    #endregion
    #region メソッド  

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {

        //求める製品を追加
        AddTargetProduct();
        //一定時間後に求める製品の追加
        StartCoroutine(CallAddTargetProduct());

    }

    /// <summary>
    /// 提出された製品を_targetProductsListの先頭に
    /// 格納されているインタフェースのメソッドに渡し
    /// 実行結果により処理分岐させる
    /// </summary>
    /// <param name="collisionObj">提出されたオブジェクト</param>
    public void SubmissionProcess(GameObject collisionObj)
    {

        //リストの先頭に格納されているインタフェースに対し製品比較を実行し実行結果が返ってくる
        bool result = _targetProductsList[0].MatchCheck(collisionObj);
        //合っていた時
        if (result)
        {

            //価格が入る
            int price = default;
            //提出されたオブジェクトにより価格分岐
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
            //コンボ数を増やす
            _chainCount.Value++;
            ///今の金額段階を調べる
            int bonusStep = _chainCount.Value / _targetProductManagerData.UpBonusLine;
            //ゲームマネージャーに金額を渡す
            _gameManagerClass.AddMoney(price + bonusStep);

        }
        //合わなかったとき
        else
        {

            //初期化
            _chainCount.Value = 0;
            _chainBonus.Value = 0;

        }
        //比べた求めている製品を削除
        _targetProductsList.RemoveAt(0);
        //求めている製品が１以下になった時
        if (_targetProductsList.Count <= 1)
        {

            //求めている製品の追加
            AddTargetProduct();

        }

    }

    /// <summary>
    /// 求める製品をリストに追加
    /// </summary>
    private void AddTargetProduct()
    {

        //enum型の要素数を取得
        int maxCount = ProductState.GetNames(typeof(ProductState)).Length;
        //要素数内のランダムな値を取得
        int number = UnityEngine.Random.Range(0, maxCount);
        //値に対応したステートを取得
        ProductState chooseProduct = (ProductState)number;
        //求める製品を生成
        GameObject targetProductObj = Instantiate(_targetProductPrefab,_parentPanel.transform);
        //インタフェース取得
        ITargetProduct iTargetProduct = targetProductObj.GetComponent<ITargetProduct>();
        //製品情報を渡す
        iTargetProduct.SetProductInformation(new TargetProductManagerClass(),_targetProductManagerData.SubmissionTimeLimit,chooseProduct);
        //取得したインタフェースをリストに格納
        _targetProductsList.Add(iTargetProduct);

    }

    /// <summary>
    /// 求める製品リストの先頭削除
    /// 生成したプロダクトクラスに呼び出しを委譲
    /// </summary>
    public void DeleteTargetProduct()
    {
        
        //インデックス0番を消す
        _targetProductsList.RemoveAt(0);

    }

    // <summary>
    // 一定時間後にAddTargetProductメソッドを呼び出す
    // </summary>
    // <returns></returns>
    private IEnumerator CallAddTargetProduct()
    {

        //設定時間後まで待つ
        yield return new WaitForSeconds(_targetProductManagerData.ProductAddTime);
        //求める製品の選択
        AddTargetProduct();
        //再起呼び出し
        StartCoroutine(CallAddTargetProduct());

    }

    #endregion
}
