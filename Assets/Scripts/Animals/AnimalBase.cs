// ---------------------------------------------------------  
// AnimalBase.cs  
// 動物の基本クラス
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;
/// <summary>
/// 動物の基本クラス
/// </summary>
public class AnimalBase : MonoBehaviour, ISatisfaction
{
    #region 変数  
    /// <summary>
    /// 移動先確認
    /// </summary>
    [SerializeField]
    private Animator _animalAnimator = default;
    [SerializeField]
    private Rigidbody _rigidbody = default;
    [SerializeField]
    protected GameObject _instanceObject;
    [SerializeField]
    private GameObject _wastePrefab;
    //インターフェース依存関係
    private IAnimalStateChage _iAnimalStateChage = default;
    private Animaltype _currentAction = default;
    private IForwardCheck _iMoveCheck = default;
    private Vector3 _moveVector = default;
    private const string TAG_ITEM = "Item";
    private const string TAG_STAGE = "Stage";
    [Header("上がる満足度"), SerializeField]
    protected const float INCREASINGSATISFACTION = 1;
    //餌を食べる時間
    private float _eatDuration = 3f;
    //動物の設定速度
    private float _speed = default;
    //動物の歩く速度
    private float _walkSpeed = 2f;
    //動物の走る速度
    private float _runSpeed = 4f;
    //収穫の間隔
    protected float _interval = 3f;
    //最大の満足度
    [SerializeField]
    protected float _maxsatisfaction = 3;
    //収穫されたかどうかのフラグ
    private bool _isHarvested = false;
    //餌を食べているかどうかのフラグ
    private bool _isEating = false;
    //最大の満足度かどうかのフラグ
    protected bool _isMaxSatisfaction = false;
    //収穫間隔計測用タイマー
    protected ReactiveProperty<float> _timer = new ReactiveProperty<float>(0);
    //満足度の初期値
    protected ReactiveProperty<float> _satisfaction = new ReactiveProperty<float>(0);
    //移動系のスクリプトを代わりにインスタンスするクラス
    private MoveDI _moveDI = default;
    //Idleスクリプトが継承しているインターフェース
    private IMoveState _idle = default;
    //Walkスクリプトが継承しているインターフェース
    private IMoveState _walk = default;
    //Runスクリプトが継承しているインターフェース
    private IMoveState _run = default;
    //要求している餌の種類
    private ReactiveProperty<TakeType> _currentFood = new ReactiveProperty<TakeType>();
    //ランダムで行動を決めるコルーチン
    private IEnumerator _action = default;
    //ランダムで移動方向を決めるコルーチン
    private IEnumerator _direciton = default;
    //ランダムで要求する餌を決めるコルーチン
    private IEnumerator _food = default;
    #endregion

    #region プロパティ
    public IReadOnlyReactiveProperty<TakeType> CurrentFood => _currentFood;
    public IReadOnlyReactiveProperty<float> Satisfaction => _satisfaction;
    public IReadOnlyReactiveProperty<float> Timer => _timer;
    public bool IsMaxSatisfaction
    {
        get => _isMaxSatisfaction;
    }
    #endregion

    #region メソッド  
    /// <summary>  
    /// 更新前処理
    /// </summary>  
    private void Start()
    {
        //コンストラクタにtransformをインスタンスを設定してインスタンス化(生成)
        _iMoveCheck = new ForwardCheckClass(this.transform);
        //移動系スクリプトをかわりにインスタンスしてもらうクラスを生成
        _moveDI = new MoveDI(_animalAnimator, _rigidbody);
        //Idleクラスをインスタンスしてもらう
        _idle = _moveDI.InstanceIdle();
        //Walkクラスをインスタンスしてもらう
        _walk = _moveDI.InstanceWalk();
        //Runクラスをインスタンスしてもらう
        _run = _moveDI.InstanceRun();
        //ステートマシンをインスタンス
        _iAnimalStateChage = new AnimalStateMachineClass(_idle);
        //コルーチンをキャッシュ
        _action = ChangeAction();
        _direciton = ChangeDirection();
        _food = ChangeFood();
        //コルーチン開始
        StartCoroutine(_action);
        StartCoroutine(_direciton);
        StartCoroutine(_food);
    }

    /// <summary>  
    /// 更新処理
    /// </summary>  
    virtual protected void Update()
    {
        //Rayが当たった判定を取得
        RaycastHit[] hits = _iMoveCheck.Check();
        bool isbark = false;

        foreach (RaycastHit hit in hits)
        {
            //壁とアイテムタグを対象
            if (hit.collider.CompareTag(TAG_STAGE) || hit.collider.CompareTag(TAG_ITEM))
            {
                isbark = true;
                break;
            }
        }

        //当たったら引き返す
        if (isbark)
        {
            _moveVector *= -0.5f;
            _iAnimalStateChage.Change(_walk, _moveVector);
            return;
        }
        //更新処理実行
        _iAnimalStateChage.Execute(_speed);
    }

    /// <summary>
    /// 動物が餌を食べる処理
    /// </summary>
    public bool EatBait(BaitClass baitClass)
    {
        //食べてる時は動物が止まる
        if (!_isEating)
        {
            _isEating = true;
            _timer.Value = 0f;
            //動物の動きを止める
            StopCoroutine(_action);
            StopCoroutine(_direciton);
            StopCoroutine(_food);
            //動きを止める
            _rigidbody.isKinematic = true;
            //待機アニメーションを再生
            _animalAnimator.SetBool("IsIdle", true);
        } else
        {
            //タイマーを進める
            _timer.Value += Time.deltaTime;
            //餌を食べ終わったら
            if (_timer.Value >= _eatDuration)
            {
                //プレイヤーが渡した餌が同じか
                if (baitClass.TakeType == _currentFood.Value)
                {
                    _satisfaction.Value += INCREASINGSATISFACTION;
                }
                //満足度がMAXか
                if (_satisfaction.Value >= _maxsatisfaction)
                {
                    _isMaxSatisfaction = true;
                }
                //満足度がMAX未満か
                else if (_satisfaction.Value < _maxsatisfaction)
                {
                    _isMaxSatisfaction = false;
                }
                //動けるようにする
                _rigidbody.isKinematic = false;
                _isEating = false;
                StartCoroutine(_action);
                StartCoroutine(_direciton);
                StartCoroutine(_food);
                //餌を食べ終わった時点でごみを出す
                DropWaste();
                _timer.Value = 0f;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 動物がごみを出す
    /// </summary>
    private void DropWaste()
    {
        //プレハブからインスタンスを生成
        Instantiate(_wastePrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 収穫する
    /// </summary>
    public bool Harvest()
    {
        //収穫中は動物が止まる
        if (!_isHarvested)
        {
            _isHarvested = true;
            //タイマーを初期化
            _timer.Value = 0f;
            //動物の動きを止める
            StopCoroutine(_action);
            StopCoroutine(_direciton);
            StopCoroutine(_food);
            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = true;
            }
            if (_animalAnimator != null)
            {
                _animalAnimator.SetBool("IsIdle", true);
            }
        } else
        {
            //指定の間隔で収穫
            _timer.Value += Time.deltaTime;
            if (_timer.Value >= _interval)
            {
                GameObject instanceObject = Instantiate(_instanceObject, transform.position, Quaternion.identity);
                instanceObject.name = _instanceObject.name;
                StartCoroutine(_action);
                StartCoroutine(_direciton);
                StartCoroutine(_food);
                _rigidbody.isKinematic = false;
                _isHarvested = false;
                _satisfaction.Value = 0;
                _isMaxSatisfaction = false;
                _timer.Value = 0f;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 動物の行動をランダムに切り替える
    /// </summary>
    public IEnumerator ChangeAction()
    {
        while (true)
        {
            //ランダムに行動を切り替える
            _currentAction = (Animaltype)Random.Range(0, 3);
            switch (_currentAction)
            {
                case Animaltype.Idle:
                    _iAnimalStateChage.Change(_idle, _moveVector);
                    break;
                case Animaltype.Walk:
                    _iAnimalStateChage.Change(_walk, _moveVector);
                    _speed = _walkSpeed;
                    break;
                case Animaltype.Run:
                    _iAnimalStateChage.Change(_run, _moveVector);
                    _speed = _runSpeed;
                    break;
            }
            //3秒から5秒のランダムな間隔で行動を切り替える yield=一時停止
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    /// <summary>
    /// ランダムな間隔で方向を切り替える
    /// </summary>
    public IEnumerator ChangeDirection()
    {
        while (true)
        {
            //ランダムな間隔で方向を切り替える
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            //ランダムな方向の単位ベクトルを取得
            _moveVector = Random.insideUnitSphere;
            //当たったら45度の方向を向く
            _moveVector = Quaternion.Euler(0, 0, 45) * _moveVector;
            //上下方向は移動しない
            _moveVector.y = 0;
        }
    }

    /// <summary>
    /// 動物がランダムで選択した食べ物を要求する
    /// </summary>
    public IEnumerator ChangeFood()
    {
        while (true)
        {
            //動物がランダムで選択した食べ物を要求する
            _currentFood.Value = (TakeType)Random.Range(0, 4);
            //ランダムな間隔で行動を切り替える
            yield return new WaitForSeconds(Random.Range(10f, 15f));
        }
    }
    #endregion
}