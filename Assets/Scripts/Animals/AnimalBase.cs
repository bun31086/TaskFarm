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
    [SerializeField] GameObject _instanceObject;
    //インターフェース依存関係
    private IAnimalStateChage _iAnimalStateChage = default;
    private Animaltype _currentAction = default;
    private IForwardCheck _iMoveCheck = default;
    private Vector3 _moveVector = default;
    private const string TAG_ITEM = "Item";
    private const string TAG_STAGE = "Stage";
    private const float SATISFACTION = 10;
    //餌を食べる時間計測用タイマー
    private float _eatTimer = 0;
    //餌を食べる時間
    private float _eatDuration = 5f;
    //動物の設定速度
    private float _speed = default;
    //動物の歩く速度
    private float _walkSpeed = 2f;
    //動物の走る速度
    private float _runSpeed = 4f;
    //収穫間隔計測用タイマー
    protected float _timer = 0;
    //収穫の間隔
    protected float _interval = 10f;
    //最大の満足度
    private float _maxsatisfaction = 100f;
    //収穫されたかどうかのフラグ
    private bool _isHarvested = false;
    //餌を食べているかどうかのフラグ
    private bool _isEating = false;
    //最大の満足度かどうかのフラグ
    private bool _isMaxSatisfaction = false;
    //満足度の初期値
    [SerializeField]
    private ReactiveProperty<float> _satisfaction = new ReactiveProperty<float>(default);


    private MoveDI _moveDI = default;
    private IMoveState _idle = default;
    private IMoveState _walk = default;
    private IMoveState _run = default;
    [SerializeField]
    private ReactiveProperty<TakeType> _currentFood = new ReactiveProperty<TakeType>();


    public IReadOnlyReactiveProperty<TakeType> CurrentFood => _currentFood;
    public IReadOnlyReactiveProperty<float> Satisfaction => _satisfaction;
    public bool IsMaxSatisfaction
    {
        get => _isMaxSatisfaction;
    }


    private IEnumerator _action = default;
    private IEnumerator _direciton = default;
    private IEnumerator _food = default;

    #endregion

    #region メソッド  
    /// <summary>  
    /// 更新前処理
    /// </summary>  
    private void Start()
    {
        //コンストラクタにtransformをインスタンスを設定してインスタンス化(生成)
        _iMoveCheck = new ForwardCheckClass(this.transform);

        _moveDI = new MoveDI(_animalAnimator,_rigidbody);
        _idle = _moveDI.InstanceIdle();
        _walk = _moveDI.InstanceWalk();
        _run = _moveDI.InstanceRun();
        _iAnimalStateChage = new AnimalStateMachineClass(_idle);
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
    private void Update()
    {
        //Rayが当たった判定を取得
        RaycastHit[] hits = _iMoveCheck.Check();
        bool isbark = false;

        foreach (RaycastHit hit in hits)
        {
            //壁とアイテムタグを対象
            if (hit.collider.CompareTag(TAG_STAGE) || hit.collider.CompareTag(TAG_ITEM))
            {
                print(hit.collider);
                isbark = true;
                break;
            }
        }

        //当たったら引き返す
        if (isbark)
        {
            Debug.LogWarning("return");
            _moveVector *= -1;
            _iAnimalStateChage.Change(_walk,_moveVector);
            return;
        }
        //収穫中の動物は動かない
        //_isHarvested = false;
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
            _eatTimer = 0f;
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
            Debug.LogError("餌を食べる");
        } else
        {
            //タイマーを進める
            _eatTimer += Time.deltaTime;
            //餌を食べ終わったら
            if (_eatTimer >= _eatDuration)
            {
                Debug.LogError(baitClass.TakeType);
                //プレイヤーが渡した餌が同じか
                if (baitClass.TakeType == _currentFood.Value)
                {
                    Debug.LogError("合ってる");
                    _satisfaction.Value += SATISFACTION;
                }
                if (_satisfaction.Value >= _maxsatisfaction)
                {
                    _isMaxSatisfaction = true;
                } else if (_satisfaction.Value < _maxsatisfaction)
                {
                    _isMaxSatisfaction = false;
                }
                _rigidbody.isKinematic = false;
                _isEating = false;
                Debug.Log("餌を食べ終わる");
                StartCoroutine(_action);
                StartCoroutine(_direciton);
                StartCoroutine(_food);
                //餌を食べ終わった時点でごみを出す
                DropWaste();
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
        //ごみのプレハブを読み込む
        GameObject wastePrefab = Resources.Load<GameObject>("WastePrefab");
        //プレハブからインスタンスを生成
        Instantiate(wastePrefab, transform.position, Quaternion.identity);
        Debug.Log("動物がごみを出す");
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
            _timer = 0f;
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
            _timer += Time.deltaTime;
            if (_timer >= _interval)
            {
                Instantiate(_instanceObject, transform.position,Quaternion.identity);
                StartCoroutine(_action);
                StartCoroutine(_direciton);
                StartCoroutine(_food);
                _rigidbody.isKinematic = false;
                _isHarvested = false;
                Debug.Log("動物が収穫される");
                _satisfaction.Value = 0;
                _isMaxSatisfaction = false;
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
            Debug.Log("Change Action " + _currentAction);
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
        //コルーチン終了
        // StopCoroutine(_action);
        //再起処理
        //StartCoroutine(_action);
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
            Debug.LogWarning(_moveVector);
        }
        //再起処理
        //StartCoroutine(_direciton);
    }

    /// <summary>
    /// 動物がランダムで選択した食べ物を要求する
    /// </summary>
    public IEnumerator ChangeFood()
    {
        while (true)
        {
            Debug.LogError("Change");
            //動物がランダムで選択した食べ物を要求する
            _currentFood.Value = (TakeType)Random.Range(0, 4);
            //ランダムな間隔で行動を切り替える
            yield return new WaitForSeconds(Random.Range(10f, 15f));
        }
        //StartCoroutine(_food);
        //Debug.Log("Change Food " + _currentFood);
    }
    #endregion
}