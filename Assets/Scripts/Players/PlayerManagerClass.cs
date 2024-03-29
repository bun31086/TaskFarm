// ---------------------------------------------------------  
// PlayerManagerClass.cs  
// プレイヤー管理クラス
// 作成日:  3/27~
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
[RequireComponent(typeof(DrowRayDebug))]
public class PlayerManagerClass : MonoBehaviour
{

    #region 変数  

    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("プレイヤーのデータ")]
    private PlayerDataClass _playerData = default;
    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("プレイヤーのアニメーター")]
    private Animator _playerAnimator = default;
    /// <summary>
    /// プレイヤーステートを変更するクラスのインスタンス
    /// </summary>
    private PlayerStateMachineClass _playerStateMachine = new PlayerStateMachineClass();
    /// <summary>
    /// 移動を確認するクラスのインスタンス
    /// </summary>
    private MoveCheckClass<Transform> _moveCheck = default;
    /// <summary>
    /// 物体の存在と種類を検知
    /// </summary>
    private RaycastHit[] _hits = default;
    /// <summary>
    /// 持っているオブジェクト
    /// </summary>
    private GameObject _holdObj = default;
    #endregion

    #region メソッド  

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {

        //自分のインスタンスをコンストラクタに渡し生成
        _moveCheck = new MoveCheckClass<Transform>(this.transform);

    }

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {

        //初期ステータスに変更
        _playerStateMachine.ChangeMoveState(new IdleClass());

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {

        //ステートマシンの更新処理
        _playerStateMachine.Update();
        //移動先確認
        _hits = _moveCheck.Check();

    }

    /// <summary>
    /// 移動の値を取得
    /// </summary>
    /// <param name="context">入力値</param>
    public void OnMove(InputAction.CallbackContext context)
    {

        //ボタンを押し続けるまたは離したとき
        if (!context.started)
        {

            return;

        }

        //入力方向を取得
        Vector3 dire = context.ReadValue<Vector2>();
        /*
         * ZのあたいがYの値となってしまっているため修正
         */
        dire.z = dire.y;
        dire.y = 0;
        //ステート変更
        _playerStateMachine.ChangeMoveState(new WalkClass(dire));

    }

    /// <summary>
    /// 持ち置きの実行判定を取得
    /// </summary>
    /// /// <param name="context">入力値</param>
    public void OnMmanualWork(InputAction.CallbackContext context)
    {

        //ボタンを押し続けるまたは離したとき
        if (!context.started)
        {

            return;

        }
        //範囲内に何もない時
        if (_hits.Length <= 0)
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

        //一番近い動物オブジェクトが入る
        GameObject nearAnimalObj = default;
        //オブジェクトとの一番近い距離が入る(初期値に探索範囲外の値を設定)
        float nearItemDist = 100;
        //取得したオブジェクトを見ていくループ
        foreach (RaycastHit hit in _hits)
        {

            //当たっているものがItemタグの場合
            if (hit.collider.CompareTag("Item"))
            {

                continue;

            }
            //オブジェクトとの距離取得
            float dist = Vector3.Distance(this.transform.position, hit.collider.transform.position);
            //現在の距離が過去の最短距離より短いとき
            if (dist < nearItemDist)
            {

                //一番近いオブジェクトとの距離と一番近い動物を更新
                nearItemDist = dist;
                nearAnimalObj = hit.collider.gameObject;

            }

        }
        //オブジェクトがある場合
        if (nearAnimalObj != null)
        {

            //モノを持った時の行動処理
            Action(nearAnimalObj);

        }
        //オブジェクトがない場合
        else
        {

            //置くステートに更新
            _playerStateMachine.ChangeBehaviorState(new PutClass(nearAnimalObj.transform, _playerAnimator));

        }

    }

    /// <summary>
    /// モノを持った時の行動処理
    /// </summary>
    private void Action(GameObject nearAnimalObj)
    {

        //持っているオブジェクトのタグで処理分岐
        switch (_holdObj.tag)
        {

            case "Bucket":

                //搾乳ステートに変更
                _playerStateMachine.ChangeBehaviorState(new SqeezeClass(nearAnimalObj.transform,_playerAnimator));

                break;

            case "Scissors":

                //毛を刈るステートに変更
                _playerStateMachine.ChangeBehaviorState(new CutClass(nearAnimalObj.transform, _playerAnimator));

                break;

            case "Broom":

                //掃除ステートに変更
                _playerStateMachine.ChangeBehaviorState(new CleanClass(nearAnimalObj,_playerAnimator));

                break;

            case "Feed":

                //餌をやるステートに変更
                _playerStateMachine.ChangeBehaviorState(new TakeFeedClass(_holdObj,nearAnimalObj.transform,_playerAnimator));

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
            if (hit.collider.CompareTag("Gate"))
            {
            
                //ドアの開閉
                _playerStateMachine.ChangeBehaviorState(new OpenClass());
            
            }
            //当たっているオブジェクトがItemタグではない場合
            if (!hit.collider.CompareTag("Item"))
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
            _playerStateMachine.ChangeBehaviorState(new HoldClass(this.transform,nearItemObj.transform, _playerAnimator));

        }

    }



    #endregion
}
