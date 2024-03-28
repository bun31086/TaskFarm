// ---------------------------------------------------------  
// IBehaviourState.cs  
// 振る舞いインターフェース
// 作成日:  3/17
// 作成者:  竹村綾人
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
