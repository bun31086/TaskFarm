// ---------------------------------------------------------  
// SliderPresenter.cs  
//   制限時間を監視
// 作成日:  4/17
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class SliderPresenter : MonoBehaviour
{

    [Header("スクリプト")]
    [SerializeField, Tooltip("監視する対象クラス")]
    private TargetProductsClass _targetProductsClass = default;
    [SerializeField, Tooltip("反映するクラス")]
    private UISlider _uISlider = default;

    private void Start()
    {

        _targetProductsClass.SubmissionTimeLimit
                                      .Subscribe
                                       (

                                            submissionTimeLimit =>
                                            {

                                                _uISlider.ViewRemainingTime(submissionTimeLimit);

                                            }

                                        ).AddTo(_targetProductsClass);

    }


}
