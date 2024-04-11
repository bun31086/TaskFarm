// ---------------------------------------------------------  
// ProductCreate.cs  
//   
// 作成日:  4/11
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System.Collections;
using UnityEngine;

public class ChooseProductClass　: MonoBehaviour
{

    private void Start()
    {

        //一定時間後に求める製品の追加
        //StartCoroutine(CallAddTargetProduct());

    }

    private void Choose()
    {

        //enum型の要素数を取得
        int maxCount = ProductState.GetNames(typeof(ProductState)).Length;
        //要素数内のランダムな値を取得
        int number = Random.Range(0, maxCount);
        //値に対応したステートを取得
        ProductState chooseProduct = (ProductState)number;

    }

    /// <summary>
    /// 一定時間後にAddTargetProductメソッドを呼び出す
    /// </summary>
    /// <returns></returns>
    //private IEnumerator CallAddTargetProduct()
    //{

    //    //設定時間後まで待つ
    //    yield return new WaitForSeconds(_targetProductManagerData.ProductAddTime);
    //    //求める製品の選択
    //    Choose();
    //    //再起呼び出し
    //    StartCoroutine(CallAddTargetProduct());

    //}

}
