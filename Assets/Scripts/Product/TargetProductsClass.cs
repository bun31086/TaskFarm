// ---------------------------------------------------------  
// Products.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class TargetProductsClass : MonoBehaviour, ITargetProduct
{

    public float SubmissionTimeLimit
    {

        get => _submissionTimeLimit;

    }

    /// <summary>
    /// 残り時間が無くなったことを通知するため取得
    /// </summary>
    private TargetProductManagerClass _targetProductManagerClass = default;
    /// <summary>
    /// 残り時間
    /// </summary>
    private float _submissionTimeLimit = default;
    /// <summary>
    /// 求める製品
    /// </summary>
    private ProductState _productState = default;

    private void Update()
    {

        //時間を引く
        _submissionTimeLimit -= Time.deltaTime;
        //残り時間が0になった時
        if (_submissionTimeLimit <= 0)
        {

            //求めている製品とヒエラルキーから自分を消す
            _targetProductManagerClass.DeleteTargetProduct();
            Destroy(this);

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
        this._submissionTimeLimit = submissionTimeLimit;
        this._productState = productState;

    }

}
