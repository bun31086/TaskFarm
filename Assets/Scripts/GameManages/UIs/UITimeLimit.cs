// ---------------------------------------------------------  
// UITimeLimit.cs  
// タイムリミットの表示
// 作成日:  4/2
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// タイムリミットの表示
/// </summary>
public class UITimeLimit : MonoBehaviour
{

    #region 変数  

    [SerializeField,Tooltip("分数テキスト")]
    private Text _minutesText = default;
    [SerializeField,Tooltip("秒数テキスト")]
    private Text _secondsText = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// 制限時間変更
    /// </summary>
    /// <param name="minutes">制限時間（分）</param>
    /// <param name="seconds">制限時間（秒）</param>
    public void TimeLimitChange(float minutes,float seconds)
    {
        _minutesText.text = minutes.ToString();
        _secondsText.text = seconds.ToString();
    }
  
    #endregion
}
