// ---------------------------------------------------------  
// CloseClass.cs  
// 柵を閉めるクラス
// 作成日:  3/29
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 柵を閉めるクラス
/// </summary>
public class CloseClass : IBehaviourState
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
        Debug.Log("Closeに入る");
        //アニメーションを再生
        //_playerAnimator.SetBool("isClose", true);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Close中");

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Closeを抜ける");
        //アニメーションを再生
        //_playerAnimator.SetBool("isClose", false);
    }

    #endregion
}
