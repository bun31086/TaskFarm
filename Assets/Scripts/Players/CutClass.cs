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

    private Animator _playerAnimator = default;
    private Transform _animalTransform = default;
    private ISatisfaction _iSatisfaction = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    /// <param name="animalTransform">毛刈りされる羊のトランスフォーム</param>
    public CutClass(Animator playerAnimator, Transform animalTransform)
    {
        _playerAnimator = playerAnimator;
        _animalTransform = animalTransform;
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
        Debug.Log("Cutに入る");
        _iSatisfaction = _animalTransform.GetComponent<ISatisfaction>();
        //毛刈りアニメーションを再生する
        //_playerAnimator.SetBool("isCut", true);
        //毛刈りを実行
        _iSatisfaction.Harvest();
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Cut中");
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Cutを抜ける");
        //毛刈りアニメーションを終了する
        //_playerAnimator.SetBool("isCut", false);
    }

    #endregion
}
