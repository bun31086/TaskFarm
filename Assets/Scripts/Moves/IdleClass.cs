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
public class IdleClass : IMoveState
{

    #region 変数  

    private readonly PlayerStateMachineClass _playerStateMachine;
    private readonly PlayerInputClass _playerInput;
    public IdleClass(PlayerStateMachineClass stateMachine, PlayerInputClass inputManager)
    {
        this._playerStateMachine = stateMachine;
        this._playerInput = inputManager;
    }

    #endregion

    #region メソッド  
    public void Enter()
    {
        Debug.Log("Idle Stateに入る");
    }

    public void Execute()
    {
        if (Mathf.Abs(_playerInput._horizontalInput) > 0)
        {
            _playerStateMachine.ChangeState(new WalkClass(_playerStateMachine, _playerInput));
        }
    }

    public void Exit()
    {
        Debug.Log("Idle Stateを抜ける");
    }

    #endregion
}
