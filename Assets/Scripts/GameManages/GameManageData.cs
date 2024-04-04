// ---------------------------------------------------------  
// GameManageData.cs  
// ゲームのデータ管理
// 作成日:  4/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// ゲームのデータ管理
/// </summary>
[CreateAssetMenu(fileName ="GameManagerData",menuName = "ScriptableObjects/GamaManagerData")]
public class GameManageData : ScriptableObject
{

    //プロパティ
    public int TargetMonay
    {

        get => _targetMonay;

    }
    public float TimeLimetMinutes
    {

        get => _timeLimetMinutes;

    }
    public float TimeLimetSeconds
    {

        get => _timeLimetSeconds;

    }

    [Header("ゲームルール")]
    [Header("目標金額")]
    [SerializeField, Tooltip("目標金額")]
    private int _targetMonay = default;
    [Header("制限時間")]
    [SerializeField, Tooltip("分")]
    private float _timeLimetMinutes = default;
    [SerializeField, Tooltip("秒")]
    private float _timeLimetSeconds = default;


}
