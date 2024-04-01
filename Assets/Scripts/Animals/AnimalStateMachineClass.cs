// ---------------------------------------------------------  
// AnimalStateMachine.cs  
// ステータス変更
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class AnimalStateMachineClass
{
    #region 変数  
    /// <summary>
    /// 移動ステータスに沿ったクラス実行ステータス
    /// </summary>
    private IMoveState _carrentState = default;
    public Vector3 _moveVec = default;
    //Idleを初期動作にする
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

    //動物の動作を秒数ランダム関数で求める Idleで初期設定
    public enum Animaltype
    {
        Idle = 0,
        waik = 1,
        run = 2,
    }
    #endregion


    #region メソッド  
    private IEnumerator ChangeAction()
    {
        while (true)
        {
            // ランダムに行動を切り替える
            _currentAction = (Animaltype)Random.Range(0, 3);
            Debug.Log("Current Action: " + _currentAction);

            // 3秒から5秒のランダムな間隔で行動を切り替える yield=一時停止
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    /// <summary>
    /// ステータスを変える処理
    /// </summary>
    /// <param name="nextState">次のステータス</param>
    public void Change(IMoveState nextState)
    {
        //実行中の処理
        _carrentState.Execute();
        //現在のステータスがあるとき
        if (_carrentState != null)
        {
            //終了処理実行
            _carrentState.Exit();

        }
        // 新しいステートを設定して初期処理を実行
        _carrentState = nextState;
        //初期処理実行
        _carrentState.Enter();
    }
    #endregion
}