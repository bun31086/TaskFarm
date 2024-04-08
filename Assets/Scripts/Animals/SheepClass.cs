// ---------------------------------------------------------  
// SheepClass.cs  
// 羊毛を出す
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 羊が羊毛を出す
/// </summary>
public class SheepClass : AnimalBase
{
    #region 変数  
    // 牛乳を出す関連の変数
    // 牛乳を出しているかどうかのフラグ
    private bool _isProducingWool = false;
    // 牛乳を出す間隔計測用タイマー
    private float _woolTimer = 0f;
    // 牛乳を出す間隔（仮の値）
    private float _woolInterval = 10f;
    [SerializeField] GameObject _wool;
    #endregion

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
            Instantiate(_wool);
        }

        // 指定の間隔で卵を出す
        _woolTimer += Time.deltaTime;
        if (_woolTimer >= _woolInterval)
        {
            Debug.Log("羊毛を出し終わる");
            _woolTimer = 0f;
        }
    }
    #endregion
}