// ---------------------------------------------------------  
// IOpenClose.cs  
// Gateインターフェース
// 作成日:  4/4
// 作成者:  竹村綾人
// ---------------------------------------------------------  
/// <summary>
/// Gateインターフェース
/// </summary>
public interface IOpenClose
{
    public bool IsOpen
    {
        get;
    }
    public void Open();
    public void Close();
}
