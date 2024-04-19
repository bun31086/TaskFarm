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

    [SerializeField,Tooltip("タイムリミットのテキスト")]
    private Text _timelimitText = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// 制限時間変更
    /// </summary>
    /// <param name="minutes">制限時間（分）</param>
    /// <param name="seconds">制限時間（秒）</param>
    public void TimeLimitChange(float minutes,float seconds)
    {

        //正規化
        seconds = Mathf.Floor(seconds);

        _timelimitText.text = string.Format("{0}:{1}", minutes, seconds);
    }
  
    #endregion
}
