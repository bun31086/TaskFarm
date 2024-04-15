// ---------------------------------------------------------  
// PumbClass.cs  
//   水を汲むクラス
// 作成日:  4/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

/// <summary>
/// 水を汲むクラス
/// </summary>
public class PumbClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// プレイヤーのアニメーター
    /// </summary>
    private Animator _playerAnimator = default;
    /// <summary>
    /// プレイヤーが持っているオブジェクト
    /// </summary>
    private GameObject _holdObj = default;
    /// <summary>
    /// 新しいバケツの名前
    /// </summary>
    private const string NEW_BUCKET_NAME = "Feed";
    /// <summary>
    /// バケツオブジェクトの水の箇所のオブジェクト名
    /// </summary>
    private const string NAME_BUCKET_WATER = "BucketWater";

    public PumbClass(GameObject holdObj , Animator playerAnimator)
    {

        //アニメーター取得
        this._playerAnimator = playerAnimator;
        //プレイヤーが持っているオブジェクトを取得
        this._holdObj = holdObj;
    
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    public void Enter()
    {

        //持っているオブジェクトの名前を切り替える
        _holdObj.name = NEW_BUCKET_NAME;
        //バケツオブジェクトの水の箇所の見た目を表示する
        _holdObj.transform.Find(NAME_BUCKET_WATER).gameObject.SetActive(true);
    
    }

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Execute()
    {
    
    
    }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     public void Exit ()
     {
     }
  
    #endregion

}
