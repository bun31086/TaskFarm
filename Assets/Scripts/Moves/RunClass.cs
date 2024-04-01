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
    public float _runSpeed = default;
    public bool _isRun = false;
    private CharacterController _characterController = default;
    private Animator _animator = default;
    private Vector3 _moveVec = default;
    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVec"></param>
    public RunClass(Vector3 moveVec ,Animator animator,CharacterController characterController)
    {
        _moveVec = moveVec;
        _animator = animator;
        _characterController = characterController;
    }
    #region メソッド  
    public void Enter()
    {
        // 走るアニメーションを開始するなどの初期化処理
        //player.Animator.SetBool("IsRunning", true);
        Debug.Log("Run Stateに入る");
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 移動方向に速度を掛けて移動
            _characterController.Move(_moveVec * _runSpeed * Time.deltaTime);
            _isRun = true;
        } else
        {
            _isRun = false;
        }
    }
    public void Exit()
    {
        Debug.Log("Run Stateを抜ける");
    }
    #endregion
}
