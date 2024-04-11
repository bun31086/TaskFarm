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
public class TableClass : MonoBehaviour , Iteble
{

    //プロパティ
    public IReadOnlyReactiveProperty<GameObject> CollisionObj => _collisionObj;
    /// <summary>
    /// 提出台に当たったオブジェクト
    /// </summary>
    private ReactiveProperty<GameObject> _collisionObj = new ReactiveProperty<GameObject>();

    /// <summary>
    /// 農産物に当たった時
    /// </summary>
    /// <param name="collision">当たったオブジェクト</param>
    private void OnCollisionExit(Collision collision)
    {

        //当たったオブジェクトが農産物の時
        if (collision.collider.CompareTag("Product"))
        {

            //当たったオブジェクトを更新
            _collisionObj.Value = collision.gameObject;
        
        }

    }

}
