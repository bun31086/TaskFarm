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
    //生成される餌の名前
    private string _feedName = "Feed";  
    //生成される餌の名前
    private string _bottleName = "Empty_Bottle";
    //ボトル箱の名前
    private string _bottleBoxName = "Box_Bottle";
    //Rayを飛ばす距離
    private float _raylength = 5f;
    //生成位置
    private Vector3 _vector3 = default;

    #endregion

    #region メソッド

    private void Start()
    {
        //生成する位置を設定
        _vector3 = this.transform.position;
        _vector3.y += 1;
        //ボトル生成箱のとき
        if (name == _bottleBoxName)
        {
            //生成するオブジェクト名を変更
            _feedName = _bottleName;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //箱の真上にRayを飛ばす
        Ray ray = new Ray(transform.position, transform.up);
        // Rayを描画
        Debug.DrawRay(ray.origin, ray.direction * _raylength, Color.red);

        //Raycastが当たらなくなったら
        if (!Physics.Raycast(ray, _raylength))
        {
            //Rayの先にfoodPrefabを生成
            Instantiate(_foodPrefab, _vector3, Quaternion.identity).name = _feedName;

        }
    }
    #endregion
}