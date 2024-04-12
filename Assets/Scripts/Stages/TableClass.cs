// ---------------------------------------------------------  
// Table.cs  
//    提出台にモノを置かれたときの処理
// 作成日:  4/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UniRx;
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
    /// <param name="collision">当たったオブジェクト</param>
    private void OnCollisionExit(Collision collision)
    {

        //当たったオブジェクトが農産物の時
        if (collision.collider.CompareTag("Product"))
        {

            _targetProductManagerClass.SubmissionProcess(collision.gameObject);
        
        }

    }

}
