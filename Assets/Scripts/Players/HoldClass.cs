// ---------------------------------------------------------  
// HoldClass.cs  
// ものを持つクラス
// 作成日:  3/27
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// ものを持つクラス
/// </summary>
public class HoldClass  : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// 持つオブジェクト
    /// </summary>
    private Transform _holdObjectTransform = default;

    private Transform _playerTransform = default;
    private Animator _playerAnimator = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="nearObject">プレイヤーに近いオブジェクト</param>
    /// <param name="playerAnimator">プレイヤーのアニメーター</param>
    /// <param name="playerTransform">プレイヤーのトランスフォーム</param>
    public HoldClass(Transform nearObject,Animator playerAnimator,Transform playerTransform)
    {
        _holdObjectTransform = nearObject;
        _playerAnimator = playerAnimator;
        _playerTransform = playerTransform;
    }


    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("Holdに入る");
        //オブジェクトをプレイヤーの子オブジェクトにする
        _holdObjectTransform.parent = _playerTransform;
        //アニメーションを再生
        _playerAnimator.SetBool("isHold", true);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Hold中");

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Holdを抜ける");
    }

  
    #endregion
}
