// ---------------------------------------------------------  
// SatisfactionPresenter.cs  
// 満足度のModelとViewの仲介
// 作成日:  4/18
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;
/// <summary>
/// 満足度のModelとViewの仲介
/// </summary>
public class SatisfactionPresenter : MonoBehaviour
{

    #region 変数  
    [SerializeField]
    private SatisfactionUI _satisfactionUICow = default;
    [SerializeField]
    private SatisfactionUI _satisfactionUIChicken = default;
    [SerializeField]
    private SatisfactionUI _satisfactionUISheep = default;
    [SerializeField]
    private CawClass _animalBaseCow = default;
    [SerializeField]
    private ChickenClass _animalBaseChicken = default;
    [SerializeField]
    private SheepClass _animalBaseSheep = default;
    
    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        _animalBaseCow.Satisfaction.Subscribe(satisfaction =>
        {
            _satisfactionUICow.SatisfactionChange(satisfaction);
        }).AddTo(this);
        _animalBaseChicken.Satisfaction.Subscribe(satisfaction =>
        {
            _satisfactionUIChicken.SatisfactionChange(satisfaction);
        }).AddTo(this);
        _animalBaseSheep.Satisfaction.Subscribe(satisfaction =>
        {
            _satisfactionUISheep.SatisfactionChange(satisfaction);
        }).AddTo(this);

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {
    }

    #endregion
}
