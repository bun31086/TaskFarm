// ---------------------------------------------------------  
// BaitClass.cs  
// 餌オブジェクトの種類を決める
// 作成日:  4/15
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// 餌オブジェクトの種類を決める
/// </summary>
public class BaitClass : MonoBehaviour
{
    [SerializeField]
    private TakeType _takeType = default;

    public TakeType TakeType
    {
        get => _takeType;
    }
}
