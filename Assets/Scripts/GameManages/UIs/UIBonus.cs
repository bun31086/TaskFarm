// ---------------------------------------------------------  
// UIBonus.cs  
// ボーナス金額の表示
// 作成日:  4/2
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ボーナス金額の表示
/// </summary>
public class UIBonus : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("連鎖ボーナステキスト")]
    private Text _bonusText = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// ボーナステキスト変更
    /// </summary>
    /// <param name="bonus">ボーナス倍率</param>
    public void BonusTextChange(int bonus)
    {
        _bonusText.text = bonus.ToString();
    }
  
    #endregion
}
