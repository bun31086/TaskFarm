// ---------------------------------------------------------  
// MoveCheckClass.cs  
// 移動先確認
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 移動先確認
/// </summary>
public class MoveCheckClass <T> where T : Transform
{

    #region 変数  

    /// <summary>
    /// 呼び出したクラスのインスタンス
    /// </summary>
    private T _callTransform = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="callClass">クラス</param>
    public MoveCheckClass(T callClass)
    {

        //呼び出したクラスのインスタンス取得
        this._callTransform = callClass;
    
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
        //開始位置
        Vector3 pos = _callTransform.position + Vector3.forward * _callTransform.localScale.z;
        //サイズ
        Vector3 size = _callTransform.localScale;
        //向き
        Quaternion dire =  _callTransform.rotation;
        //距離
        float dist = _callTransform.localScale.z;

        //Boxcast内のすべてのオブジェクト取得
        hits = Physics.BoxCastAll(pos,size,_callTransform.forward,dire,dist);

        return hits;
    }
  
    #endregion

}
