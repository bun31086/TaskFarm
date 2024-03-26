// ---------------------------------------------------------  
// PlayerManagerClass.cs  
// プレイヤー管理クラス
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// プレイヤー管理
/// </summary>
public class PlayerManagerClass : MonoBehaviour
{

    #region 変数  

    private PlayerStateMachineClass _playerStateMachine;
    private PlayerInputClass _playerInput;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {
     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        _playerStateMachine = new PlayerStateMachineClass();
        _playerInput = GetComponent<PlayerInputClass>();

        _playerStateMachine.ChangeState(new IdleClass(_playerStateMachine, _playerInput));

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {
        _playerStateMachine.Update();

    }

    #endregion
}
