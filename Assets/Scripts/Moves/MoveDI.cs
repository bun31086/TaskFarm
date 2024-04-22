// ---------------------------------------------------------  
// MoveDI.cs  
//   
// 作成日:  4/18
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MoveDI
{
    #region 変数  
    private Animator _animator = default;
    private Rigidbody _rigidbody = default;
    private IdleClass _idle = default;

    public IdleClass Idle
    {
        get => _idle;
    }

    public MoveDI(Animator animator,Rigidbody rigidbody)
    {
        _animator = animator;
        _rigidbody = rigidbody;
        InstanceIdle();
    }
    #endregion

    #region メソッド  
    public IMoveState InstanceIdle()
    {
        IMoveState iMove = new IdleClass(_animator);
        return iMove;
    }
    public IMoveState InstanceWalk()
    {
        IMoveState iMove = new WalkClass(_animator, _rigidbody);
        return iMove;
    }
    public IMoveState InstanceRun()
    {
        IMoveState iMove = new RunClass(_animator, _rigidbody);
        return iMove;
    }
    #endregion
}
