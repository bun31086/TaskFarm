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
    private Animator _animalAnimator = default;
    private Transform _transform;
    protected Vector3 _moveVector = default;
    private float _walkSpeed = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVec"></param>
    /// <param name="animator"></param>
    /// <param name="characterController"></param>
    public WalkClass(Vector3 moveVector, Animator animator, CharacterController characterController)
    {
        _moveVector = moveVector;
        _animalAnimator = animator;
        _characterController = characterController;
    }
    #endregion

    #region メソッド
    public void MoveDirection(Transform transform, float walkSpeed )
    {
        _transform = transform;
        _walkSpeed = walkSpeed;
    }
    public void Enter()
    {
        _animalAnimator.SetBool("IsWalk", true);
    }

    public void Execute()
    {
        Debug.Log("歩きの更新処理");
        // 移動速度を掛けて移動
        _characterController.Move(_moveVector * _walkSpeed * Time.deltaTime);
        // 移動処理
        Vector3 moveDirection = new Vector3(1, 0, 0); // 例として右方向に移動するとします
        _transform.Translate(moveDirection * _walkSpeed * Time.deltaTime);

    }
    public void Exit()
    {
        // Walkアニメーションを終了
        _animalAnimator.SetBool("IsWalk", false);
    }
    #endregion
}