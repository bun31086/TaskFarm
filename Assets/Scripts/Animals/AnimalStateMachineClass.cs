// ---------------------------------------------------------  
// AnimalStateMachine.cs  
// 動物ステータス変更
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 動物ステータス変更
/// </summary>
public class AnimalStateMachineClass : IAnimalStateChage
{
    #region 変数  
    /// <summary>
    /// 移動ステータスに沿ったクラス実行ステータス
    /// </summary>
    private IMoveState _currentState = default;
    #endregion

    #region メソッド  
    /// <summary>
    /// 実行中の処理
    /// </summary>
    public void Execute()
    {
        //実行中の処理
        _currentState.Execute();
    }

    /// <summary>
    /// ステータスを変える処理
    /// </summary>
    /// <param name="nextState">次のステータス</param>
    public void Change(IMoveState nextState)
    {
        //現在のステータスがあるとき
        if (_currentState != null)
        {
            //終了処理実行
            _currentState.Exit();
        }
        // 新しいステートを設定して初期処理を実行
        _currentState = nextState;
        //初期処理実行
        _currentState.Enter();
    }
    #endregion
}