// ---------------------------------------------------------  
// IMoveState.cs  
// 移動インターフェース
// 作成日:  3/17
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// 移動インターフェース
/// </summary>
public interface IMoveState
{
    public void Enter(Vector3 moveVector);
    public void Execute(float speed);
    public void Exit();
}

