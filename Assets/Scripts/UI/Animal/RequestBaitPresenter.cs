// ---------------------------------------------------------  
// RequestBaitPresenter.cs  
// 動物の表示仲介
// 作成日:  4/18
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;
/// <summary>
/// 動物の表示仲介
/// </summary>
public class RequestBaitPresenter : MonoBehaviour
{

    #region 変数  
    [SerializeField]
    private RequestBaitUI _requestBaitUICow = default;
    [SerializeField]
    private AnimalBase _animalBaseCow = default;
    [SerializeField]
    private RequestBaitUI _requestBaitUIChicken = default;
    [SerializeField]
    private AnimalBase _animalBaseChicken = default;
    [SerializeField]
    private RequestBaitUI _requestBaitUISheep = default;
    [SerializeField]
    private AnimalBase _animalBaseSheep = default;

    #endregion
   
    #region メソッド  
  
     /// <summary>  
     /// 初期化処理  
     /// </summary>  
     void Start()
     {
            _animalBaseCow.CurrentFood.Subscribe(currentFood =>
            {
                _requestBaitUICow.SpriteChange(currentFood);
            }).AddTo(this);
            _animalBaseChicken.CurrentFood.Subscribe(currentFood =>
            {
                _requestBaitUIChicken.SpriteChange(currentFood);
            }).AddTo(this);
            _animalBaseSheep.CurrentFood.Subscribe(currentFood =>
            {
                _requestBaitUISheep.SpriteChange(currentFood);
            }).AddTo(this);
     }
      
    #endregion
}
