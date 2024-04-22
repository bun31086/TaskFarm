// ---------------------------------------------------------  
// WalkClass.cs  
// 歩きのクラス
// 作成日:　3/27
// 作成者:　對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 動物もプレイヤーも使う歩きクラス
/// </summary>
public class WalkClass : IMoveState
{
    #region 変数
    private Rigidbody _rigidbody = default;
    private Animator _animator = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="rigidbody"></param>
    public WalkClass( Animator animator, Rigidbody rigidbody)
    {
        _animator = animator;
        _rigidbody = rigidbody;
    }
    #endregion

    #region メソッド
    public void Enter(Vector3 moveVector)
    {
        _animator.SetBool("IsWalk", true);
        //移動する方向に向きを変える
        Vector3 lookPos = moveVector + _rigidbody.transform.position;
        _rigidbody.transform.LookAt(lookPos);
    }

    public void Execute(float speed)
    {
        // 移動速度を掛けて移動
        _rigidbody.velocity = (_rigidbody.transform.forward * speed) + (Vector3.up * _rigidbody.velocity.y);
    }

    public void Exit()
    {
        // Walkアニメーションを終了
        _animator.SetBool("IsWalk", false);
    }
    #endregion
}