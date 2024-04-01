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
    private CharacterController _characterController = default;
    private Animator _animator = default;
    private Vector3 _moveVec = default;
    #endregion

  /// <summary>
  /// コンストラクタ
  /// </summary>
  /// <param name="moveVec"></param>
  /// <param name="animator"></param>
  /// <param name="characterController"></param>
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
    }

    public void Execute()
    {
        Debug.Log("歩きの更新処理");
        // 移動方向に速度を掛けて移動
        _characterController.Move(_moveVec * _walkSpeed * Time.deltaTime);
    }

    public void Exit()
    {
        _animator.SetBool("IsWalk", false);
    }
    #endregion
}
