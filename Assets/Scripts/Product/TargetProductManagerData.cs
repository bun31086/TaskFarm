// ---------------------------------------------------------  
// TargetProductManagerData.cs  
//   求める製品のデータ管理
// 作成日:  4/4
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;

[CreateAssetMenu(fileName = "TargetProductManagerData", menuName = "ScriptableObjects/TargetProductManagerData")]
public class TargetProductManagerData : ScriptableObject
{
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
    public int AddProductValue
    {

        get => _addProductValue;

    }
    public float SubmissionTimeLimit
    {

        get => _submissionTimeLimit;

    }
    public float ProductAddTime
    {

        get => _productAddTime;

    }

    [Header("製品追加時間")]
    [SerializeField, Tooltip("製品追加時間")]
    private float _productAddTime = 30f;
    [Header("製品追加量")]
    [SerializeField, Tooltip("1度の製品追加量")]
    private int _addProductValue = 3;
    [Header("製品提出のタイムリミット")]
    [SerializeField, Tooltip("製品提出のタイムリミット")]
    private float _submissionTimeLimit = 30;
    [Header("ボーナス金額が上がる連鎖数")]
    [SerializeField, Tooltip("ボーナス金額が上がる連鎖数")]
    private int _upBonusLine = 5;
    [Header("製品の金額")]
    [SerializeField, Tooltip("牛乳の価格")]
    private int _milkPrice = default;
    [SerializeField, Tooltip("卵の価格")]
    private int _eggPrice = default;
    [SerializeField, Tooltip("羊の毛の価格")]
    private int _woolPrice = default;

}
