// ---------------------------------------------------------  
// ChickenClass.cs  
// 鶏が卵を産むクラス
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 鶏が卵を産むクラス
/// </summary>
public class ChickenClass : AnimalBase
{
    #region 変数
    #endregion

    #region メソッド 
    override protected void Update()
    {
        base.Update();
        //満足度がMAXか
        if (INCREASINGSATISFACTIO >= _maxsatisfaction)
        {
            _isMaxSatisfaction = true;
            Harvested();
        }
        //満足度がMAX未満か
        else if (INCREASINGSATISFACTIO < _maxsatisfaction)
        {
            _isMaxSatisfaction = false;
        }
    }

    //鶏が卵を産む
    public void Harvested()
    {
        //卵を生成する処理
        GameObject instanceObject = Instantiate(_instanceObject, transform.position, Quaternion.identity);
        instanceObject.name = _instanceObject.name;
    }
    #endregion
}