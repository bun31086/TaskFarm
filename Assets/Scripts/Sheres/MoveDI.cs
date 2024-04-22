// ---------------------------------------------------------  
// MoveDI.cs  
// 移動系処理を代わりにインスタンス
// 作成日:  4/18
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// 移動系処理を代わりに生成
/// </summary>
public class MoveDI
{

    #region 変数  

    private Animator _animator = default;
    private Rigidbody _rigidbody = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="animator">それぞれのアニメーター</param>
    /// <param name="rigidbody">それぞれのリジッドボディー</param>
    public MoveDI(Animator animator,Rigidbody rigidbody)
    {
        _animator = animator;
        _rigidbody = rigidbody;
        // インスタンス生成時に実行
        InstanceIdle();
    }

    #endregion

    #region メソッド  

    /// <summary>
    /// Idleクラス生成
    /// </summary>
    public IMoveState InstanceIdle()
    {
        IMoveState iMove = new IdleClass(_animator);
        return iMove;
    }
    /// <summary>
    /// Walkクラス生成
    /// </summary>
    public IMoveState InstanceWalk()
    {
        IMoveState iMove = new WalkClass(_animator, _rigidbody);
        return iMove;
    }
    /// <summary>
    /// Runクラス生成
    /// </summary>
    public IMoveState InstanceRun()
    {
        IMoveState iMove = new RunClass(_animator, _rigidbody);
        return iMove;
    }
  
    #endregion
}
