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
public  class IdleClass : IMoveState
{
    #region 変数 
    private Animator _animator = default;
    private bool _isIdle = false;
    #endregion

    #region メソッド  
    public void Enter()
    {
        _animator.SetBool("IsIdle", true);
        Debug.Log("Idle Stateに入る");
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        _animator.SetBool("Isldle", false);
        Debug.Log("Idle Stateを抜ける");
    }
    #endregion
}
