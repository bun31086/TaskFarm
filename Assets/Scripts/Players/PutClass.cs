// ---------------------------------------------------------  
// PutClass.cs  
// ものを離すクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
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
    /// <summary>
    /// Rayが当たるLayer
    /// </summary>
    public LayerMask _layerMask = default;
    /// <summary>
    /// プレイヤーのアニメーター
    /// </summary>
    private Animator _playerAnimator = default;


    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="holdObjectTransform">持っているオブジェクトのトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメーター</param>
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
        // Raycastを定義
        RaycastHit hit = default;
        // Rayの長さを定義
        float rayLength = 5f;
        // Layerを指定
        _layerMask = 1 << LayerMask.NameToLayer("Table");
        // Rayを出し、当たったらTrue
        bool isHit = Physics.Raycast(_holdObjectTransform.position, Vector3.down,out hit, rayLength,1);
        // 置くものの下に地面がないか
        if (!isHit)
        {
            return;
        }
        // 持っているオブジェクトの親オブジェクトを解除
        _holdObjectTransform.parent = null;
        // 当たり判定をつける
        _holdObjectBoxCollider.enabled = true;
        // アニメーションを再生
        _playerAnimator.SetBool("IsPut", true);
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
        _playerAnimator.SetBool("IsPut", false);
    }

    #endregion
}
