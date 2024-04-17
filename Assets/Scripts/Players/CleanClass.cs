// ---------------------------------------------------------  
// CleanClass.cs  
// 掃除するクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 掃除するクラス
/// </summary>
public class CleanClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// 掃除されるオブジェクト
    /// </summary>
    private GameObject _nearObject = default;

    private Animator _playerAnimator = default;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="nearObject">掃除されるオブジェクト</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public CleanClass(GameObject nearObject,Animator playerAnimator)
    {
        _nearObject = nearObject;
        _playerAnimator = playerAnimator;
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        // 掃除アニメーションを開始する
        //_playerAnimator.SetBool("isClean", true);
        // 掃除していたオブジェクトを消す
        _nearObject.SetActive(false);
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
        // 掃除アニメーションを終了する
        //_playerAnimator.SetBool("isClean", false);
    }

    #endregion
}
