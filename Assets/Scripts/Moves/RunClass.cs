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
    //public Animator Animator
    //{
    //    get; private set;
    //}
    private float _runSpeed = 4f;
    private bool _isRun = false;
    private CharacterController _characterController = default;
    private Animator _animator = default;
    private Vector3 _moveVector = default;
    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVec"></param>
    /// <param name="animator"></param>
    /// <param name="characterController"></param>
    public RunClass(Vector3 moveVec,/*float runSpeed,*/ Animator animator, CharacterController characterController)
    {
        _moveVector = moveVec;
        //_runSpeed = runSpeed;
        _animator = animator;
        _characterController = characterController;
    }
    #region メソッド  
    public void Enter()
    {
        // 走るアニメーションを開始するなどの初期化処理
        _animator.SetBool("IsRun", true);
        _characterController.transform.LookAt(_moveVector);
    }

    public void Execute()
    {
        Debug.Log("走りの更新処理");
        Debug.Log(_moveVector + ":" + _runSpeed);
        // 移動方向に速度を掛けて移動
        _characterController.Move(_moveVector * _runSpeed * Time.deltaTime);
    }
    public void Exit()
    {
        _animator.SetBool("IsRun", true);
    }
    #endregion
}
