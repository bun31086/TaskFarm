// ---------------------------------------------------------  
// UIResult.cs  
// リザルトUI
// 作成日:  4/19
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// リザルトUI
/// </summary>
public class UIResult : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private Text _moneyText = default;
    [SerializeField]
    private Text _targetMoneyText = default;
    [SerializeField]
    private Image[] _starImage = new Image[3];



    #endregion


    #region メソッド  

    public void ResultChange(float money, float targetMoney)
    {
        this.gameObject.SetActive(true);
        _moneyText.text = string.Format("所持金　：{0}", money);
        _targetMoneyText.text = string.Format("目標金額：{0}", targetMoney);
        float starOneMoney = targetMoney / 3;
        money /= starOneMoney;
        float starCount = Mathf.Floor(money);
        for (int x = 0; x < starCount; x++)
        {
            _starImage[x].color = Color.white;
        }
    }

    #endregion
}
