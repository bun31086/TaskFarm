// ---------------------------------------------------------  
// ChickenClass.cs  
// 卵を産む
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 鶏が卵を産む
/// </summary>
public class ChickenClass : AnimalBase
{
    #region 変数
    // 卵を出す関連の変数
    // 卵を出しているかどうかのフラグ
    private bool _isProducingEgg = false;
    // 卵を出す間隔計測用タイマー
    private float _eggTimer = 0f;
    // 卵を出す間隔（仮の値）
    private float _eggInterval = 10f;
    [SerializeField] GameObject _chichken;
    #endregion

    #region メソッド  
    // 卵を出す
    public void Produce()
    {
        // 卵を出す間であれば
        if (!_isProducingEgg)
        {
            // 卵を出し始める
            _isProducingEgg = true;
            // タイマーを初期化
            _eggTimer = 0f;
            Debug.Log("鶏が卵を出し始める.");
            Instantiate(_chichken);
        }

        // 指定の間隔で卵を出す
        _eggTimer += Time.deltaTime;
        if (_eggTimer >= _eggInterval)
        {
            Debug.Log("卵を出し終わる");
            _eggTimer = 0f;
        }
    }
    #endregion
}