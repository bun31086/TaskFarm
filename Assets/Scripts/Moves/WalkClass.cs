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
    protected CharacterController _characterController = default;
    private Animator _animator = default;
    protected Vector3 _moveVector = default;
    private float _walkSpeed = 2f;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVec"></param>
    /// <param name="animator"></param>
    /// <param name="characterController"></param>
    public WalkClass(Vector3 moveVector, /*float walkspeed,*/Animator animator, CharacterController characterController)
    {
        _moveVector = moveVector;
        //_walkSpeed = walkspeed;
        _animator = animator;
        _characterController = characterController;
    }
    //WalkClass _walkClass = new WalkClass(_moveVector, animator, characterController);
    //walkClass.MoveDirection(transform, walkSpeed);
    #endregion

    #region メソッド
    //public void MoveDirection(Transform transform, float walkSpeed )
    //{
    //    _transform = transform;
    //    _walkSpeed = walkSpeed;
    //}
    public void Enter()
    {
        Debug.Log("enter");
        _animator.SetBool("IsWalk", true);
        //移動する方向に向きを変える
        _characterController.transform.LookAt(_moveVector);
    }

    public void Execute()
    {
        Debug.Log(_moveVector +":"+ _walkSpeed);
        // 移動速度を掛けて移動
        _characterController.Move(_moveVector * _walkSpeed * Time.deltaTime);
        // 移動処理 例右方向に移動する
        //Vector3 moveDirection = new Vector3(1, 0, 0);
        //_transform.Translate(moveDirection * _walkSpeed * Time.deltaTime);
    }
    public void Exit()
    {
        
        // Walkアニメーションを終了
        _animator.SetBool("IsWalk", false);
        Debug.Log("exit");
    }
    #endregion
}