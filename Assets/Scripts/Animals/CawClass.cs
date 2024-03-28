// ---------------------------------------------------------  
// CawClass.cs  
// 牛乳を出す
// 作成日:  3/28
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class CawClass : AnimalBase, ISatisfaction
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
    private bool _isProducingMilk = false;
    // 牛乳を出す間隔計測用タイマー
    private float _milkTimer = 0f;
    // 牛乳を出す間隔（仮の値）
    private float _milkInterval = 10f;
    #endregion

    #region メソッド  
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
    /// 収穫する
    /// </summary>
    public void Harvest()
    {
        // 収穫される
        if (!_isHarvested)
        {
            _isHarvested = true;
            Debug.Log("牛が収穫する");
        }
    }
    // 牛乳を出す
    public void ProduceMilk()
    {
        // 牛乳を出す中であれば
        if (!_isProducingMilk)
        {
            // 牛乳を出し始める
            _isProducingMilk = true;
            // タイマーを初期化
            _milkTimer = 0f;
            Debug.Log("牛が牛乳を出し始める.");
        }

        // 指定の間隔で牛乳を出す
        _milkTimer += Time.deltaTime;
        if (_milkTimer >= _milkInterval)
        {
            Debug.Log("牛乳を出し終わる");
            _milkTimer = 0f;
        }
    }

    private void Update()
    {
        // 餌を食べる中であれば、餌を食べ続ける
        if (_isEating)
        {
            EatBait();
        }

        // 牛乳を出す中であれば、牛乳を出し続ける
        if (_isProducingMilk)
        {
            ProduceMilk();
        }
    } 
    #endregion
}