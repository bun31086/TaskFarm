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
    public int FarstAddProductValue
    {

        get => _farstAddProductValue;

    }
    public int ConstAddProductValue
    {

        get => _constAddProductValue;

    }
    public float SubmissionTimeLimit
    {

        get => _submissionTimeLimit;

    }
    public float ProductAddTime
    {

        get => _productAddTime;

    }

    [Header("求める製品追加時間")]
    [SerializeField, Tooltip("製品追加時間")]
    private float _productAddTime = 30f;
    [Header("求める製品がないときの追加量")]
    [SerializeField, Tooltip("求める製品がないときの製品追加量")]
    private int _farstAddProductValue = 3;
    [Header("追加の求める製品追加量")]
    [SerializeField, Tooltip("追加の求める製品追加量")]
    private int _constAddProductValue = 1;
    [Header("求める製品提出のタイムリミット")]
    [SerializeField, Tooltip("求める製品提出のタイムリミット")]
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
