// ---------------------------------------------------------  
// IBehaviourState.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  

/// <summary>
/// 振る舞いインターフェース
/// </summary>
public interface IBehaviourState
{
    public void Enter();
    public void Execute();
    public void Exit();
}
