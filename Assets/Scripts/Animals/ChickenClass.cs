// ---------------------------------------------------------  
// ChickenClass.cs  
// 卵を産む
// 作成日:  
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 鶏が卵を産む
/// </summary>
public class ChickenClass : AnimalBase, ISatisfaction
{
    #region 変数  
    // 餌を食べる関連の変数 
    // 餌を食べているかどうかのフラグ
    private bool _isEating = false;
    // 餌を食べる時間計測用タイマー
    private float _eatTimer = 0f;
    // 餌を食べる時間（仮の値）
    private float _eatDuration = 5f;

    // 収穫関連の変数
    // 収穫されたかどうかのフラグ
    private bool _isHarvested = false;

    // 牛乳を出す関連の変数
    // 牛乳を出しているかどうかのフラグ
    private bool _isProducingMeat = false;
    // 牛乳を出す間隔計測用タイマー
    private float _meatTimer = 0f;
    // 牛乳を出す間隔（仮の値）
    private float _meatInterval = 10f;
    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {

    }

    /// <summary>
    /// 餌を食べる
    /// </summary>
    public void EatBait()
    {
        //牛が餌を食べる状態
        if (!_isEating)
        {
            _isEating = true;
            _eatTimer = 0f;
            Debug.Log("牛が餌を食べ始める");
        }

        // 指定時間経過したら
        _eatTimer += Time.deltaTime;
        if (_eatTimer >= _eatDuration)
        {
            _isEating = false;
            Debug.Log("牛が餌を食べ終わる");
        }
    }

    /// <summary>
    /// 収穫
    /// </summary>
    public void Harvest()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
