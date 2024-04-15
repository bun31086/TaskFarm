// ---------------------------------------------------------  
// ChickenClass.cs  
// 卵を産む
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 鶏が卵を産む
/// </summary>
public class ChickenClass : AnimalBase
{
    #region 変数
    // 卵を出す間隔計測用タイマー
    private float _eggTimer = 0f;
    // 卵を出す間隔（仮の値）
    private float _eggInterval = 10f;

    #endregion

    #region メソッド  
    // 卵を出す
    public void Harvested()
    {
        // 指定の間隔で卵を出す
        _eggTimer += Time.deltaTime;
        if (_eggTimer >= _eggInterval)
        {
            Debug.Log("卵を出し終わる");
            _eggTimer = 0f;
        }
    }
    #endregion
}