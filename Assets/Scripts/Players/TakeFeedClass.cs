// ---------------------------------------------------------  
// TakeFeedClass.cs  
// 餌を与えるクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 餌を与えるクラス
/// </summary>
public class TakeFeedClass : IBehaviourState
{

    #region 変数  

    /// <summary>
    /// 動物の満足度インターフェース
    /// </summary>
    private ISatisfaction _iSatisfaction = default;
    /// <summary>
    /// 持っているオブジェクトのトランスフォーム
    /// </summary>
    private GameObject _holdObject = default;
    /// <summary>
    /// 餌をあげている動物のトランスフォーム
    /// </summary>
    private Transform _animalTransform = default;

    private Animator _playerAnimator = default;

    private bool _isWater = false;
    private BaitClass _baitClass = default;
    private const string GREEN_BAIT = "GreenBait";
    private const string RED_BAIT = "RedBait";
    private const string BLUE_BAIT = "BlueBait";
    private const string WATER = "Water";
    /// <summary>
    /// 新しいバケツの名前
    /// </summary>
    private const string NEW_BUCKET_NAME = "Feed";
    /// <summary>
    /// バケツオブジェクトの水の箇所のオブジェクト名
    /// </summary>
    private const string NAME_BUCKET_WATER = "BucketWater";

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="holdObject">持っているオブジェクト</param>
    /// <param name="animalTransform">餌をあげている動物のトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public TakeFeedClass(GameObject holdObject, Transform animalTransform,Animator playerAnimator)
    {
        _holdObject = holdObject;
        _animalTransform = animalTransform;
        _playerAnimator = playerAnimator;
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        Debug.Log("TakeFoodに入る");
        _iSatisfaction = _animalTransform.GetComponent<ISatisfaction>();
        // 持っているオブジェクトにBaitClassがアタッチされているとき
        if (_holdObject.TryGetComponent(out BaitClass baitType))
        {
            _baitClass = baitType;
            // 餌の種類によって処理を分ける
            switch (_baitClass.TakeType)
            {
                case TakeType.Red:
                    _isWater = false;
                    break;
                case TakeType.Blue:
                    _isWater = false;
                    break;
                case TakeType.Green:
                    _isWater = false;
                    break;
                case TakeType.Water:
                    _isWater = true;
                    break;
            }
            // 餌を与えるアニメーションを再生する
            _playerAnimator.SetTrigger("IsTake");
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        Debug.Log("TakeFood中");
        // 動物に餌を与える
        bool isTake = _iSatisfaction.EatBait(_baitClass);
        // 動物が餌を食べ終わったら
        if (!isTake)
        {
            // 餌を与えるアニメーションを終了する
            _playerAnimator.SetTrigger("IsTake");
            // 水を与えたとき
            if (_isWater)
            {
                //持っているオブジェクトの名前を切り替える
                _holdObject.name = NEW_BUCKET_NAME;
                //バケツオブジェクトの水の箇所の見た目を表示する
                _holdObject.transform.Find(NAME_BUCKET_WATER).gameObject.SetActive(false);

            }
            // 餌を与えたとき
            else
            {
                // 持っているオブジェクト(餌オブジェクト)の親子関係を外す
                _holdObject.transform.parent = null;
                // オブジェクトを消す
                _holdObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
        Debug.Log("TakeFoodを抜ける");
    }

    #endregion
}
