// ---------------------------------------------------------  
// MoveCheckClass.cs  
// 移動先確認
// 作成日:  4/27~
// 作成者:  湯元来輝「
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 移動先確認
/// </summary>
public class MoveCheckClass<T> where T : Transform
{

    #region 変数  

    /// <summary>
    /// 呼び出したクラスのインスタンス
    /// </summary>
    private T _callTrans = default;
    /// <summary>
    /// BoxCastのサイズ倍率
    /// </summary>
    private float _magnification = 5;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="callTrans">呼んだクラスがアタッチされているトランスフォーム</param>
    public MoveCheckClass(T callTrans)
    {

        //呼んだクラスがアタッチされているトランスフォーム取得
        this._callTrans = callTrans;

    }

    #endregion

    #region メソッド  

    /// <summary>
    /// 移動先確認
    /// </summary>
    /// <returns>Rayの情報</returns>
    public RaycastHit[] Check()
    {
        //Rayに当たった全ての情報が入る
        RaycastHit[] hits = default;
        //倍率をかけたサイズ取得
        Vector3 size = _callTrans.lossyScale * _magnification;
        //サイズのYを呼び出したクラスのトランスフォームに変える
        size.y = _callTrans.lossyScale.y;
        //開始位置
        Vector3 pos = _callTrans.position + _callTrans.forward * size.z / 2;
        //向き
        Quaternion dire = _callTrans.rotation;
        //距離
        float dist = size.z / 2;
        //プレイヤーのLayerMask取得
        int layer = LayerMask.NameToLayer("Player");
        //演算子を反転
        layer = ~(1 << layer);
        //Boxcast内のすべてのオブジェクト取得
        hits = Physics.BoxCastAll(pos, size, _callTrans.forward, dire, dist,layer);
        //モノが入った時
        if (hits.Length > 0)
        {

            //最初に入ったものの名前
            Debug.Log(hits[0].collider.name);
        
        }
        //Rayの情報を返す
        return hits;

    }

    #endregion

}
