// ---------------------------------------------------------  
// Products.cs  
//   求めている製品の情報が入る
// 作成日:  4/13
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

/// <summary>
/// 求めている製品の情報が入る
/// </summary>
public class TargetProductsClass : MonoBehaviour, ITargetProduct
{

    public ProductState ProductState
    {

        get => _productState;
    
    }
    public IReadOnlyReactiveProperty<float> SubmissionTimeLimit => _submissionTimeLimit;

    /// <summary>
    /// 残り時間
    /// </summary>
    private ReactiveProperty<float> _submissionTimeLimit = new ReactiveProperty<float>(default);
    /// <summary>
    /// 残り時間が無くなったことを通知するため取得
    /// </summary>
    private TargetProductManagerClass _targetProductManagerClass = default;
    /// <summary>
    /// 残り時間を記憶しておく
    /// </summary>
    private float _submissionTimeLimitMemory = default;
    /// <summary>
    /// 求める製品
    /// </summary>
    private ProductState _productState = default;

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

        //時間を引く
        _submissionTimeLimit.Value -= Time.deltaTime;
        //残り時間が0になった時
        if (_submissionTimeLimit.Value <= 0)
        {

            //自身についているインターフェースを取得
            ITargetProduct myITargetProduct = this as ITargetProduct;
            //求めている製品とヒエラルキーから自分を消す
            _targetProductManagerClass.DeleteTargetProduct(myITargetProduct);
            //非アクティブ化
            this.gameObject.SetActive(false);

        }

    }

    /// <summary>
    /// 提出された製品と自分の持っている求めている製品を比較し
    /// 適切なBoolを返す
    /// </summary>
    /// <param name="collisionObj"></param>
    public bool MatchCheck(GameObject collisionObj)
    {

        //求めている製品と一致したとき
        if (_productState.ToString() == collisionObj.name)
        {

            return true;

        }
        return false;

    }

    /// <summary>
    /// 自分の製品情報取得
    /// </summary>
    /// <param name="targetProductManagerClass">求めている製品管理クラス</param>
    /// <param name="submissionTimeLimit">このクラスの生存時間</param>
    /// <param name="productState">このクラスの求めている製品</param>
    public void SetProductInformation(TargetProductManagerClass targetProductManagerClass, float submissionTimeLimit, ProductState productState)
    {

        /*
         * 外部から値を取得
         */
        this._targetProductManagerClass = targetProductManagerClass;
        this._submissionTimeLimit.Value = submissionTimeLimit;
        this._submissionTimeLimitMemory = submissionTimeLimit;
        this._productState = productState;

    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialization()
    {

        Debug.LogError("初期化");
        _submissionTimeLimit.Value = _submissionTimeLimitMemory;
    
    }

}
