// ---------------------------------------------------------  
// TakemuraTestClass.cs  
// 処理動作用（竹村）
// 作成日:  3/29
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class TakemuraTestClass : MonoBehaviour
{

    #region 変数  
    [SerializeField]
    private MoveDI _move = default;
    [SerializeField]
    private Animator _animator = default;
    [SerializeField]
    private Rigidbody _rigidbody = default;

    private IdleClass _idle = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {
        //インスタンス生成
        _move = new MoveDI(_animator,_rigidbody);
     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        _idle = _move.Idle;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
     {
        //
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.LogError("A");
            //_idle.Enter();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.LogError("S");

            _move = null;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.LogError("D");

            //_move.Idle.Enter();
        }
    }
  
    #endregion
}
