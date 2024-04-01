// ---------------------------------------------------------  
// GateClass.cs  
// ゲートの動作クラス
// 作成日:  4/1
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// ゲートの動作クラス
/// </summary>
public class GateClass : MonoBehaviour
{

    #region 変数  

    /// <summary>
    /// 柵が開いているか
    /// </summary>
    private bool _isOpen = false;
    /// <summary>
    /// 回転可能か
    /// </summary>
    bool _isRotate = true;
    /// <summary>
    /// 待ち時間
    /// </summary>
    private WaitForSeconds _waitTime = default;
    /// <summary>
    /// 回転の支点
    /// </summary>
    private Vector3 _rotatePoint = default;
    /// <summary>
    /// 回転方向
    /// </summary>
    private Vector3 _rotateDirection = default;
    /// <summary>
    /// 回転する回数
    /// </summary>
    private const int CONST_ROTATE_COUNT = 90;
    /// <summary>
    /// 回転する角度
    /// </summary>
    private const int CONST_ONE_ROTATE = 1;

    #endregion

    #region プロパティ  

    public bool IsOpen
    {
        get => _isOpen;
    }

    #endregion

    #region メソッド  


    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        //待ち時間を設定
        _waitTime = new WaitForSeconds(0.005f);
        //現在のポジションを取得
        _rotatePoint = this.transform.position;
        //回転軸を計算する
        _rotatePoint.x -= this.transform.localScale.x / 2;
    }

    /// <summary>
    /// 柵を開ける
    /// </summary>
    public void Open()
    {
        //もし回転可能なら
        if (_isRotate)
        {
            //回転不可能にする
            _isRotate = false;
            //回転方向を変更
            _rotateDirection = Vector3.up;
            //コルーチンを開始する
            StartCoroutine(GateRotate());
            //フラグを変更する
            _isOpen = true;
        }
    }
    /// <summary>
    /// 柵を閉める
    /// </summary>
    public void Close()
    {
        //もし回転可能なら
        if (_isRotate)
        {
            //回転不可能にする
            _isRotate = false;
            //回転方向を変更
            _rotateDirection = Vector3.down;
            //コルーチンを開始する
            StartCoroutine(GateRotate());
            //フラグを変更する
            _isOpen = false;
        }
    }

    /// <summary>
    /// 柵を回転させる
    /// </summary>
    IEnumerator GateRotate()
    {
        int count = 0;
        //CONST_ROTATE_COUNT回繰り返す
        while (count < CONST_ROTATE_COUNT)
        {
            //カウントアップ
            count++;
            //回転させる
            this.transform.RotateAround(_rotatePoint, _rotateDirection, CONST_ONE_ROTATE);
            //待つ
            yield return _waitTime;
        }
        //回転可能にする
        _isRotate = true;
    }
    #endregion

}
