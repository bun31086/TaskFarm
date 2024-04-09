// ---------------------------------------------------------  
// PlayerData.cs  
// プレイヤーの情報管理
// 作成日:  3/27~
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// プレイヤーの情報管理
/// </summary>
[CreateAssetMenu(fileName ="PlayerData",menuName = "ScriptableObjects/PlayerData")]
public class PlayerManagerData : ScriptableObject
{

    //プロパティ
    public float Speed
    {

        get => _speed;

    }

    [Header("プレイヤーのステータス")]
    [SerializeField,Tooltip("プレイヤーの移動速度")]
    private float _speed = 5f;

}
