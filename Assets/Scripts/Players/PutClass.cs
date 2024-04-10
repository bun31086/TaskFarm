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
    /// <summary>
    /// 持っているオブジェクトのリジッドボディー
    /// </summary>
    private BoxCollider _holdObjectBoxCollider = default;

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
        _holdObjectBoxCollider = holdObjectTransform.GetComponent<BoxCollider>();
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("Putに入る");
        RaycastHit hit = default;
        // Rayの長さを定義
        float rayLength = 5f;
        // Rayを出し、当たったらTrue
        bool isHit = Physics.Raycast(_holdObjectTransform.position, Vector3.down,out hit, rayLength);
        // 置くものの下に地面がないか
        if (!isHit)
        {
            return;
        }
        // オブジェクトを床に置くときの位置を定義
        Vector3 putPosition = hit.point;
        // オブジェクトを直径から半径にするために使用
        const int CONVERT_HALF = 2;
        // オブジェクトの半径を計算する
        float objectHeight = _holdObjectBoxCollider.size.y * (_holdObjectTransform.localScale.y / CONVERT_HALF);
        // オブジェクトの半径分、座標をあげる
        putPosition.y += objectHeight;
        // オブジェクトを地面に置く
        _holdObjectTransform.position = putPosition;
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
