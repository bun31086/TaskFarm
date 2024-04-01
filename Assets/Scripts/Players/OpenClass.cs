// ---------------------------------------------------------  
// OpenClass.cs  
// 柵を開け閉めするクラス
// 作成日:  3/29
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 柵を開け閉めクラス
/// </summary>
public class OpenClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// 開閉するゲートのスクリプト
    /// </summary>
    private GateClass _gateClass = default;

    private Animator _playerAnimator = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="gateTransform">開閉するゲームのトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public OpenClass(Transform gateTransform, Animator playerAnimator)
    {
        _playerAnimator = playerAnimator;
        _gateClass = gateTransform.GetComponent<GateClass>();
    }

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("Openに入る");
        //アニメーションを再生
        //_playerAnimator.SetBool("isOpen", true);

        //ゲートの開閉フラグを確認する
        if (!_gateClass.IsOpen)
        {
            //ゲートを開ける
            _gateClass.Open();
        } 
        else
        {           
            //ゲートを閉める
            _gateClass.Close();
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Open中");

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Openを抜ける");
        //アニメーションを再生
        //_playerAnimator.SetBool("isOpen", false);
    }

    #endregion
}
