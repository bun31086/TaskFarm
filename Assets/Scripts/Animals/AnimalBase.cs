// ---------------------------------------------------------  
// AnimalBase.cs  
// 動物が歩く，待機
// 作成日:  
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class AnimalBase : MonoBehaviour
{
    #region 変数  
    /// <summary>
    /// ステータス変更
    /// </summary>
    private AnimalStateMachineClass _animalStateMachineClass = new AnimalStateMachineClass();
    private MoveCheckClass _moveCheckClass;
    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
    {
    }

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        _animalStateMachineClass.Change(new IdleClass());
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animalStateMachineClass.Change(new WalkClass());
        }
    }

    #endregion
}

internal class MoveCheckClass
{
}