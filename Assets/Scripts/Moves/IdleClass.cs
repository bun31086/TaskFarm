// ---------------------------------------------------------  
// IdleClass.cs  
// 待機のクラス
// 作成日:  
// 作成者:  
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
    public IdleClass()
    {
    }

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
