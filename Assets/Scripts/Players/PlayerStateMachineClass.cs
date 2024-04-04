// ---------------------------------------------------------  
// PlayerStateMachineClass.cs  
// プレイヤーのステート管理
// 作成日:  4/27
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// プレイヤーのステート管理
/// </summary>
public class PlayerStateMachineClass : IstateChenge
{

    /// <summary>
    /// 現在の移動系のステート
    /// </summary>
    private IMoveState _currentMoveState;
    /// <summary>
    /// 現在の振る舞い系のステート
    /// </summary>
    private IBehaviourState _currentBehaviorState;

    /// <summary>
    /// 移動系のステートを変えるときの処理
    /// </summary>
    /// <param name="nextState">次のステート</param>
    public void ChangeMoveState(IMoveState nextState)
    {

        //現在のステートがあるとき
        if (_currentMoveState != null)
        {

            //現在のクラスの終了処理
            _currentMoveState.Exit();

        }
        //現在のステートを更新
        _currentMoveState = nextState;
        //現在のクラスの開始処理
        _currentMoveState.Enter();

    }


    /// <summary>
    /// 振る舞い系のステートを変えるときの処理
    /// </summary>
    /// <param name="nextState">次のステート</param>
    public void ChangeBehaviorState(IBehaviourState nextState)
    {

        //現在のステートがあるとき
        if (_currentBehaviorState != null)
        {

            //現在のクラスの終了処理
            _currentBehaviorState.Exit();

        }
        //現在のステートを更新
        _currentBehaviorState = nextState;
        //現在のステートがあるとき
        if (_currentBehaviorState != null)
        {

            //現在のクラスの開始処理
            _currentBehaviorState.Enter();

        }


    }

    /// <summary>
    /// 現在のクラスの更新処理を呼び出す
    /// </summary>
    public void Update()
    {

        //現在の移動系クラスの実行処理
        _currentMoveState.Execute();
        //現在のステートがあるとき
        if (_currentBehaviorState != null)
        {

            //現在の振る舞い系クラスの実行処理
            _currentBehaviorState.Execute();

        }

    }
}
