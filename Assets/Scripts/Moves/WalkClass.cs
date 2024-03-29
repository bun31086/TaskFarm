// ---------------------------------------------------------  
// WalkClass.cs  
// 歩きのクラス
// 作成日:　3/27
// 作成者:　對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 歩きクラス
/// </summary>
public class WalkClass : IMoveState
{

    #region 変数 
    public float _walkSpeed = default;
    private bool _iswalk = false;
    private CharacterController _characterController = default;
    private Animator _animator = default;
    private Vector3 _moveVec = default;
    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVec"></param>
    public WalkClass(Vector3 moveVec, Animator animator, CharacterController characterController)
    {
        _moveVec = moveVec;
        _animator = animator;
        _characterController = characterController;
    }
    #region メソッド  
    public void Enter()
    {
        _animator.SetBool("IsWalk", true);
        Debug.Log("Walk Stateに入る");
    }

    public void Execute()
    {
        ////入力移動キー
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        
        ////歩く方向
        //Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 移動方向に速度を掛けて移動
        _characterController.Move(_moveVec * _walkSpeed * Time.deltaTime);
    }
    public void Exit()
    {
        _animator.SetBool("IsWalk", false);
        Debug.Log("Walk Stateを抜ける");
    }
    #endregion
}
