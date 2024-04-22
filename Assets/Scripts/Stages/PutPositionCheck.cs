// ---------------------------------------------------------  
// PutPositionCheck.cs  
// 物を置く位置を計算し、移動させる
// 作成日:  4/17
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// 物を置く位置を計算し、移動させる
/// </summary>
public class PutPositionCheck : MonoBehaviour
{

    #region 変数  

    private Transform _transform = default;
    private BoxCollider _boxCollider = default;
    /// <summary>
    /// 物オブジェクトの親オブジェクト名
    /// </summary>
    private const string PARENT_NAME = "Actions";

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {
        _transform = this.transform;
        _boxCollider = this.GetComponent<BoxCollider>();
     }
    
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
    {
        // 親オブジェクトがある場合
        if (_transform.parent != null)
        {
            return;
        }
        RaycastHit hit = default;
        // Rayの長さを定義
        float rayLength = 5f;
        // Rayを出す
        Physics.Raycast(_transform.position, Vector3.down, out hit, rayLength);
        // オブジェクトを床に置くときの位置を定義
        Vector3 putPosition = hit.point;
        // オブジェクトを直径から半径にするために使用
        const int CONVERT_HALF = 2;
        // オブジェクトの半径を計算する
        float objectHeight = _boxCollider.size.y * (_transform.localScale.y / CONVERT_HALF);
        // オブジェクトの半径分、座標をあげる
        putPosition.y += objectHeight;
        // オブジェクトを地面に置く
        _transform.position = putPosition;
        // 親オブジェクトをActionObjectFolderに変更
        _transform.parent = GameObject.Find(PARENT_NAME).transform;

    }

    #endregion
}
