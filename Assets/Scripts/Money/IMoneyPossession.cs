using UniRx;

public interface IMoneyPossession
{

    IReadOnlyReactiveProperty<int> MoneyPossession { get; }

    void AddMoney(int money);

}
