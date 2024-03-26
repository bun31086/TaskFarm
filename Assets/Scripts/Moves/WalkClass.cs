// ---------------------------------------------------------  
// WalkClass.cs  
// 歩きのクラス
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 歩きクラス
/// </summary>
public class WalkClass : IMoveState
{

    #region 変数  
    private readonly PlayerStateMachineClass _playerStateMachine;
    private readonly PlayerInputClass _playerInput;

    public WalkClass(PlayerStateMachineClass stateMachine, PlayerInputClass inputManager)
    {
        this._playerStateMachine = stateMachine;
        this._playerInput = inputManager;
    }

    #endregion


    #region メソッド  
    public void Enter()
    {
        Debug.Log("Walk Stateに入る");
    }

    public void Execute()
    {
        if (Mathf.Abs(_playerInput._horizontalInput) == 0)
        {
            _playerStateMachine.ChangeState(new IdleClass(_playerStateMachine, _playerInput));
        }
    }

    public void Exit()
    {
        Debug.Log("Walk Stateを抜ける");
    }

    #endregion
}
