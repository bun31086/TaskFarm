// ---------------------------------------------------------  
// OpenClass.cs  
// 柵を開けるクラス
// 作成日:  3/29
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 柵を開けるクラス
/// </summary>
public class OpenClass : IBehaviourState
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
        Debug.Log("Openに入る");
        //アニメーションを再生
        //_playerAnimator.SetBool("isOpen", true);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Open中");

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Openを抜ける");
        //アニメーションを再生
        //_playerAnimator.SetBool("isOpen", false);
    }

    #endregion
}
