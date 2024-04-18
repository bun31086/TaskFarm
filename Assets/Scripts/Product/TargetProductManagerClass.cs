// ---------------------------------------------------------  
// TargetProductManager.cs  
//   求める製品の生成と管理
// 作成日:  4/4
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

/// <summary>
///  求める製品の管理
/// </summary>
public class TargetProductManagerClass : MonoBehaviour
{

    #region プロパティ  

    /// <summary>
    /// 値の変更感知に使用
    /// </summary>
    public IReadOnlyReactiveCollection<ITargetProduct> TargetProductCollection => _targetProductsCollection;
    /// <summary>
    /// 値の変化時に取得される値
    /// </summary>
    public List<ITargetProduct> TargetProductsList => _targetProductsCollection.ToList();
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
    /// 求める製品のインターフェース入るリスト
    /// </summary>
    private ReactiveCollection<ITargetProduct> _targetProductsCollection = new ReactiveCollection<ITargetProduct> { };
    /// <summary>
    /// 実行中の求める製品が入るリスト
    /// </summary>
    private List<GameObject> _useProductObj = new List<GameObject> { };
    /// <summary>
    /// 停止中の求める製品が入るリスト
    /// </summary>
    private List<GameObject> _notUseProductObj = new List<GameObject> { };
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

        Debug.LogError("呼ばれない");
        //リストの先頭に格納されているインタフェースに対し製品比較を実行し実行結果が返ってくる
        bool result = _targetProductsCollection[0].MatchCheck(collisionObj);
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
        _targetProductsCollection.RemoveAt(0);
        //求めている製品が１以下になった時
        if (_targetProductsCollection.Count <= 1)
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

        //１ループ前に出したランダムな値
        int oldNumber = default;
        //生成量分ループ
        for (int i = 0; _targetProductManagerData.AddProductValue > i; i++)
        {
           
            //enum型の要素数を取得
            int maxCount = ProductState.GetNames(typeof(ProductState)).Length;
            //要素数内のランダムな値を取得
            int number = UnityEngine.Random.Range(0, maxCount);
            //前に出した数と同じ場合
            if (number == oldNumber)
            {

                //もう一度要素数内のランダムな値を取得
                number = UnityEngine.Random.Range(0, maxCount);

            }
            //過去の値を更新
            oldNumber = number;
            //値に対応したステートを取得
            ProductState chooseProductState = (ProductState)number;
            //生成または取り出した求める製品のオブジェクトを入れる変数
            GameObject targetProductObj = default;
            //非実行中のリストの中身があるとき
            if (_notUseProductObj.Count  > 0)
            {

                //非実行中の求める製品のリストから取り出す
                GameObject outObj = _notUseProductObj[0];
                //取り出したオブジェクトをアクティブ化
                outObj.SetActive(true);
                //取り出した要素を削除
                _notUseProductObj.RemoveAt(0);
                //取り出したオブジェクトを変数に格納
                targetProductObj = outObj;

            } 
            //非実行中のリストの中身がないとき
            else
            {

                //求める製品オブジェクトを生成し変数に格納
                targetProductObj = Instantiate(_targetProductPrefab, _parentPanel.transform);

            }
            //インタフェース取得
            ITargetProduct iTargetProduct = targetProductObj.GetComponent<ITargetProduct>();
            //製品情報を渡す
            iTargetProduct.SetProductInformation(this, _targetProductManagerData.SubmissionTimeLimit, chooseProductState);
            //取得したインタフェースをコレクションに格納
            _targetProductsCollection.Add(iTargetProduct);
            //生成したまたは取り出した求める製品オブジェクトを実行中のリストに格納
            _useProductObj.Add(targetProductObj);

        }

    }

    /// <summary>
    /// 求める製品リストの先頭削除
    /// 生成したプロダクトクラスに呼び出しを委譲
    /// </summary>
    /// <param name="itargetProduct">メソッドを呼び出した求める製品のインターフェース</param>
    /// <param name="callGameObject">メソッドを呼び出した求める製品のオブジェクト</param>
    public void DeleteTargetProduct(ITargetProduct itargetProduct , GameObject callGameObject)
    {

        //インデックス0番を消す
        _targetProductsCollection.Remove(itargetProduct);
        //求める製品オブジェクトを実行中のリストから削除
        _useProductObj.Remove(callGameObject);
        //求める製品オブジェクトを非実行中のリストに格納
        _notUseProductObj.Add(callGameObject);
        //求める製品オブジェクトを非アクティブ化
        callGameObject.SetActive(false);
        //求めている製品が１以下になった時
        if (_targetProductsCollection.Count <= 0)
        {

            //求めている製品の追加
            AddTargetProduct();

        }

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
