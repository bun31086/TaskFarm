// ---------------------------------------------------------  
// AnimalPresenter.cs  
// 動物のModelとViewを仲介
// 作成日:  4/19
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;
/// <summary>
/// 動物のModelとViewを仲介
/// </summary>
public class AnimalPresenter : MonoBehaviour
{

    #region 変数  

    [SerializeField,Header("牛の作業UI")]
    private WorkPercentUI _workPercentUICow = default;
    [SerializeField, Header("鶏の作業UI")]
    private WorkPercentUI _workPercentUIChicken = default;
    [SerializeField, Header("羊の作業UI")]
    private WorkPercentUI _workPercentUISheep = default;
    [SerializeField, Header("牛の満足度UI")]
    private SatisfactionUI _satisfactionUICow = default;
    [SerializeField, Header("鶏の満足度UI")]
    private SatisfactionUI _satisfactionUIChicken = default;
    [SerializeField, Header("羊の満足度UI")]
    private SatisfactionUI _satisfactionUISheep = default;
    [SerializeField, Header("牛の要求餌UI")]
    private RequestBaitUI _requestBaitUICow = default;
    [SerializeField, Header("鶏の要求餌UI")]
    private RequestBaitUI _requestBaitUIChicken = default;
    [SerializeField, Header("羊の要求餌UI")]
    private RequestBaitUI _requestBaitUISheep = default;
    [SerializeField, Header("牛のModel")]
    private CowClass _cowClass = default;
    [SerializeField, Header("鶏のModel")]
    private ChickenClass _chickenClass = default;
    [SerializeField, Header("羊のModel")]
    private SheepClass _sheepClass = default;



    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        _cowClass.Timer.Subscribe(timer =>
        {
            _workPercentUICow.PercentChange(timer);
        }).AddTo(this);
        _sheepClass.Timer.Subscribe(timer =>
        {
            _workPercentUISheep.PercentChange(timer);
        }).AddTo(this);
        _chickenClass.Timer.Subscribe(timer =>
        {
            _workPercentUIChicken.PercentChange(timer);
        }).AddTo(this);
        _cowClass.Satisfaction.Subscribe(satisfaction =>
        {
            _satisfactionUICow.SatisfactionChange(satisfaction);
        }).AddTo(this);
        _chickenClass.Satisfaction.Subscribe(satisfaction =>
        {
            _satisfactionUIChicken.SatisfactionChange(satisfaction);
        }).AddTo(this);
        _sheepClass.Satisfaction.Subscribe(satisfaction =>
        {
            _satisfactionUISheep.SatisfactionChange(satisfaction);
        }).AddTo(this);
        _cowClass.CurrentFood.Subscribe(currentFood =>
        {
            _requestBaitUICow.SpriteChange(currentFood);
        }).AddTo(this);
        _chickenClass.CurrentFood.Subscribe(currentFood =>
        {
            _requestBaitUIChicken.SpriteChange(currentFood);
        }).AddTo(this);
        _sheepClass.CurrentFood.Subscribe(currentFood =>
        {
            _requestBaitUISheep.SpriteChange(currentFood);
        }).AddTo(this);

    }

    #endregion
}
