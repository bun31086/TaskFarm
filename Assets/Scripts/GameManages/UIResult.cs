// ---------------------------------------------------------  
// UIResult.cs  
// リザルトUI
// 作成日:  4/19
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// リザルトUI
/// </summary>
public class UIResult : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("所持金テキスト")]
    private Text _moneyText = default;
    [SerializeField, Tooltip("目標金額テキスト")]
    private Text _targetMoneyText = default;
    [SerializeField, Tooltip("スター画像")]
    private Image[] _starImage = new Image[3];

    #endregion

    #region メソッド  

    /// <summary>
    /// リザルトを表示する
    /// </summary>
    /// <param name="money">所持金</param>
    /// <param name="targetMoney">目標金額</param>
    public void ResultChange(float money, float targetMoney)
    {
        // キャンバスを表示
        this.gameObject.SetActive(true);
        // 所持金テキスト変更
        _moneyText.text = string.Format("所持金　：{0}", money);
        // 目標金額テキスト変更
        _targetMoneyText.text = string.Format("目標金額：{0}", targetMoney);
        // 目標金額を３で割って、それぞれ届いていたら星を光らせる処理
        float starOneMoney = targetMoney / 3;
        money /= starOneMoney;
        float starCount = Mathf.Floor(money);
        // スターの表示
        for (int x = 0; x < starCount; x++)
        {
            _starImage[x].color = Color.white;
        }
    }

    #endregion
}
