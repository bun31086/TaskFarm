// ---------------------------------------------------------  
// IMoveState.cs  
// 移動インターフェース
// 作成日:  3/17
// 作成者:  竹村綾人
// ---------------------------------------------------------  

/// <summary>
/// 移動インターフェース
/// </summary>
public interface IMoveState
{
    public void Enter();
    public void Execute();
    public void Exit();
}

