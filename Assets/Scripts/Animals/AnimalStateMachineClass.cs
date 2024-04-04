// ---------------------------------------------------------  
// AnimalStateMachine.cs  
// ステータス変更
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
    public Vector3 _moveVec = default;
    ////Idleを初期動作にする
    public Animaltype _currentAction = Animaltype.Idle;

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
    public void Update()
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

    //public IEnumerator ChangeAction()
    //{
    //    //Debug.Log("コルーチン");
    //    while (true)
    //    {
    //        // ランダムに行動を切り替える
    //        _currentAction = (Animaltype)Random.Range(0, 3);
    //        Debug.Log("Current Action: " + _currentAction);
    //        // 3秒から5秒のランダムな間隔で行動を切り替える yield=一時停止
    //        yield return new WaitForSeconds(Random.Range(3f, 5f));
    //        Debug.Log("処理終わり");
    //    }
    //}
    #endregion
}