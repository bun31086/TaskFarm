// ---------------------------------------------------------  
// TargetProductManagerPresenter.cs  
//   求める製品を管理するクラスのリストを監視し表示するためのクラスに渡す
// 作成日:  湯元来輝
// 作成者:  4/15
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;
using System.Collections.Generic;
using System;

/// <summary>
/// 求める製品を管理するクラスのリストを監視し表示するためのクラスに渡す
/// </summary>
public class TargetProductPresenterClass : MonoBehaviour
{

    [Header("スクリプト")]
    [SerializeField, Tooltip("見るクラスが格納されたリストを検出")]
    private TargetProductManagerClass _targetProductManagerClass = default;
    [SerializeField, Tooltip("画面に表示するためのクラス")]
    private UITargetProductClass _uITargetProductManager = default;

    /// <summary>
    /// 初期処理
    /// </summary>
    private void Start()
    {

        _targetProductManagerClass.TargetProductList.Subscribe
                                    ( targetProductList =>
                                    {

                                        //画面に表示
                                        _uITargetProductManager.ViewTargetProduct((IObservable<ITargetProduct>)targetProductList);

                                    }
                                    ).AddTo(_targetProductManagerClass);

    }


}
