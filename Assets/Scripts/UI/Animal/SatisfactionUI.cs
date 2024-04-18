// ---------------------------------------------------------  
// SatisfactionUI.cs  
// 動物の満足度UI
// 作成日:  4/18
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 動物の満足度UI
/// </summary>
public class SatisfactionUI : MonoBehaviour
{

    #region 変数  

    private Slider _slider = default;

    #endregion
    
    #region メソッド  
    
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     private void Awake ()
     {
        _slider = this.GetComponent<Slider>();
     }

    /// <summary>
    /// 満足度UIの大きさ変更
    /// </summary>
    /// <param name="satisfaction">大きさ</param>
    public void SatisfactionChange(float satisfaction)
    {
        _slider.value = satisfaction;
    }
  
    #endregion
}
