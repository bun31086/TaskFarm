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
    private Animator _animator = default;

    #endregion


    #region メソッド  
    public void Enter()
    {
        
        Debug.Log("Walk Stateに入る");
    }

    public void Execute()
    {
        //移動
        float horizontal = Input.GetAxis("Horizontal");
        float verocity = Input.GetAxis("Verocity");

    }

    public void Exit()
    {
        Debug.Log("Walk Stateを抜ける");
    }

    #endregion
}
