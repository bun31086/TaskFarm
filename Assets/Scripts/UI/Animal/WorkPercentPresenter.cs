// ---------------------------------------------------------  
// WorkPercentPresenter.cs  
// 作業率のViewとModelを仲介
// 作成日:  4/19
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 作業率のViewとModelを仲介
/// </summary>
public class WorkPercentPresenter : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private WorkPercentUI _workPercentUICow = default;
    [SerializeField]
    private WorkPercentUI _workPercentUIChicken = default;
    [SerializeField]
    private WorkPercentUI _workPercentUISheep = default;
    [SerializeField]
    private CowClass _animalBaseCow = default;
    [SerializeField]
    private ChickenClass _animalBaseChicken = default;
    [SerializeField]
    private SheepClass _animalBaseSheep = default;

    #endregion

    #region メソッド  

     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     private void Start ()
     {
        //_animalBaseCow.


     }
  
    #endregion
}
