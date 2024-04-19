// ---------------------------------------------------------  
// InstanceFood.cs  
// 目の前の餌を生成するクラス
// 作成日:  4/19
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

/// <summary>
/// 目の前の餌を生成するクラス
/// </summary>
public class InstanceFood : MonoBehaviour
{
    #region 変数  
    [Header("生成する餌のPrefab"), SerializeField]
    private GameObject _foodPrefab;
    [Header("Rayが当たる対象のPrefab"), SerializeField]
    private GameObject _targetPrefab;
    //生成される餌の名前
    private string _spawnName = "Food";
    //Rayを飛ばす距離
    private float _raylength = 5f;
    #endregion

    #region メソッド
    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //プレイヤーの前方にRayを飛ばす
        Ray ray = new Ray(transform.position, transform.forward);
        // Rayを描画
        Debug.DrawRay(ray.origin, ray.direction * _raylength, Color.red);

        RaycastHit hitInfo;
        //Raycastがあたったら
        if (Physics.Raycast(ray, out hitInfo, _raylength))
        {
            //Rayが当たったオブジェクトがtargetPrefabであるかをチェック
            if (hitInfo.collider.gameObject.CompareTag(_targetPrefab.tag))
            {
                //Rayの先にfoodPrefabを生成
                Instantiate(_foodPrefab, hitInfo.point, Quaternion.identity).name = _spawnName;
            }
        }
    }
    #endregion
}