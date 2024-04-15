// ---------------------------------------------------------  
// GameManagerClass.cs  
// ゲームを管理する
// 作成日:  4/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UniRx;
using UnityEngine;

/// <summary>
/// ゲームを管理する
/// </summary>
public class GameManagerClass : MonoBehaviour
{

    #region プロパティ

    public IReadOnlyReactiveProperty<float> TimeLimetMinutes => _timeLimetMinutes;
    public IReadOnlyReactiveProperty<float> TimeLimetSeconds => _timeLimetSeconds;
    public IReadOnlyReactiveProperty<int> TargetMonay => _targetMonay;
    public IReadOnlyReactiveProperty<int> MoneyPossession => _moneyPossession;

    #endregion
    #region 変数  

    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("ゲームマネジャーのデータ")]
    private GameManageData _gameManageData = default;

    /// <summary>
    /// 制限時間（分）
    /// </summary>
    private ReactiveProperty<float> _timeLimetMinutes = new ReactiveProperty<float>(default);
    /// <summary>
    /// 制限時間（秒）
    /// </summary>
    private ReactiveProperty<float> _timeLimetSeconds = new ReactiveProperty<float>(default);
    /// <summary>
    /// 目標金額
    /// </summary>
    private ReactiveProperty<int> _targetMonay = new ReactiveProperty<int>(default);
    /// <summary>
    /// 所持金
    /// </summary>
    private ReactiveProperty<int> _moneyPossession = new ReactiveProperty<int>(default);
    /// <summary>
    /// ゲームマネージャーのステート
    /// </summary>
    private GameManagerSutatus _gameManagerSutatus = GameManagerSutatus.Main;

    #endregion
    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {

        //スクリプタブルオブジェクトからのデータの読み込み
        _timeLimetMinutes.Value = _gameManageData.TimeLimetMinutes;
        _timeLimetSeconds.Value = _gameManageData.TimeLimetSeconds;
        _targetMonay.Value = _gameManageData.TargetMonay;

     }

     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {

        //現在のステートで処理分岐
        switch (_gameManagerSutatus)
        {

            case GameManagerSutatus.Title:

                break;
            case GameManagerSutatus.Main:



                break;
            case GameManagerSutatus.Result:

                break;

        }

    }

     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {

        //現在のステートで処理分岐
        switch (_gameManagerSutatus)
        {

            case GameManagerSutatus.Title:

                break;
            case GameManagerSutatus.Main:

                //制限時間更新
                _timeLimetSeconds.Value -= Time.deltaTime;
                //秒が0になった時
                if (_timeLimetSeconds.Value <= 0)
                {

                    //分を更新
                    _timeLimetMinutes.Value -= 1;
                    //秒を初期化
                    _timeLimetSeconds.Value = 60;

                }

                break;
            case GameManagerSutatus.Result:

                //目標金額まで達したかを調べる
                ClearCheck();

                break;

        }
        
    }

    /// <summary>
    /// 外部から金額を取得し加算
    /// </summary>
    /// <param name="price"></param>
    public void AddMoney(int price)
    {

        //提出されたオブジェクトの金額分を足す
        _moneyPossession.Value += price;

    }

    /// <summary>
    /// 所持金が目標の金額まで達したかを調べる
    /// </summary>
    private void ClearCheck()
    {

        //所持金が目標金額を超えた時
        if (_targetMonay.Value <= _moneyPossession.Value)
        {

            //リザルトにステートを変更
            _gameManagerSutatus = GameManagerSutatus.Result;

        }

    }

    /// <summary>
    /// ゲーム開始ボタンを押されたときにメインにステートを変更
    /// </summary>
    public void OnGameStart()
    {

        //メインにステートを変更
        _gameManagerSutatus = GameManagerSutatus.Main;
    
    }

    /// <summary>
    /// リザルト確認ボタンを押したときにタイトルにステートを変更
    /// </summary>
    private void OnResultFinsh()
    {

        //タイトルにステートを変更
        _gameManagerSutatus = GameManagerSutatus.Title;
    
    }
  
    #endregion

}
