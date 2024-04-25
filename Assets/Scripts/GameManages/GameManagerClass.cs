// ---------------------------------------------------------  
// GameManagerClass.cs  
// ゲームを管理する
// 作成日:  4/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームを管理する
/// </summary>
public class GameManagerClass : MonoBehaviour
{

    #region プロパティ

    public IReadOnlyReactiveProperty<int> TargetMonay => _targetMonay;
    public IReadOnlyReactiveProperty<bool> IsResult => _isResult;
    public bool IsFarstTargetClear
    {

        get => _isFarstTargetClear;
    
    }
    public bool IsSecondTargetClear
    {

        get => _isSecondTargetClear;

    }
    public bool IsThirdTargetClear
    {

        get => _isThirdTargetClear;

    }



    #endregion
    #region 変数  

    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("ゲームのルール情報")]
    private GameRuleData _gameManageData = default;

    /// <summary>
    /// 目標金額
    /// </summary>
    private ReactiveProperty<int> _targetMonay = new ReactiveProperty<int>(0);
    /// <summary>
    /// リザルトを表示するかの判定
    /// </summary>
    private ReactiveProperty<bool> _isResult = new ReactiveProperty<bool>(false);
    /// <summary>
    /// 所持金のインターフェース
    /// </summary>
    private IMoneyPossession _iMoneyPossession = default;
    /// <summary>
    /// 第一目標金額をクリアしたかの判定
    /// </summary>
    bool _isFarstTargetClear = false;
    /// <summary>
    /// 第二目標金額をクリアしたかの判定
    /// </summary>
    bool _isSecondTargetClear = false;
    /// <summary>
    /// 第三目標金額をクリアしたかの判定
    /// </summary>
    bool _isThirdTargetClear = false;

    /// <summary>
    /// 第一目標金額
    /// </summary>
    private int _farstTargetMoney = default;
    /// <summary>
    /// 第二目標金額
    /// </summary>
    private int _secondTargetMoney = default;
    /// <summary>
    /// 第三目標金額
    /// </summary>
    private int _thirdTargetMoney = default;

    #endregion
    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        //時間を戻す
        Time.timeScale = 1;
        //現在のシーンがメインの時制限時間と所持金のインターフェース取得
        //現在のシーンの名前取得
        string secenName = SceneManager.GetActiveScene().name;
        //現在のシーンがメインの時
        if (secenName == "MainScene")
        {

            //所持金のインターフェース取得
            _iMoneyPossession = GameObject.Find("MoneyPossession").GetComponent<IMoneyPossession>();
            //目標金額取得
            _targetMonay.Value = _gameManageData.TargetMonay;
            //第三目標金額取得
            _thirdTargetMoney = _targetMonay.Value;
            //第二目標金額取得
            _secondTargetMoney = _thirdTargetMoney / 2;
            //第一目標金額取得
            _farstTargetMoney = _secondTargetMoney / 2;

        }

    }

    /// <summary>
    /// 所持金が目標の金額のどこまで達したかを調べる
    /// </summary>
    /// <returns>目標金額を超えたかの判定</returns>
    private void ClearCheck()
    {

        //第一標金額を超えた時
        if (_iMoneyPossession.MoneyPossession.Value >= _farstTargetMoney)
        {

            //第一目標金額を超えた判定にする
            _isFarstTargetClear = true;
        
        }
        //第二目標金額を超えた時
        if (_iMoneyPossession.MoneyPossession.Value >= _secondTargetMoney)
        {

            //第二目標金額を超えた判定にする
            _isSecondTargetClear = true;

        }
        //第三目標金額を超えた時
        if (_iMoneyPossession.MoneyPossession.Value >= _thirdTargetMoney)
        {

            //第三目標金額を超えた判定にする
            _isThirdTargetClear = true;

        }

    }

    /// <summary>
    /// リザルト処理
    /// </summary>
    public void Result()
    {

        //時間停止
        Time.timeScale = 0;
        //所持金が目標の金額のどこまで達したかを調べる
        ClearCheck();
        // リザルト表示判定にする
        _isResult.Value = true;

    }

   

    #endregion

}
