// ---------------------------------------------------------  
// GameTimeLimet.cs  
//    時間をカウントダウンする
// 作成日:  4/19
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

/// <summary>
/// 時間をカウントダウンする
/// </summary>
public class GameTimeLimet : MonoBehaviour
{

    public IReadOnlyReactiveProperty<float> RemainingMinutesTime => _remainingMinutesTime;
    public IReadOnlyReactiveProperty<float> RemainingSecondsTime => _remainingSecondsTime;

    [Header("スクリプタブルオブジェクト")]
    [SerializeField,Tooltip("ゲームのルール情報")]
    private GameRuleData _gameRuleData = default;
    [Header("オブジェクト")]
    [SerializeField, Tooltip("ゲームマネージャー")]
    private GameManagerClass _gameManager = default;
    //残り時間(分)
    private ReactiveProperty<float> _remainingMinutesTime = new ReactiveProperty<float>(default);
    //残り時間(秒)
    private ReactiveProperty<float> _remainingSecondsTime = new ReactiveProperty<float>(default);

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {

        //ゲームルール情報の分と秒を取得
        _remainingMinutesTime.Value = _gameRuleData.TimeLimetMinutes;
        _remainingSecondsTime.Value = _gameRuleData.TimeLimetSeconds; 

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

        //カウントダウン
        _remainingSecondsTime.Value -= Time.deltaTime;
        //秒が0より小さいとき
        if (_remainingSecondsTime.Value <= 0)
        {

            //初期化
            _remainingSecondsTime.Value = 60;
            //１分削る
            _remainingMinutesTime.Value -= 1;
            //分が-1になった時
            if (_remainingMinutesTime.Value <= -1)
            {

                _gameManager.Result();

            }


        }
        
    }



}
