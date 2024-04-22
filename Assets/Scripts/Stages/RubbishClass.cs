// ---------------------------------------------------------  
// RubbishClass.cs  
// ごみオブジェクト
// 作成日:  4/17
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// ごみオブジェクトスクリプト
/// </summary>
public class RubbishClass : MonoBehaviour
{
  
    #region メソッド    

    /// <summary>
    /// ごみエリアに入ったとき
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITreadTrash iTreadTrash))
        {
            iTreadTrash.IsTread = true;
        }
    }

    /// <summary>
    /// ごみエリアを出たとき
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ITreadTrash iTreadTrash))
        {
            iTreadTrash.IsTread = false;
        }
    }

    #endregion
}
