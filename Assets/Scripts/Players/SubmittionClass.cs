// ---------------------------------------------------------  
// SubmittionClass.cs  
// プレイヤーの物の提出
// 作成日:  3/29
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// プレイヤーの物の提出
/// </summary>
public class SubmittionClass : IBehaviourState
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
        Debug.Log("Submittionに入る");
        //アニメーションを再生
        //_playerAnimator.SetBool("isSubmittion", true);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Submittion中");

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Submittionを抜ける");
        //アニメーションを再生
        //_playerAnimator.SetBool("isSubmittion", false);
    }

    #endregion
}
