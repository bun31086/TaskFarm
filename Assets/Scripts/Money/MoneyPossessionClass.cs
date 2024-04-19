// ---------------------------------------------------------  
// Money.cs  
//   お金の管理
// 作成日:  4/19
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MoneyPossessionClass : MonoBehaviour,IMoneyPossession
{

    public int MoneyPossession
    {

        get => _moneyPossession;

    }

    /// <summary>
    /// 所持金
    /// </summary>
    private int _moneyPossession = 0;

    #region メソッド  

    /// <summary>
    /// 所持金を加算
    /// </summary>
    /// <param name="money">加算する金額</param>
    public void AddMoney(int money)
    {

        //所持金の加算
        _moneyPossession += money;
    
    }
  
    #endregion

}
