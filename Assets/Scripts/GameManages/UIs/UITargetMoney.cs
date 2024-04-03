// ---------------------------------------------------------  
// UITargetMoney.cs  
// 目標金額の表示
// 作成日:  4/2
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 目標金額の表示
/// </summary>
public class UITargetMoney : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("目標金額テキスト")]
    private Text _targetMoneyText = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// 目標金額の変更
    /// </summary>
    /// <param name="targetMoney">目標金額</param>
    public void TargetMoneyChange(int targetMoney)
    {
        _targetMoneyText.text = targetMoney.ToString();
    }  
    #endregion
}
