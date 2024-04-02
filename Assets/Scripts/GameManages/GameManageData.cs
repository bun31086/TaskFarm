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
    public int MilkPrice
    {

        get => _milkPrice;

    }
    public int EggPrice
    {

        get => _eggPrice;

    }
    public int WoolPrice
    {

        get => _woolPrice;

    }
    public int UpBonusLine
    {

        get => _upBonusLine;

    }

    [Header("ゲームルール")]
    [Header("目標金額")]
    [SerializeField, Tooltip("目標金額")]
    private int _targetMonay = default;
    [Header("ボーナス金額が上がる連鎖数")]
    [SerializeField, Tooltip("ボーナス金額が上がる連鎖数")]
    private int _upBonusLine = 5;
    [Header("制限時間")]
    [SerializeField, Tooltip("分")]
    private float _timeLimetMinutes = default;
    [SerializeField, Tooltip("秒")]
    private float _timeLimetSeconds = default;
    [Header("製品の金額")]
    [SerializeField, Tooltip("牛乳の価格")]
    private int _milkPrice = default;
    [SerializeField, Tooltip("卵の価格")]
    private int _eggPrice = default;
    [SerializeField, Tooltip("羊の毛の価格")]
    private int _woolPrice = default;

}
