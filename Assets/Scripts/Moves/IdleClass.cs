// ---------------------------------------------------------  
// IdleClass.cs  
// 待機のクラス
// 作成日:  3/27
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 待機のクラス
/// </summary>
public class IdleClass : IMoveState
{
    #region 変数 
    private Animator _animator = default;
    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="animator">入力値</param>
    public IdleClass(Animator animator)
    {
        _animator = animator;
    }

    #region メソッド  
    public void Enter()
    {
        Debug.Log("待機開始");
        _animator.SetBool("IsIdle", true);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        // Idleアニメーションを終了
        _animator.SetBool("IsIdle", false);
    }
    #endregion
}
