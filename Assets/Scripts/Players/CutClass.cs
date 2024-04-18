// ---------------------------------------------------------  
// CutClass.cs  
// 羊の毛を刈り取るクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
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
    private Rigidbody _playerRigidbody = default;

    private Animator _playerAnimator = default;
    private Transform _animalTransform = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="animalTransform">毛刈りされる羊のトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public CutClass(Transform animalTransform, Animator playerAnimator)
    {
        _playerAnimator = playerAnimator;
        _animalTransform = animalTransform;
        _playerRigidbody = _playerAnimator.GetComponent<Rigidbody>();
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("Cutに入る");
        _iSatisfaction = _animalTransform.GetComponent<ISatisfaction>();
        // 満足度がたまっていないとき
        if (!_iSatisfaction.IsMaxSatisfaction)
        {
            return;
        }
        // 毛刈りアニメーションを再生する
        //_playerAnimator.SetBool("isCut", true);
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
        Debug.Log("Cut中");        
        // 毛刈りしているか調べる
        bool isCut = _iSatisfaction.Harvest();
        // 毛刈りが終わったら
        if (isCut)
        {
            // 毛刈りアニメーションを終了する
            _playerAnimator.SetTrigger("IsIdle");
            _playerRigidbody.isKinematic = false;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Cutを抜ける");
    }

    #endregion
}
