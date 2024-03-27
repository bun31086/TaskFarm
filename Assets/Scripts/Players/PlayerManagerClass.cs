// ---------------------------------------------------------  
// PlayerManagerClass.cs  
// プレイヤー管理クラス
// 作成日:  3/27~
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
/// <summary>
/// プレイヤー管理
/// </summary>
public class PlayerManagerClass : MonoBehaviour
{

    #region 変数  

    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("プレイヤーのデータ")]
    private PlayerDataClass _playerData = default;

    /// <summary>
    /// プレイヤーステートを変更するクラスのインスタンス
    /// </summary>
    private PlayerStateMachineClass _playerStateMachine = new PlayerStateMachineClass();
    /// <summary>
    /// 移動を確認するクラスのインスタンス
    /// </summary>
    private MoveCheckClass<Transform> _moveCheck = default;





    /// <summary>
    /// プレイヤーインプットクラスのインスタンス
    /// </summary>
    private PlayerInputClass _playerInput = new PlayerInputClass();

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {

        //自分のインスタンスをコンストラクタに渡し生成
        _moveCheck = new MoveCheckClass<Transform>(this.transform);

    }

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        
        //初期ステータスに変更
        _playerStateMachine.ChangeState(new IdleClass(_playerStateMachine, _playerInput));
        //移動先確認
        _moveCheck.Check();

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {

        _playerStateMachine.Update();

    }

    /// <summary>
    /// 移動の値を取得
    /// </summary>
    /// <param name="context">入力値</param>
    public void OnMove(InputAction.CallbackContext context)
    {


    }

    #endregion
}
