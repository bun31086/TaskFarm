// ---------------------------------------------------------  
// PlayerStateMachineClass.cs  
// プレイヤーのステート管理
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// プレイヤーのステート管理
/// </summary>
public class PlayerStateMachineClass
{
    private IMoveState _currentState;

    public void ChangeState(IMoveState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.Execute();
        }
    }
}
