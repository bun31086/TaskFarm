// ---------------------------------------------------------  
// CutClass.cs  
// 羊の毛を刈り取るクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 羊の毛を刈り取るクラス
/// </summary>
public class CutClass : MonoBehaviour
{

    #region 変数  

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("Cleanに入る");
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Clean中");
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Cleanを抜ける");
    }

    #endregion
}
