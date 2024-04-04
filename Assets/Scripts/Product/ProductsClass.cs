// ---------------------------------------------------------  
// Products.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class ProductsClass 
{

    /// <summary>
    /// 残り時間が無くなったことを通知するため取得
    /// </summary>
    TargetProductManagerClass _targetProductManagerClass = default;
    /// <summary>
    /// 残り時間
    /// </summary>
    private float _productTimeLimeit = default;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="targetProductManagerClass">ターゲットプロダクトクラス</param>
    public ProductsClass(TargetProductManagerClass targetProductManagerClass,float productTimeLimeit)
    {

        //インスタンスを受け取り
        this._targetProductManagerClass = targetProductManagerClass;
        //残り時間の受け取り
        this._productTimeLimeit = productTimeLimeit;
    
    }

    void Update()
    {

        //時間を引く
        _productTimeLimeit -= Time.deltaTime;
        //残り時間が0になった時
        if (_productTimeLimeit <= 0)
        {

            //求めている製品から自分を消す
            _targetProductManagerClass.DeleteTargetProduct();
        
        }
    
    }



}
