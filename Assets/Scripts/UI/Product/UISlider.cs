// ---------------------------------------------------------  
// UISlider.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{

    /// <summary>
    /// 自分のスライダー
    /// </summary>
    private Slider _mySlider = default;

    private void Start()
    {

       _mySlider = this.GetComponent<Slider>();

    }

    /// <summary>
    /// 残り時間を反映
    /// </summary>
    /// <param name="remainingTime">残り時間</param>
    public void ViewRemainingTime(float remainingTime)
    {

        print(remainingTime);
        _mySlider.value = remainingTime;
    
    }

}
