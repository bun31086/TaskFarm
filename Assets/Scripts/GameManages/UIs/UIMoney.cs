// ---------------------------------------------------------  
// UIMoney.cs  
// 所持金の表示
// 作成日:  4/2
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 所持金の表示
/// </summary>
public class UIMoney : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("所持金テキスト")]
    private Text _nowMoneyText = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// 所持金変更
    /// </summary>
    /// <param name="nowMoney"> 所持金</param>
    public void NowMoneyChange(int nowMoney)
    {
        _nowMoneyText.text = nowMoney.ToString();
    }  
    #endregion
}
