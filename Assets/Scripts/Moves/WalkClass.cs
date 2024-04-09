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
    private Vector3 _moveVector = default;
    private float _walkSpeed = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVector"></param>
    /// <param name="walkspeed">歩く速度</param>
    /// <param name="animator"></param>
    /// <param name="rigidbody"></param>
    public WalkClass(Vector3 moveVector, float walkspeed, Animator animator, Rigidbody rigidbody)
    {
        _moveVector = moveVector;
        _walkSpeed = walkspeed;
        _animator = animator;
        _rigidbody = rigidbody;
    }
    #endregion

    #region メソッド
    public void Enter()
    {
        _animator.SetBool("IsWalk", true);
        //移動する方向に向きを変える
        Vector3 lookPos = _moveVector + (Vector3.up * _rigidbody.transform.position.y);
        _rigidbody.transform.LookAt(lookPos);
    }

    public void Execute()
    {
        Debug.Log("歩きの更新処理");
        // 移動速度を掛けて移動
        _rigidbody.velocity = (_rigidbody.transform.forward * _walkSpeed) +
                              (Vector3.up * _rigidbody.velocity.y);
    }

    public void Exit()
    {
        // Walkアニメーションを終了
        _animator.SetBool("IsWalk", false);
    }
    #endregion
}