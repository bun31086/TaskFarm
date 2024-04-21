// ---------------------------------------------------------  
// CanvasRotate.cs  
// キャンバスを常にカメラに向かせる
// 作成日:  4/18
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasRotate : MonoBehaviour
{

    #region 変数  

    private Transform _transform = default;
    private Quaternion _cameraRotation = default;
    private Transform _cameraTransform = default;

    #endregion
  
  
    #region メソッド  
    
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        _transform = this.transform;
        _cameraRotation = Camera.main.transform.rotation;
        _cameraTransform = Camera.main.transform;
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
        _transform.rotation = _cameraRotation;
     }
  
    #endregion
}
