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

    /// <summary>
    /// ステータスを変えるときの処理
    /// </summary>
    /// <param name="nextState">次のステータス</param>
    public void ChangeState(IMoveState nextState)
    {

        //現在のステータスが
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = nextState;
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
