// ---------------------------------------------------------  
// RubbishClass.cs  
// ごみオブジェクト
// 作成日:  4/17
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class RubbishClass : MonoBehaviour
{

    #region 変数  

    private const string PLAYER_TAG = "Player";

    #endregion
  
  
    #region メソッド  
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
  
     }
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerManagerClass playerClass))
        {
            playerClass.IsTread = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerManagerClass playerClass))
        {
            playerClass.IsTread = false;
        }
    }

    #endregion
}
