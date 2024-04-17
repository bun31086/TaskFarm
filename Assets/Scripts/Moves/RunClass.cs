// ---------------------------------------------------------  
// RunClass.cs  
// 走るクラス
// 作成日:  3/28
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 走るクラス
/// </summary>
public class RunClass : IMoveState
{
    #region 変数
    private Rigidbody _rigidbody = default;
    private Animator _animator = default;
    private Vector3 _moveVector = default;  
    private float _runSpeed = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVector"></param>
    /// <param name="runSpeed">走る速度</param>
    /// <param name="animator"></param>
    /// <param name="rigidbody"></param>
    public RunClass(Animator animator, Rigidbody rigidbody)
    {
        _animator = animator;
        _rigidbody = rigidbody;
    }
    #endregion

    #region メソッド
    public void Enter(Vector3 moveVector)
    {
       // Debug.Log("走り開始");
        //Debug.Log(_moveVector);
        _animator.SetBool("IsRun", true);
        //移動する方向に向きを変える
        Vector3 lookPos = _moveVector + _rigidbody.transform.position;
        _rigidbody.transform.LookAt(lookPos);
    }

    public void Execute(float speed)
    {
        // 移動方向に速度を掛けて移動
        _rigidbody.velocity = (_rigidbody.transform.forward * speed) + (Vector3.up * _rigidbody.velocity.y);
    }
    public void Exit()
    {
        // Runアニメーションを終了
        _animator.SetBool("IsRun", false);
    }
    #endregion
}
