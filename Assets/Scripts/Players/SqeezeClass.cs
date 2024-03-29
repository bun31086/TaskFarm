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

    private Animator _playerAnimator = default;
    private Transform _animalTransform = default;
    private ISatisfaction _iSatisfaction = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="animalTransform">搾乳される牛のトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public SqeezeClass(Transform animalTransform, Animator playerAnimator)
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
        Debug.Log("Sqeezeに入る");
        _iSatisfaction = _animalTransform.GetComponent<ISatisfaction>();
        //一番近くの牛の乳を搾る
        _iSatisfaction.Harvest();
        //乳搾りアニメーションを開始する
        //_playerAnimator.SetBool("isHarvest", true);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("Sqeeze中");

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("Sqeezeを抜ける");
        //乳搾りアニメーションを終了する
        //_playerAnimator.SetBool("isHarvest", false);
    }

    #endregion
}
