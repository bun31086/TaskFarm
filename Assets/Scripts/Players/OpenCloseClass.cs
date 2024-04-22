// ---------------------------------------------------------  
// OpenCloseClass.cs  
// 柵を開け閉めするクラス
// 作成日:  3/29
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// 柵を開け閉めクラス
/// </summary>
public class OpenCloseClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// ゲートのインターフェース
    /// </summary>
    private IOpenClose _iGate = default;

    private Animator _playerAnimator = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="gateTransform">開閉するゲームのトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public OpenCloseClass(Transform gateTransform, Animator playerAnimator)
    {
        _playerAnimator = playerAnimator;
        _iGate = gateTransform.GetComponent<IOpenClose>();
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        // ゲートの開閉フラグを確認する
        if (!_iGate.IsOpen)
        {
            // ゲートを開ける
            _iGate.Open();
        } 
        else
        {
            // ゲートを閉める
            _iGate.Close();
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
    }

    #endregion
}
