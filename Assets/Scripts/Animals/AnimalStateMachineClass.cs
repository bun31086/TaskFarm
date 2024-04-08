// ---------------------------------------------------------  
// AnimalStateMachine.cs  
// 動物ステータス変更
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class AnimalStateMachineClass : IAnimalStateChage
{
    #region 変数  
    /// <summary>
    /// 移動ステータスに沿ったクラス実行ステータス
    /// </summary>
    private IMoveState _currentState = default;
    private Vector3 _moveVec = default;
    ////Idleを初期動作にする
    private Animaltype _currentAction = Animaltype.Idle;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVec">入力値</param>
    public AnimalStateMachineClass(Vector3 moveVec)
    {
        _moveVec = moveVec;
    }

    public AnimalStateMachineClass()
    {

    }
    #endregion


    #region メソッド  
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
        //Debug.Log("ステータス変更");
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