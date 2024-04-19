// ---------------------------------------------------------  
// TargetProductManagerPresenter.cs  
//   求める製品を管理するクラスのリストを監視し表示するためのクラスに渡す
// 作成日:  湯元来輝
// 作成者:  4/15
// ---------------------------------------------------------  
using UniRx;
using UnityEngine;

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
    private void Awake()
    {

        //要素が追加された時に呼び出し
        _targetProductManagerClass.TargetProductCollection
                                  .ObserveAdd()
                                  .Subscribe
                                    (

                                        targetProductList =>
                                        {

                                            //リストを渡す
                                            _uITargetProductManager.ListAdd(_targetProductManagerClass.TargetProductsList);

                                        }

                                    ).AddTo(_targetProductManagerClass);
        //要素が削除された時に呼び出し
        _targetProductManagerClass.TargetProductCollection
                                  .ObserveRemove()
                                  .Subscribe
                                    (

                                        targetProductList =>
                                        {

                                            //リストを渡す
                                            _uITargetProductManager.ListReMove(_targetProductManagerClass.TargetProductsList);

                                        }

                                    ).AddTo(_targetProductManagerClass);

    }


}
