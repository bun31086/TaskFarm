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
    private WalkClass _walkClass = default;
    private Vector3 _moveVector = default;

    private const string TAG_ITEM = "Item";
    private const string TAG_STAGE = "Stage";

    // 餌を食べているかどうかのフラグ
    private bool _isEating = false;
    // 餌を食べる時間計測用タイマー
    private float _eatTimer = 0f;
    // 餌を食べる時間（仮の値）
    private float _eatDuration = 5f;
    // 動物の歩く速度
    private float _walkSpeed = 2f;
    // 動物の走る速度
    private float _runSpeed = 4f;
    // 収穫されたかどうかのフラグ
    private bool _isHarvested = false;
    private bool _garbage = false;
    #endregion

    #region メソッド  
    /// <summary>  
    /// 動物が待機する
    /// </summary>  
    private void Start()
    {
        // コンポーネント取得
        // コンストラクタにtransformをインスタンスを設定してインスタンス化(生成)
        _iMoveCheck = new ForwardCheckClass(this.transform);
        // コルーチン開始
        StartCoroutine(ChangeAction());
        StartCoroutine(ChangeDirection());
        StartCoroutine(ChangeFood());
        // ランダムな方向の単位ベクトルを取得
        _moveVector = Random.onUnitSphere;
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
        //更新処理実行
        _iAnimalStateChage.Execute();
    }

    /// <summary>
    /// 餌を食べる
    /// </summary>
    public void EatBait()
    {
        // 動物が餌を食べる状態
        if (!_isEating)
        {
            _isEating = true;
            _eatTimer = 0f;
            Debug.Log("動物が餌を食べ始める");
        }

        // 指定時間経過したら
        _eatTimer += Time.deltaTime;
        if (_eatTimer >= _eatDuration)
        {
            _isEating = false;
            Debug.Log("動物が餌を食べ終わる");

            _garbage = true;
            Debug.Log("ゴミを出した");
        }
    }

    /// <summary>
    /// 収穫する
    /// </summary>
    public void Harvest()
    {
        // 収穫される
        if (!_isHarvested)
        {
            _isHarvested = true;
            Debug.Log("動物が収穫される");
        }
    }

    public IEnumerator ChangeAction()
    {
        while (true)
        {
            // ランダムに行動を切り替える
            _currentAction = (Animaltype)Random.Range(0, 3);
            Debug.Log("Current Action: " + _currentAction);
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
            // 3秒から5秒のランダムな間隔で行動を切り替える yield=一時停止
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }
    public IEnumerator ChangeDirection()
    {
        while (true)
        {
            // ランダムな方向の単位ベクトルを取得
            _moveVector = Random.insideUnitSphere;
            // 上下方向は移動しない
            _moveVector.y = 0;
            // ランダムな間隔で行動を切り替える
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    public IEnumerator ChangeFood()
    {
        //動物がランダムで選択した食べ物を要求する
        while (true)
        {
            _currentFood = (AnimalFoodtype)Random.Range(0, 3);
            Debug.Log("Change Food " + _currentAction);

            //ランダムな間隔で行動を切り替える
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }

    }
    #endregion
}