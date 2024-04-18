// ---------------------------------------------------------  
// SatisfactionUI.cs  
// 動物の満足度UI
// 作成日:  4/18
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 動物の満足度UI
/// </summary>
public class SatisfactionUI : MonoBehaviour
{

    #region 変数  

    private Vector3 _uiSize = Vector3.one;
    private Transform _transform = default;

    #endregion
    
    #region メソッド  
    
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     private void Awake ()
     {
        _transform = this.transform;
     }

    /// <summary>
    /// 満足度UIの大きさ変更
    /// </summary>
    /// <param name="satisfaction">大きさ</param>
    public void SatisfactionChange(float satisfaction)
    {
        _uiSize.x = satisfaction;
        _transform.localScale = _uiSize;
    }
  
    #endregion
}
