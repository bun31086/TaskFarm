// ---------------------------------------------------------  
// GameTimeLimet.cs  
//    時間をカウントダウンする
// 作成日:  4/19
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

/// <summary>
/// 時間をカウントダウンする
/// </summary>
public class GameTimeLimet : MonoBehaviour
{

    [Header("スクリプタブルオブジェクト")]
    [SerializeField,Tooltip("ゲームのルール情報")]
    private GameRuleData _gameRuleData = default;
    [Header("オブジェクト")]
    [SerializeField, Tooltip("ゲームマネージャー")]
    private GameManagerClass _gameManager = default;
    //残り時間
    private float _remainingTime = default;

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {

        //ゲームルール情報の分と秒を秒に変換
        _remainingTime = _gameRuleData.TimeLimetMinutes * 60 +
                     _gameRuleData.TimeLimetSeconds; 

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

        //カウントダウン
        _remainingTime -= Time.deltaTime;
        //残り時間が0になった時
        if (_remainingTime <= 0)
        {

            _gameManager.Result();
        
        }
        
    }



}
