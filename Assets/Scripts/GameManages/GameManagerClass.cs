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

    #endregion
    #region 変数  

    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("ゲームのルール情報")]
    private GameRuleData _gameManageData = default;

    /// <summary>
    /// 目標金額
    /// </summary>
    private ReactiveProperty<int> _targetMonay = new ReactiveProperty<int>(default);
    /// <summary>
    /// 所持金のインターフェース
    /// </summary>
    private IMoneyPossession _iMoneyPossession = default;

    #endregion
    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {

        //現在のシーンがメインの時制限時間と所持金のインターフェース取得
        //現在のシーンの名前取得
        string secenName = SceneManager.GetActiveScene().name;
        //現在のシーンがメインの時
        if (secenName == "Main")
        {

            //所持金のインターフェース取得
            _iMoneyPossession = GameObject.Find("MoneyPossession").GetComponent<IMoneyPossession>();

        }

    }

    /// <summary>
    /// 所持金が目標の金額まで達したかを調べる
    /// </summary>
    /// <returns>目標金額を超えたかの判定</returns>
    private bool ClearCheck()
    {

        //所持金が目標金額を超えた時
        if (_targetMonay.Value <= _iMoneyPossession.MoneyPossession.Value)
        {

            return true;

        }
        return false;

    }

    /// <summary>
    /// リザルト処理
    /// </summary>
    public void Result()
    {

        //時間停止
        Time.timeScale = 0;
        //クリアしたかの判定を受け取る
        bool isCrear = ClearCheck();

    }

    /// <summary>
    /// ゲーム開始ボタンを押されたときにメインシーンに移動
    /// </summary>
    public void OnGameStart()
    {

        // メインシーンに移動する
        SceneManager.LoadScene("Main");

    }

    /// <summary>
    /// リザルトで確認ボタンを押したときにタイトルシーンに移動
    /// </summary>
    public void OnResultFinsh()
    {

        // タイトルシーンに移動する
        SceneManager.LoadScene("SceneName");

    }



    #endregion

}
