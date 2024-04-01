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
    /// ステータス変更
    /// </summary>
    protected AnimalStateMachineClass _animalStateMachineClass = new AnimalStateMachineClass();
    /// <summary>
    /// 移動先確認
    /// </summary>
    private MoveCheckClass _moveCheckClass = default;

    private Animator _animalAnimator = default;

    private CharacterController _characterController = default;

    //Idleを初期動作にする
    public Animaltype _currentAction = Animaltype.Idle;

    public Vector3 _moveVec = default;
    // 餌を食べる関連の変数 
    // 餌を食べているかどうかのフラグ
    private bool _isEating = false;
    // 餌を食べる時間計測用タイマー
    private float _eatTimer = 0f;
    // 餌を食べる時間（仮の値）
    private float _eatDuration = 5f;
    // 収穫関連の変数
    // 収穫されたかどうかのフラグ
    private bool _isHarvested = false;
    #endregion

    #region メソッド  
    /// <summary>  
    /// 動物が待機する
    /// </summary>  
    private void Start()
    {
        //vector3の計算をする
        //Vector3 moveVec = new Vector3();
        //コンポーネント取得
        _animalAnimator  = this.GetComponent<Animator>();
        _characterController = this.GetComponent<CharacterController>();
        //クラスにコンポーネントを定義
        _animalStateMachineClass.Change(new IdleClass(_animalAnimator));
        //コンストラクタにtransformをインスタンスを設定してインスタンス化(生成)
        _moveCheckClass = new MoveCheckClass(this.transform);
        //コルーチン開始
        StartCoroutine(ChangeAction());

    }

    /// <summary>  
    /// 動物が歩く
    /// </summary>  
    //protected void Update()
    //{
    //    _animalStateMachineClass.Update();
    //}

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
        Debug.Log("コルーチン");
        while (true)
        {
            // ランダムに行動を切り替える
            _currentAction = (Animaltype)Random.Range(0, 3);
            Debug.Log("Current Action: " + _currentAction);
            switch (_currentAction)
            {
                case Animaltype.Idle:
                    _animalStateMachineClass.Change(new IdleClass(_animalAnimator));
                    break;
                case Animaltype.waik:
                    _animalStateMachineClass.Change(new WalkClass(_moveVec,_animalAnimator,_characterController));
                    break;
                case Animaltype.run:
                    _animalStateMachineClass.Change(new RunClass(_moveVec, _animalAnimator, _characterController));
                    break;
            }

            // 3秒から5秒のランダムな間隔で行動を切り替える yield=一時停止
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            Debug.Log("処理終わり");
        }
    }
    #endregion
}