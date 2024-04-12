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
    /// <summary>
    /// オブジェクトを持つ位置
    /// </summary>
    private Vector3 _objectPos = new Vector3(0,0,0.5f);

    private Transform _playerTransform = default;
    private Animator _playerAnimator = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="playerTransform">プレイヤーのトランスフォーム</param>
    /// <param name="nearObjectTransform">プレイヤーに近いオブジェクト</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public HoldClass(Transform playerTransform, Transform nearObjectTransform, Animator playerAnimator)
    {
        _holdObjectTransform = nearObjectTransform;
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
       
        // オブジェクトをプレイヤーの子オブジェクトにする
        _holdObjectTransform.parent = _playerTransform;
        // オブジェクトをプレイヤーの正面に配置
        _holdObjectTransform.localPosition = _objectPos;
        // オブジェクトの向きを固定する
        _holdObjectTransform.localRotation = Quaternion.identity;
        // アニメーションを再生
        _playerAnimator.SetBool("IsHold", true);

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
        // アニメーションを終了
        _playerAnimator.SetBool("IsHold", false);

    }


    #endregion
}
