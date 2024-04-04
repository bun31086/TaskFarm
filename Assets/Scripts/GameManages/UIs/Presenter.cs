// ---------------------------------------------------------  
// Presenter.cs  
// ViewとModelを繋ぐ
// 作成日:  4/2
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;
/// <summary>
/// ViewとModelを繋ぐ
/// </summary>
public class Presenter : MonoBehaviour
{

    #region 変数  

    [SerializeField, Tooltip("ゲームマネージャー(Model)")]
    private GameManagerClass _gameManagerClass = default;
    [SerializeField, Tooltip("ゲームマネージャー(Model)")]
    private TargetProductManagerClass _targetProductManagerClass = default;
    [SerializeField, Tooltip("ボーナス金額UI(View)")]
    private UIBonus _uiBonus = default;
    [SerializeField, Tooltip("タイムリミットUI(View)")]
    private UITimeLimit _uiTimeLimit = default;
    [SerializeField, Tooltip("求める製品UI(View)")]
    private UITargetProduct _uiTargetProduct = default;
    [SerializeField, Tooltip("目標金額UI(View)")]
    private UITargetMoney _uiTargetMoney = default;
    [SerializeField, Tooltip("所持金UI(View)")]
    private UIMoney _uiMoney = default;

    /// <summary>
    /// 分数
    /// </summary>
    private float _minutes = default;
    /// <summary>
    /// 秒数
    /// </summary>
    private float _seconds = default;

    #endregion
    
    #region メソッド  
  
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     void Awake()
     {
        //分数が変化したとき
        _gameManagerClass.TimeLimetMinutes
            .Subscribe(minutes => {
                //分数を格納
                _minutes = minutes;
                //UI変更
                _uiTimeLimit.TimeLimitChange(_minutes, _seconds);
            }).AddTo(this);
        //秒数が変化したとき
        _gameManagerClass.TimeLimetSeconds
            .Subscribe(seconds => {
                //秒数を格納
                _seconds = seconds;
                //UI変更
                _uiTimeLimit.TimeLimitChange(_minutes, _seconds);
            }).AddTo(this);
        //目標金額が変化したとき
        _gameManagerClass.TargetMonay
            .Subscribe(targetMoney => {
                //UI変更
                _uiTargetMoney.TargetMoneyChange(targetMoney);
            }).AddTo(this);
        //所持金が変化したとき
        _gameManagerClass.MoneyPossession
            .Subscribe(nowMoney => {
                //UI変更
                _uiMoney.NowMoneyChange(nowMoney);
            }).AddTo(this);
        //連鎖ボーナスが変化したとき
        _targetProductManagerClass.ChainBonus
            .Subscribe(bonus => {
                //UI変更
                _uiBonus.BonusTextChange(bonus);
            }).AddTo(this);
     }
    
    #endregion
}
