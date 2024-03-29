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
    public Animator Animator {get; private set; }
    public float _runSpeed = default;
    private bool _runAnim = false;
    private CharacterController _characterController = new CharacterController();
    public Vector3 _moveVec = default;
    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVec"></param>
    public RunClass(Vector3 moveVec)
    {
        _moveVec = moveVec;
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
        ////入力移動キー
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        ////歩く方向
        //Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 移動方向に速度を掛けて移動
            _characterController.Move(_moveVec * _runSpeed * Time.deltaTime);
            _runAnim = true;
        } else
        {
            _runAnim = false;
        }
    }
    public void Exit()
    {
        Debug.Log("Run Stateを抜ける");
    }
    #endregion
}