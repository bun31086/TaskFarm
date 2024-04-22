// ---------------------------------------------------------  
// CutClass.cs  
// 羊の毛を刈り取るクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// 羊の毛を刈り取るクラス
/// </summary>
public class CutClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// 動物の満足度インターフェース
    /// </summary>
    private ISatisfaction _iSatisfaction = default;
    /// <summary>
    /// プレイヤーのリジッドボディー
    /// </summary>
    private Rigidbody _playerRigidbody = default;
    /// <summary>
    /// プレイヤーのリジッドボディー
    /// </summary>
    private Animator _playerAnimator = default;
    /// <summary>
    /// アニマルのトランスフォーム
    /// </summary>
    private Transform _animalTransform = default;
    /// <summary>
    /// 作業アニメーション
    /// </summary>
    private const string MILK_ANIMATION = "IsMilk";
    /// <summary>
    /// 待機アニメーション
    /// </summary>
    private const string IDLE_ANIMATION = "IsIdle";

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="animalTransform">毛刈りされる羊のトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public CutClass(Transform animalTransform, Animator playerAnimator,Rigidbody playerRigidbody)
    {
        _playerAnimator = playerAnimator;
        _animalTransform = animalTransform;
        _playerRigidbody = playerRigidbody;
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        _iSatisfaction = _animalTransform.GetComponent<ISatisfaction>();
        // 満足度がたまっていないとき
        if (!_iSatisfaction.IsMaxSatisfaction)
        {
            return;
        }
        // 毛刈りアニメーションを再生する
        _playerAnimator.SetTrigger(MILK_ANIMATION);
        // プレイヤーの動きを止める
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
        // 毛刈りしているか調べる
        bool isCut = _iSatisfaction.Harvest();
        // 毛刈りが終わったら
        if (isCut)
        {
            // 毛刈りアニメーションを終了する
            _playerAnimator.SetTrigger(IDLE_ANIMATION);
            // プレイヤーの動きを戻す
            _playerRigidbody.isKinematic = false;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
    }

    #endregion
}
