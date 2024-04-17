// ---------------------------------------------------------  
// SqeezeClass.cs  
// 牛の乳を搾るクラス
// 作成日:  3/29
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 牛の乳を搾るクラス
/// </summary>
public class SqeezeClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// 動物の満足度インターフェース
    /// </summary>
    private ISatisfaction _iSatisfaction = default;
    private Rigidbody _playerRigidbody = default;

    private Animator _playerAnimator = default;
    private Transform _animalTransform = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="animalTransform">搾乳される牛のトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public SqeezeClass(Transform animalTransform, Animator playerAnimator)
    {
        _playerAnimator = playerAnimator;
        _animalTransform = animalTransform;
        _playerRigidbody =_playerAnimator.GetComponent<Rigidbody>();
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("Sqeezeに入る");
        _iSatisfaction = _animalTransform.GetComponent<ISatisfaction>();
        // 満足度がたまっていないとき
        if (!_iSatisfaction.IsMaxSatisfaction)
        {
            return;
        }
        // 乳搾りアニメーションを開始する
        _playerAnimator.SetTrigger("IsMilk");
        _playerRigidbody.isKinematic = true;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        // 満足度がたまっていないとき
        if (!_iSatisfaction.IsMaxSatisfaction)
        {
            return;
        }
        Debug.Log("Sqeeze中");
        // 搾乳中か調べる
        bool isSqeeze = _iSatisfaction.Harvest();
        // 搾乳が終わったら
        if (!isSqeeze)
        {
            // 乳搾りアニメーションを終了する
            _playerAnimator.SetTrigger("IsIdle");
            _playerRigidbody.isKinematic = false;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Sqeezeを抜ける");
    }

    #endregion
}
