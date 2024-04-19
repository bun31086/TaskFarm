// ---------------------------------------------------------  
// WorkPercentUI.cs  
// 作業率を表示するUI
// 作成日:  4/19
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// 作業率を表示するUI
/// </summary>
public class WorkPercentUI : MonoBehaviour
{

    #region 変数  

    private Slider _slider = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        _slider = this.GetComponent<Slider>();
    }

    /// <summary>
    /// 作業率を表示
    /// </summary>
    /// <param name="Percent">作業率</param>
    public void PercentChange(float percent)
    {
        _slider.value = percent;
    }

    #endregion
}
