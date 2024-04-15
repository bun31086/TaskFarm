// ---------------------------------------------------------  
// AnimalBase.cs  
// 動物が歩く，待機
// 作成日:  3/29
// 作成者:  對馬礼乃
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// 動物がランダムで歩く、待機する
/// </summary>
public class AnimalBase : MonoBehaviour, ISatisfaction
{
    #region 変数  
    /// <summary>
    /// 移動先確認
    /// </summary>
    private ForwardCheckClass _forwardCheckClass = default;
    [SerializeField]
    private Animator _animalAnimator = default;
    [SerializeField]
    private Rigidbody _rigidbody = default;
    //インターフェース依存関係
    private IAnimalStateChage _iAnimalStateChage = new AnimalStateMachineClass { };
    //Idleを初期動作にする
    private Animaltype _currentAction = Animaltype.Idle;
    private AnimalFoodtype _currentFood = default;
    private IForwardCheck _iMoveCheck = default;
    private Vector3 _moveVector = default;

    private const string TAG_ITEM = "Item";
    private const string TAG_STAGE = "Stage";

    //餌を食べる時間計測用タイマー
    private float _eatTimer = 0f;
    //餌を食べる時間（仮の値）
    private float _eatDuration = 5f;
    //動物の歩く速度
    private float _walkSpeed = 2f;
    //動物の走る速度
    private float _runSpeed = 4f;
    //収穫されたかどうかのフラグ
    private bool _isHarvested = false;
    //餌を食べているかどうかのフラグ
    private bool _isEating = false;
    //餌の種類（食べ物か水か）
    private FoodType _foodType;

    //private bool _isKinematic; 
    private int _hungerLevel = 0;
    private int _maxHungerLevel = 3;

    [SerializeField] GameObject _milk;
    // 牛乳を出しているかどうかのフラグ
    private bool _isProducing = false;
    // 牛乳を出す間隔計測用タイマー
    private float _timer = 0f;
    // 牛乳を出す間隔（仮の値）
    private float _interval = 10f;
    private bool _isMaxSatisfaction = false;
    public bool IsMaxSatisfaction
    {
        get => _isMaxSatisfaction;
    }
    #endregion

    #region メソッド  
    /// <summary>  
    /// 動物が待機する
    /// </summary>  
    private void Start()
    {
        //コンポーネント取得
        //コンストラクタにtransformをインスタンスを設定してインスタンス化(生成)
        _iMoveCheck = new ForwardCheckClass(this.transform);
        //コルーチン開始
        StartCoroutine(ChangeAction());
        StartCoroutine(ChangeDirection());
        StartCoroutine(ChangeFood());
        //ランダムな方向の単位ベクトルを取得
        //_moveVector = Random.onUnitSphere;
        print(_moveVector + "原因の可能性");
    }

    /// <summary>  
    /// 動物が歩く
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
            _iAnimalStateChage.Change(new WalkClass(_moveVector, _walkSpeed, _animalAnimator, _rigidbody));
            return;
        }
        //収穫中の動物は動かない
        _isHarvested = false;
        //更新処理実行
        _iAnimalStateChage.Execute();
    }

    public void EatBait()
    {
    }
    /// <summary>
    /// 動物が餌を食べる処理
    /// </summary>
    public void EatBait(GameObject foodGameObject)
    {
        //animalFoodtype 
        if (!_isEating)
        {
            _isEating = true;
            _eatTimer = 0f;
            Debug.Log("餌を食べる");
        } else
        {
            //要求と違う場合プレイヤーに返す
            return;
        }

        //指定時間経過したら
        _eatTimer += Time.deltaTime;
        if (_eatTimer >= _eatDuration)
        {
            _isEating = false;
            Debug.Log("動物が餌を食べ終わる");

            //餌を食べ終わった時点でごみを出す
            DropWaste();
        }
    }


    /// <summary>
    /// ごみを出す
    /// </summary>
    private void DropWaste()
    {
        Debug.Log("動物がごみを出す");
        //ごみのプレハブを読み込む
        GameObject wastePrefab = Resources.Load<GameObject>("WastePrefab");

        //プレハブからインスタンスを生成
        Instantiate(wastePrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 収穫する
    /// </summary>
    public  bool Harvest()
    {
        //収穫中は動物が止まる
        if (!_isHarvested)
        {
            _isHarvested = true;
            // タイマーを初期化
            _timer = 0f;
            StopCoroutine(ChangeAction());
            StopCoroutine(ChangeDirection());
            StopCoroutine(ChangeFood());
            //_isKinematic = true;
            //動物の動きが止まる
            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = true;
            }
            if (_animalAnimator != null)
            {
                _animalAnimator.SetBool("IsIdle", true);
            }

            Debug.Log("動物が収穫される");
        } else
        {
            // 指定の間隔で牛乳を出す
            _timer += Time.deltaTime;
            if (_timer >= _interval)
            {
                Debug.Log("牛乳を出し終わる");
                _timer = 0f;
                Instantiate(_milk);
                StartCoroutine(ChangeAction());
                StartCoroutine(ChangeDirection());
                StartCoroutine(ChangeFood());
                _rigidbody.isKinematic = false;
                _animalAnimator.SetBool("IsIdle", false);
                _isHarvested = false;
                return true;

            }
            
        }
        return false;
    }

    public IEnumerator ChangeAction()
    {
        //ランダムに行動を切り替える
        _currentAction = (Animaltype)Random.Range(0, 3);
        switch (_currentAction)
        {
            case Animaltype.Idle:
                _iAnimalStateChage.Change(new IdleClass(_animalAnimator));
                break;
            case Animaltype.Walk:
                _iAnimalStateChage.Change(new WalkClass(_moveVector, _walkSpeed, _animalAnimator, _rigidbody));
                break;
            case Animaltype.Run:
                _iAnimalStateChage.Change(new RunClass(_moveVector, _runSpeed, _animalAnimator, _rigidbody));
                break;
        }
        //3秒から5秒のランダムな間隔で行動を切り替える yield=一時停止
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        //コルーチン終了
        StopCoroutine(ChangeAction());
        //再起処理
        StartCoroutine(ChangeAction());
    }
    public IEnumerator ChangeDirection()
    {
        //ランダムな間隔で行動を切り替える
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        //ランダムな方向の単位ベクトルを取得
        _moveVector = Random.insideUnitSphere;
        //上下方向は移動しない
        _moveVector.y = 0;
        //再起処理
        StartCoroutine(ChangeDirection());
    }

    public IEnumerator ChangeFood()
    {
        //動物がランダムで選択した食べ物を要求する
        _currentFood = (AnimalFoodtype)Random.Range(0, 3);
        Debug.Log("Change Food " + _currentFood);

        //ランダムな間隔で行動を切り替える
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        StartCoroutine(ChangeFood());

    }
    #endregion
}