// ---------------------------------------------------------  
// IAnimalStateChage.cs  
// 依存性逆転のインターフェース
// 作成日:　4/4
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
// < summary >
// 依存性逆転のインターフェース
// </ summary >
public interface IAnimalStateChage
{
    public void Change(IMoveState nextState,Vector3 moveVector);

    public void Execute(float speed);
}