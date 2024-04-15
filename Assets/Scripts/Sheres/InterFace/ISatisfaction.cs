// ---------------------------------------------------------  
// ISatisfaction.cs  
// 動物の好感度変更
// 作成日:  3/27
// 作成者:  竹村綾人
// ---------------------------------------------------------  

/// <summary>
/// 動物の好感度変更
/// </summary>
public interface ISatisfaction
{
    public bool EatBait(BaitClass baitClass);
    public bool Harvest();
    public bool IsMaxSatisfaction{
        get;
    }
}
