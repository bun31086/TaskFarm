// ---------------------------------------------------------  
// GameManageData.cs  
// ゲームのデータ管理
// 作成日:  4/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// ゲームのデータ管理
/// </summary>
[CreateAssetMenu(fileName ="GameManagerData",menuName = "ScriptableObjects/GamaManagerData")]
public class GameManageData : ScriptableObject
{

    [Header("ゲームルール")]
    [SerializeField, Tooltip("目標金額")]
    private float _targetMonay = default;
    [SerializeField,Tooltip("制限時間")]
    private float _timeLimet = default;
    //プロパティ
    public float TargetMonay
    {
        get => _targetMonay;
    }
    public float TimeLimet
    {
        get => _timeLimet;
    }
}
