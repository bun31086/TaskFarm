// ---------------------------------------------------------  
// CawClass.cs  
// 牛乳を出す
// 作成日:  3/28
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 牛が牛乳を出す
/// </summary>
public class CawClass : AnimalBase
{
    #region 変数      
    [SerializeField] GameObject _milk;
    // 牛乳を出しているかどうかのフラグ
    private bool _isProducingMilk = false;
    // 牛乳を出す間隔計測用タイマー
    private float _milkTimer = 0f;
    // 牛乳を出す間隔（仮の値）
    private float _milkInterval = 10f;
    #endregion

    #region メソッド  
    /// <summary>
    /// 牛乳を出す
    /// </summary>
    public void Produce()
    {
        Debug.Log("ぎゅうにゅう");
        // 牛乳を出す中であれば
        if (!_isProducingMilk)
        {
            // 牛乳を出し始める
            _isProducingMilk = true;
            // タイマーを初期化
            _milkTimer = 0f;
            Debug.Log("牛が牛乳を出し始める.");
            Instantiate(_milk);
        }

        // 指定の間隔で牛乳を出す
        _milkTimer += Time.deltaTime;
        if (_milkTimer >= _milkInterval)
        {
            Debug.Log("牛乳を出し終わる");
            _milkTimer = 0f;
        }
    }
    #endregion
}