// ---------------------------------------------------------  
// DrowRayDebug.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class DrowRayDebug : MonoBehaviour
{

    [SerializeField, Tooltip("DrowWireCubeの大きさ")]
    private float _magnification = 10;

    /// <summary>
    /// Boxcastのデバック
    /// </summary>
    private void OnDrawGizmos()
    {

        //倍率をかけたサイズ取得
        Vector3 size = this.transform.localScale * _magnification;
        //サイズのYをアタッチしているトランスフォームに変える
        size.y = this.transform.localScale.y;
        Debug.Log(size + "Drow");
        //開始位置
        Vector3 pos = this.transform.position + transform.forward * size.z / 2;
        //向き
        Quaternion dire = this.transform.rotation;

        /*
         * DrawWireCubeをプレイヤーと同じ向きに回転させる
         */
        //現在のGizmosの変換行列（マトリックス）を取得
        Matrix4x4 originalMatrix = Gizmos.matrix;
        // Gizumoをプレイヤーの向きに回転
        Gizmos.matrix = Matrix4x4.TRS(pos, dire, Vector3.one);
        //DrowWireCubeを表示
        Gizmos.DrawWireCube(Vector3.zero, size);
        //ほかのGizmoに影響を出さないようにするために初期化
        Gizmos.matrix = originalMatrix;

    }
}