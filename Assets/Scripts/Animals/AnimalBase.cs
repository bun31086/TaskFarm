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
    private MoveCheckClass _moveCheckClass = default;

    private Animator _animalAnimator = default;

    private CharacterController _characterController = default;
    //インターフェース依存関係
    public IAnimalStateChage _iAnimalStateChage = new AnimalStateMachineClass { };

    //Idleを初期動作にする
    public Animaltype _currentAction = Animaltype.Idle;

    public Vector3 _moveVector = default;
    // 餌を食べる関連の変数 
    // 餌を食べているかどうかのフラグ
    private bool _isEating = false;
    // 餌を食べる時間計測用タイマー
    private float _eatTimer = 0f;
    // 餌を食べる時間（仮の値）
    private float _eatDuration = 5f;
    //動物の歩く速度
    public float _walkSpeed = default;
    //動物の走る速度
    public float _runSpeed = default;
    // 収穫関連の変数
    // 収穫されたかどうかのフラグ
    private bool _isHarvested = false;

    //private WalkClass _walkClass;

    //private IMoveState _currentState = default;
    #endregion

    #region メソッド  
    /// <summary>  
    /// 動物が待機する
    /// </summary>  
    private void Start()
    {
        //コンポーネント取得
        _animalAnimator = this.GetComponent<Animator>();
        _characterController = this.GetComponent<CharacterController>();
        //クラスにコンポーネントを定義
        //_animalStateMachineClass.Change(new IdleClass(_animalAnimator));
        //コンストラクタにtransformをインスタンスを設定してインスタンス化(生成)
        _moveCheckClass = new MoveCheckClass(this.transform);
        //コルーチン開始
        StartCoroutine(ChangeAction());
        StartCoroutine(ChangeDirection());

        // ランダムな方向の単位ベクトルを取得
        Vector3 randomDirection = Random.onUnitSphere;
    }
    /// <summary>  
    /// 動物が歩く
    /// </summary>  
    //public void Update()
    //{
    //    _currentState.Execute();
    //}

    public void Change(IMoveState nextState)
    {

    }
    /// <summary>
    /// 餌を食べる
    /// </summary>
    public void EatBait()
    {
        //動物が餌を食べる状態
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
        // Debug.Log("コルーチン");
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
                    _iAnimalStateChage.Change(new WalkClass(_moveVector, _animalAnimator, _characterController));
                    break;
                case Animaltype.Run:
                    _iAnimalStateChage.Change(new RunClass(_moveVector, _animalAnimator, _characterController));
                    break;
                //    case Animaltype.Idle:
                //        _animalStateMachineClass.Change(new IdleClass(_animalAnimator));
                //        break;
                //    case Animaltype.Walk:
                //        _animalStateMachineClass.Change(new WalkClass(_moveVector, _animalAnimator, _characterController));
                //        break;
                //    case Animaltype.Run:
                //        _animalStateMachineClass.Change(new RunClass(_moveVector, _animalAnimator, _characterController));
                //        break;
            }
            // 3秒から5秒のランダムな間隔で行動を切り替える yield=一時停止
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            Debug.Log("処理終わり");
        }
    }
    public IEnumerator ChangeDirection()
    {
        while (true)
        {
            // ランダムな方向の単位ベクトルを取得
            Vector3 randomDirection = Random.insideUnitSphere;
            // 上下方向は移動しない
            randomDirection.y = 0;

            // ランダムな方向に向かって移動
            _characterController.Move(randomDirection);

            // ランダムな間隔で行動を切り替える
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            Debug.Log("Random Direction: " + randomDirection);
        }
    }
    #endregion
}



//public class RandomDirectionMovement : MonoBehaviour
//{
//    public float moveSpeed = 3.0f;
//    private MovementController _movementController;

//    private void Start()
//    {
//        _movementController = new MovementController(transform, moveSpeed);
//        StartCoroutine(RandomMovement());
//    }

//    private IEnumerator RandomMovement()
//    {
//        _movementController.Enter();

//        while (true)
//        {
//            _movementController.Execute();

//            // ランダムな秒数待機
//            yield return new WaitForSeconds(Random.Range(3f, 5f));
//        }

//        _movementController.Exit();
//    }
//}
