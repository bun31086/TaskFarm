// ---------------------------------------------------------  
// TakeFeedClass.cs  
// 餌を与えるクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 餌を与えるクラス
/// </summary>
public class TakeFeedClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// 動物の満足度インターフェース
    /// </summary>
    private ISatisfaction _iSatisfaction = default;
    /// <summary>
    /// 持っているオブジェクトのトランスフォーム
    /// </summary>
    private GameObject _holdObject = default;
    /// <summary>
    /// 餌をあげている動物のトランスフォーム
    /// </summary>
    private Transform _animalTransform = default;

    private Animator _playerAnimator = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="holdObject">持っているオブジェクト</param>
    /// <param name="animalTransform">餌をあげている動物のトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public TakeFeedClass(GameObject holdObject, Transform animalTransform,Animator playerAnimator)
    {
        _holdObject = holdObject;
        _animalTransform = animalTransform;
        _playerAnimator = playerAnimator;
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("TakeFoodに入る");
        _iSatisfaction = _animalTransform.GetComponent<ISatisfaction>();
        // 動物に餌を与える
        _iSatisfaction.EatBait();
        // 餌を与えるアニメーションを再生する
        //_playerAnimator.SetBool("isTake",true);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("TakeFood中");

    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("TakeFoodを抜ける");
        // 餌を与えるアニメーションを終了する
        //_playerAnimator.SetBool("isTake", false);
        // 持っているオブジェクト(餌オブジェクト)の親子関係を外す
        _holdObject.transform.parent = null;
        // オブジェクトを消す
        _holdObject.SetActive(false);
    }

    #endregion
}
