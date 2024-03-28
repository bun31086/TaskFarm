// ---------------------------------------------------------  
// AnimalStateMachine.cs  
//   
// 作成日:  
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class AnimalStateMachineClass
{

    private Animator _animator;
    #region 変数  
    /// <summary>
    /// 移動ステータスに沿ったクラス
    /// </summary>
    private IMoveState _carrentState = default;
    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
    {
    }

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {
        //実行処理
        _carrentState.Execute();
    }

    /// <summary>
    /// ステータスを変える処理
    /// </summary>
    /// <param name="nextState">次のステータス</param>
    public void Change(IMoveState nextState)
    {
        //現在のステータスがあるとき
        if (_carrentState != null)
        {
            //終了処理実行
            _carrentState.Exit();

        }
        //ステータス更新
        _carrentState = nextState;
        //初期処理実行
        _carrentState.Enter();
    }

    #endregion
}