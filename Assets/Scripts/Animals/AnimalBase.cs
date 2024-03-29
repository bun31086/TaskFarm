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
    private AnimalStateMachineClass _animalStateMachineClass = new AnimalStateMachineClass();
    /// <summary>
    /// 移動先確認
    /// </summary>
    private MoveCheckClass<Transform> _moveCheckClass = default;

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

    public Vector3 _moveVec = default;
    #region プロパティ  
    #endregion

    #region メソッド  
    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
    {

    }
    /// <summary>  
    /// 動物が待機する
    /// </summary>  
    private void Start()
    {
        //vector3の計算をする
        Vector3 moveVec = new Vector3();
        //コンポーネント取得
        Animator animator = this.GetComponent<Animator>();
        CharacterController characterController = this.GetComponent<CharacterController>();
        //以下クラスにコンポーネントを定義
        _animalStateMachineClass.Change(new RunClass(moveVec,animator,characterController));

        _animalStateMachineClass.Change(new WalkClass(moveVec, animator, characterController));

        _animalStateMachineClass.Change(new IdleClass(moveVec, animator, characterController));
        //コンストラクタにtransformをインスタンスを設定してインスタンス化(生成)
        _moveCheckClass = new MoveCheckClass<Transform>(this.transform);
    }

    /// <summary>  
    /// 動物が歩く
    /// </summary>  
    private void Update()
    {
       
    }
    /// <summary>
    /// 餌を食べる
    /// </summary>
    public void EatBait()
    {
        //鶏が餌を食べる状態
        if (!_isEating)
        {
            _isEating = true;
            _eatTimer = 0f;
            Debug.Log("鶏が餌を食べ始める");
        }

        // 指定時間経過したら
        _eatTimer += Time.deltaTime;
        if (_eatTimer >= _eatDuration)
        {
            _isEating = false;
            Debug.Log("鶏が餌を食べ終わる");
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
            Debug.Log("鶏が収穫される");
        }
    }
    #endregion
}