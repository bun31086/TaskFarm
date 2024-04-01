// ---------------------------------------------------------  
// SheepClass.cs  
// 羊毛を出す
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class SheepClass : AnimalBase, ISatisfaction
{

    #region 変数  
    // 牛乳を出す関連の変数
    // 牛乳を出しているかどうかのフラグ
    private bool _isProducingWool = false;
    // 牛乳を出す間隔計測用タイマー
    private float _woolTimer = 0f;
    // 牛乳を出す間隔（仮の値）
    private float _woolInterval = 10f;
    #endregion

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="moveVec"></param>
    public SheepClass(Vector3 moveVec)
    {
        _moveVec = moveVec;
    }
    #region メソッド  
    // 羊毛を出す
    public void Produce()
    {
        // 羊毛を出す中であれば
        if (!_isProducingWool)
        {
            // 羊毛を出し始める
            _isProducingWool = true;
            // タイマーを初期化
            _woolTimer = 0f;
            Debug.Log("羊が羊毛を出し始める.");
        }

        // 指定の間隔で卵を出す
        _woolTimer += Time.deltaTime;
        if (_woolTimer >= _woolInterval)
        {
            Debug.Log("羊毛を出し終わる");
            _woolTimer = 0f;
        }
    }

    public void Update()
    {
        // 牛乳を出す間、牛乳を出し続ける
        if (_isProducingWool)
        {
            Produce();
        }
        #endregion
    }
}