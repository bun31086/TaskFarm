// ---------------------------------------------------------  
// IState.cs  
// ステートパターンに使用
// 作成日:  
// 作成者:  
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

