// ---------------------------------------------------------  
// PlayerManagerClass.cs  
// プレイヤー管理クラス
// 作成日:  3/27~
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// プレイヤー管理クラス
/// </summary>
[RequireComponent(typeof(DrowRayDebug))]
public class PlayerManagerClass : MonoBehaviour
{

    #region 変数  

    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("プレイヤーのデータ")]
    private PlayerManagerData _playerData = default;
    [Header("キャラクターコントローラー")]
    [SerializeField, Tooltip("プレイヤーのキャラクターコントローラー")]
    private Rigidbody _playerRigidbody = default;
    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("プレイヤーのアニメーター")]
    private Animator _playerAnimator = default;
    [Header("インプットシステム")]
    [SerializeField, Tooltip("手作業のボタン入力")]
    private InputActionReference _onManual = default;
    [SerializeField, Tooltip("移動の入力ボタン")]
    private InputActionReference _onWalk = default;

    /// <summary>
    /// プレイヤーステートを変更するインターフェースのインスタンス
    /// </summary>
    private IstateChenge _iStateChengeInterFace = default;
    /// <summary>
    /// 移動を確認するインターフェースのインスタンスが入る
    /// </summary>
    private IForwardCheck _iMoveCheckInterFace = default;
    /// <summary>
    /// 持っているオブジェクト
    /// </summary>
    private GameObject _holdObj = null;
    /// <summary>
    /// インプットシステム本体
    /// </summary>
    private PlayerInput _playerInput = default;
    /// <summary>
    /// 物体の存在と種類を検知
    /// </summary>
    private RaycastHit[] _hits = default;
    #region タグや動物などの名前をコンストで設定
    /*
     * タグ名
     */
    /// <summary>
    /// アイテムについてるタグ
    /// </summary>
    private const string TAG_ITEM = "Item";
    /// <summary>
    /// 川についているタグ
    /// </summary>
    private const string TAG_RIVER = "River";
    /// <summary>
    /// ゲート（門）についているタグ
    /// </summary>
    private const string TAG_GATE = "Gate";
    /// <summary>
    /// 動物についているタグ
    /// </summary>
    private const string TAG_ANIMAL = "Animal";
    /// <summary>
    /// ステージについているタグ
    /// </summary>
    private const string TAG_STAGE = "Stage";
    /// <summary>
    /// ゴミについているタグ
    /// </summary>
    private const string TAG_RUBBISH = "rubbish";
    /*
     * アイテムの名前
     */
    /// <summary>
    /// 空のバケツについてる名前
    /// </summary>
    private const string NAME_BUCKET_ENPTY = "Bucket_Enpty";
    /// <summary>
    /// 水入りバケツについている名前
    /// </summary>
    private const string NAME_BUCKET_WATER = "Bucket_Water";
    /// <summary>
    /// ハサミについている名前
    /// </summary>
    private const string NAME_SCISSORS = "Scissors";
    /// <summary>
    /// 箒についている名前
    /// </summary>
    private const string NAME_BROOM = "Broom";
    /// <summary>
    /// 餌についている名前
    /// </summary>
    private const string NAME_FEED = "Feed";
    /*
     * 動物の名前
     */
    /// <summary>
    /// 牛についているなまえ
    /// </summary>
    private const string NAME_CAW = "Caw";
    /// <summary>
    /// 鳥についている名前
    /// </summary>
    private const string NAME_SHEEP = "Sheep";
    /// <summary>
    /// 鳥についている名前
    /// </summary>
    private const string NAME_CHICKEN = "Chicken";
    #endregion
    /// <summary>
    /// プレイヤーの速さ
    /// </summary>
    private float _walkSpeed = default;

    #endregion
    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {

        //生成時に初期ステートをコンストラクタに設定
        _iStateChengeInterFace = new PlayerStateMachineClass(new None(), new IdleClass(_playerAnimator));
        //自分のとトランスフォームをコンストラクタに渡し生成
        _iMoveCheckInterFace = new ForwardCheckClass(this.transform);
        /*
         * スクリプタブルオブジェクトからデータの読み込み
         */
        //スピード読み込み
        _walkSpeed = _playerData.Speed;
        //インプットシステムの取得
        _playerInput = new PlayerInput();
        //インプットシステムを有効化
        _playerInput.Enable();
        //対応した関数の登録
        _onManual.action.started += OnManualWork;
        _onManual.action.performed += OnManualWork;
        _onManual.action.canceled += OnManualWork;
        _onWalk.action.started += OnMove;
        _onWalk.action.performed += OnMove;
        _onWalk.action.canceled += OnMove;

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {

        //ステートマシンの更新処理
        _iStateChengeInterFace.Update();
        //移動先確認
        _hits = _iMoveCheckInterFace.Check();

    }


    /// <summary>
    /// 移動の値を取得
    /// </summary>
    /// <param name="context">入力値</param>
    private void OnMove(InputAction.CallbackContext context)
    {

        //入力を終えた時
        if (context.canceled)
        {

            //止まるステート変更
            _iStateChengeInterFace.ChangeMoveState(new IdleClass(_playerAnimator));
            return;

        }
        //入力方向を取得
        Vector3 direction = context.ReadValue<Vector2>();
        /*
         * ZのあたいがYの値となってしまっているため修正
         */
        direction.z = direction.y;
        direction.y = 0;
        //歩くステートに変更
        _iStateChengeInterFace.ChangeMoveState(new WalkClass(direction,_walkSpeed,_playerAnimator,_playerRigidbody));

    }

    /// <summary>
    /// 手作業の実行判定を取得し
    /// 状況に応じた処理を実行
    /// </summary>
    /// /// <param name="context">入力値</param>
    private void OnManualWork(InputAction.CallbackContext context)
    {

        //ボタンを押し続けるまたは離したとき
        if (!context.started)
        {

            return;

        }
        //持っているオブジェクトがあるとき
        if (_holdObj != null)
        {

            //オブジェクトを持っているときの処理
            HoldProcess();

        }
        //持っているオブジェクトがないとき
        else
        {

            //オブジェクトを持ってないときの処理
            NotHoldProcess();

        }

    }

    /// <summary>
    /// オブジェクトを持っているときの処理
    /// </summary>
    private void HoldProcess()
    {

        //一番近いアイテムとステージ以外のオブジェクトが入る
        GameObject nearObj = default;
        //オブジェクトとの一番近い距離が入る(初期値に探索範囲外の値を設定)
        float nearDistans = 100;
        //取得したオブジェクトを見ていくループ
        foreach (RaycastHit hit in _hits)
        {

            //当たっているものがItemとStageタグ以外の場合
            if (hit.collider.CompareTag(TAG_ITEM)  || hit.collider.CompareTag(TAG_STAGE))
            {

                continue;

            }
            //動物オブジェクトとの距離取得
            float distance = Vector3.Distance(this.transform.position, hit.collider.transform.position);
            //現在の距離が過去の最短距離より短いとき
            if (distance < nearDistans)
            {

                //一番近いオブジェクトとの距離と一番近い動物を更新
                nearDistans = distance;
                nearObj = hit.collider.gameObject;

            }

        }
        //オブジェクトがある場合
        if (nearObj != null)
        {

            print(_holdObj+"を使うPlayerの使うアクションをする");
            //モノを持った時の行動処理
            ManualAction(nearObj);

        }
        //オブジェクトがない場合
        else
        {


            print(_holdObj + "を置く");
            //置くステートに更新
            _iStateChengeInterFace.ChangeBehaviorState(new PutClass(_holdObj.transform, _playerAnimator));
            //持っているオブジェクトを空にする
            _holdObj = null;

        }

    }

    /// <summary>
    /// モノを持った時の行動処理
    /// </summary>
    private void ManualAction(GameObject nearAnimalObj)
    {

        //持っているオブジェクトのタグで処理分岐
        switch (_holdObj.name)
        {

            case NAME_BUCKET_ENPTY:

                //川のタグ以外がついているとき
                if (!(nearAnimalObj.CompareTag(TAG_RIVER)))
                {

                    return;

                }
                print("川にアクション");
                //組むステートに変更
                _iStateChengeInterFace.ChangeBehaviorState(new PumbClass(_holdObj, _playerAnimator));

                break;
            case NAME_BROOM:

                //ゴミのタグ以外がついているとき
                if (!nearAnimalObj.CompareTag(TAG_RUBBISH))
                {

                    return;

                }
                print("ゴミにアクション");
                //掃除ステートに変更
                _iStateChengeInterFace.ChangeBehaviorState(new CleanClass(nearAnimalObj, _playerAnimator));

                break;
            case NAME_FEED:

                //動物のタグ以外がついているとき
                if (!nearAnimalObj.CompareTag(TAG_ANIMAL))
                {

                    return;

                }
                print("動物にアクション");
                //餌をやるステートに変更
                _iStateChengeInterFace.ChangeBehaviorState(new TakeFeedClass(_holdObj, nearAnimalObj.transform, _playerAnimator));

                break;
            case NAME_BUCKET_WATER:

                //近くにある動物オブジェクトが牛の以外場合
                if (!(nearAnimalObj.name.CompareTo(NAME_CAW) == 0))
                {

                    return;
                
                }
                print("牛にアクション");
                //搾乳ステートに変更
                _iStateChengeInterFace.ChangeBehaviorState(new SqeezeClass(nearAnimalObj.transform, _playerAnimator));

                break;

            case NAME_SCISSORS:

                //近くにある動物オブジェクトが羊の以外場合
                if (!(nearAnimalObj.name.CompareTo(NAME_SHEEP) == 0))
                {

                    return;

                }
                print("羊にアクション");
                //毛を刈るステートに変更
                _iStateChengeInterFace.ChangeBehaviorState(new CutClass(nearAnimalObj.transform, _playerAnimator));

                break;

        }

    }

    /// <summary>
    /// オブジェクトを持ってないときの処理
    /// </summary>
    private void NotHoldProcess()
    {

        //一番近いアイテムオブジェクトが入る
        GameObject nearItemObj = default;
        //オブジェクトとの一番近い距離が入る(初期値に探索範囲外の値を設定)
        float nearItemDist = 100;
        //取得したオブジェクトを見ていくループ
        foreach (RaycastHit hit in _hits)
        {

            //当たっているオブジェクトが扉の場合
            if (hit.collider.CompareTag(TAG_GATE))
            {

                //ドアの開閉
                _iStateChengeInterFace.ChangeBehaviorState(new OpenClass(hit.transform, _playerAnimator));
                //登録したアイテム削除
                nearItemObj = null;
                break;

            }
            //当たっているオブジェクトがItemタグではない場合
            if (!hit.collider.CompareTag(TAG_ITEM))
            {

                continue;

            }
            //オブジェクトとの距離取得
            float dist = Vector3.Distance(this.transform.position, hit.collider.transform.position);
            //現在の距離が過去の最短距離より短いとき
            if (dist < nearItemDist)
            {

                //一番近いオブジェクトとの距離と一番近いアイテムを更新
                nearItemDist = dist;
                nearItemObj = hit.collider.gameObject;

            }

        }
        //中身がある場合
        if (nearItemObj != null)
        {

            //持っているオブジェクト取得
            _holdObj = nearItemObj;
            //持つステートに変更
            _iStateChengeInterFace.ChangeBehaviorState(new HoldClass(this.transform, nearItemObj.transform, _playerAnimator));

        }

    }

    #endregion

}
