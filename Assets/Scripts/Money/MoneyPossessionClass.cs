// ---------------------------------------------------------  
// Money.cs  
//   お金の管理
// 作成日:  4/19
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class MoneyPossessionClass : MonoBehaviour,IMoneyPossession
{

    public IReadOnlyReactiveProperty<int> MoneyPossession => _moneyPossession;

    /// <summary>
    /// 所持金
    /// </summary>
    private ReactiveProperty<int> _moneyPossession = new ReactiveProperty<int>(0);

    #region メソッド  

    /// <summary>
    /// 所持金を加算
    /// </summary>
    /// <param name="money">加算する金額</param>
    public void AddMoney(int money)
    {

        //所持金の加算
        _moneyPossession.Value += money;
    
    }
  
    #endregion

}
