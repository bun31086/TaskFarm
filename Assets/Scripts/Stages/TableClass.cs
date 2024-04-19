// ---------------------------------------------------------  
// Table.cs  
//    提出台にモノを置かれたときの処理
// 作成日:  4/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;

/// <summary>
/// 提出台にモノを置かれたときの処理
/// </summary>
public class TableClass : MonoBehaviour 
{

    [Header("スクリプト")]
    [SerializeField, Tooltip("求めている製品の管理をするクラス")]
    private TargetProductManagerClass _targetProductManagerClass = default;

    /// <summary>
    /// 農産物に当たった時
    /// </summary>
    /// <param name="collider">当たったオブジェクト</param>
    private void OnTriggerEnter(Collider collider)
    {

        //当たったオブジェクトが農産物の時
        if (collider.CompareTag("Product"))
        {
            // 要求商品と同じか調べる
            _targetProductManagerClass.SubmissionProcess(collider.gameObject);
            // そのオブジェクトを消す
            collider.gameObject.SetActive(false);
        
        }

    }

}
