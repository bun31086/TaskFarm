// ---------------------------------------------------------  
// PutClass.cs  
// ものを離すクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// ものを離すクラス
/// </summary>
public class PutClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// 持っているオブジェクトのトランスフォーム
    /// </summary>
    private Transform _holdObjectTransform = default;

    private Animator _playerAnimator = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="holdObjectTransform">持っているオブジェクトのトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public PutClass(Transform holdObjectTransform, Animator playerAnimator)
    {
        _holdObjectTransform = holdObjectTransform;
        _playerAnimator = playerAnimator;
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("Putに入る");
        // オブジェクトを床に置くときの位置を定義
        Vector3 putPosition = new Vector3(0,-1,1);
        // オブジェクトを地面に置く
        _holdObjectTransform.localPosition = putPosition;
        // 持っているオブジェクトの親オブジェクトを解除
        _holdObjectTransform.parent = null;
        // アニメーションを再生
        //_playerAnimator.SetBool("isHold", false);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Put中");

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Putを抜ける");
    }

    #endregion
}
