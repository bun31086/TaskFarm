// ---------------------------------------------------------  
// TakeFeedClass.cs  
// 餌を与えるクラス
// 作成日:  3/28
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
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
    /// <summary>
    /// 水であるときTrue
    /// </summary>
    private bool _isWater = false;
    /// <summary>
    /// 作業中の時True
    /// </summary>
    private bool _isTaked = false;
    /// <summary>
    /// 餌オブジェクトの種類を決めるクラス
    /// </summary>
    private BaitClass _baitClass = default;
    /// <summary>
    /// 新しいバケツの名前
    /// </summary>
    private const string NEW_BUCKET_NAME = "Bucket_Enpty";
    /// <summary>
    /// バケツオブジェクトの水の箇所のオブジェクト名
    /// </summary>
    private const string NAME_BUCKET_WATER = "BucketWater";

    private Animator _playerAnimator = default;
    private Rigidbody _playerRigid = default;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="holdObject">持っているオブジェクト</param>
    /// <param name="animalTransform">餌をあげている動物のトランスフォーム</param>
    /// <param name="playerAnimator">プレイヤーのアニメータ</param>
    public TakeFeedClass(GameObject holdObject, Transform animalTransform,Animator playerAnimator,Rigidbody playerRigidbody)
    {
        _holdObject = holdObject;
        _animalTransform = animalTransform;
        _playerAnimator = playerAnimator;
        _playerRigid = playerRigidbody;
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    public void Enter()
    {
        _iSatisfaction = _animalTransform.GetComponent<ISatisfaction>();
        // フラグの初期化
        _isTaked = false;
        // 持っているオブジェクトにBaitClassがアタッチされているとき
        if (_holdObject.TryGetComponent(out BaitClass baitType))
        {
            _baitClass = baitType;
            // 餌の種類によって処理を分ける
            switch (_baitClass.TakeType)
            {
                case TakeType.RedBait:
                    _isWater = false;
                    break;
                case TakeType.BlueBait:
                    _isWater = false;
                    break;
                case TakeType.GreenBait:
                    _isWater = false;
                    break;
                case TakeType.Bucket:
                    _isWater = true;
                    break;
            }
            // 餌を与えるアニメーションを再生する
            _playerAnimator.SetTrigger("IsMilk");
            // プレイヤーの動作を止める
            _playerRigid.isKinematic = true;
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    public void Execute()
    {
        if (_isTaked)
        {
            return;
        }
        // 動物に餌を与える
        bool isTake = _iSatisfaction.EatBait(_baitClass);
        // 動物が餌を食べ終わったら
        if (isTake)
        {
            _isTaked = isTake;
            // 餌を与えるアニメーションを終了する
            _playerAnimator.SetTrigger("IsIdle");
            // 水を与えたとき
            if (_isWater)
            {
                // 持っているオブジェクトの名前を切り替える
                _holdObject.name = NEW_BUCKET_NAME;
                // バケツオブジェクトの水の箇所の見た目を表示する
                _holdObject.transform.Find(NAME_BUCKET_WATER).gameObject.SetActive(false);
                // プレイヤーの動作を戻す
                _playerRigid.isKinematic = false;
            }
            // 餌を与えたとき
            else
            {
                // 持っているオブジェクト(餌オブジェクト)の親子関係を外す
                _holdObject.transform.parent = null;
                // オブジェクトを消す
                _holdObject.SetActive(false);
                // 持つアニメーションを終了する
                _playerAnimator.SetTrigger("IsPut");
                // プレイヤーの動作を戻す
                _playerRigid.isKinematic = false;
            }
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Exit()
    {
    }

    #endregion
}
