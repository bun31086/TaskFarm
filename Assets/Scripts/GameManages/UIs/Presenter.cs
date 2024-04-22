// ---------------------------------------------------------  
// Presenter.cs  
// ViewとModelを繋ぐ
// 作成日:  4/2
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;
/// <summary>
/// ViewとModelを繋ぐ
/// </summary>
public class Presenter : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("マネークラス")]
    private MoneyPossessionClass _moneyPossessionClass = default;
    [SerializeField, Tooltip("タイムクラス")]
    private GameTimeLimet _gameTimeLimet = default;
    [SerializeField, Tooltip("ゲームマネージャー(Model)")]
    private GameManagerClass _gameManagerClass = default;
    [SerializeField, Tooltip("タイムリミットUI(View)")]
    private UITimeLimit _uiTimeLimit = default;
    [SerializeField, Tooltip("目標金額UI(View)")]
    private UITargetMoney _uiTargetMoney = default;
    [SerializeField, Tooltip("所持金UI(View)")]
    private UIMoney _uiMoney = default;
    [SerializeField, Tooltip("リザルト(View)")]
    private UIResult _uiResult = default;

    /// <summary>
    /// 分数
    /// </summary>
    private float _minutes = default;
    /// <summary>
    /// 秒数
    /// </summary>
    private float _seconds = default;
    /// <summary>
    /// 目標金額
    /// </summary>
    private float _targetMoneyNow = default;
    /// <summary>
    /// 所持金
    /// </summary>
    private float _moneyNow = default;

    #endregion
    
    #region メソッド  
  
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     void Awake()
     {
        //分数が変化したとき
        _gameTimeLimet.RemainingMinutesTime
            .Subscribe(minutes => {
                //分数を格納
                _minutes = minutes;
                //UI変更
                _uiTimeLimit.TimeLimitChange(_minutes, _seconds);
            }).AddTo(this);
        //秒数が変化したとき
        _gameTimeLimet.RemainingSecondsTime
            .Subscribe(seconds => {
                //秒数を格納
                _seconds = seconds;
                //UI変更
                _uiTimeLimit.TimeLimitChange(_minutes, _seconds);
            }).AddTo(this);
        //目標金額が変化したとき
        _gameManagerClass.TargetMonay
            .Subscribe(targetMoney => {
                _targetMoneyNow = targetMoney;
                //UI変更
                _uiTargetMoney.TargetMoneyChange(targetMoney);
            }).AddTo(this);
        //所持金が変化したとき
        _moneyPossessionClass.MoneyPossession
            .Subscribe(nowMoney => {
                _moneyNow = nowMoney;
                //UI変更
                _uiMoney.NowMoneyChange(nowMoney);
            }).AddTo(this);
        _gameManagerClass.IsResult
            .Where(isResult => isResult == true)
            .Subscribe(isResult =>
            {
                //リザルト表示
                _uiResult.ResultChange(_moneyNow, _targetMoneyNow);
            }).AddTo(this);
    }
    
    #endregion
}
