// ---------------------------------------------------------  
// TakemuraTestClass.cs  
// 処理動作用（竹村）
// 作成日:  3/29
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class TakemuraTestClass : MonoBehaviour
{

    #region 変数  

    private CleanClass _clean = default;
    private CutClass _cut = default;
    private SqeezeClass _sqeeze = default;
    private HoldClass _hold = default;
    private PutClass _put = default;
    private TakeFeedClass _takeFood = default;

    [SerializeField] private Transform _nearObjectTransform = default;
    [SerializeField] private Animator _playerAnimator = default;
    [SerializeField] private Transform _playerTransform = default;
    [SerializeField] private Transform _animalObjTransform = default;
    [SerializeField] private Transform _holdObjTransform = default;
    [SerializeField] private GameObject _holdObject = default;
    [SerializeField] private GameObject _nearObject = default;
    [SerializeField] private GameObject _gateObject = default;

    private IOpenClose _iOpenClose = default;

    #endregion

    #region プロパティ  

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {
        //インスタンス生成
        _hold = new HoldClass(_playerTransform,_nearObjectTransform,_playerAnimator);
        _put = new PutClass(_holdObjTransform,_playerAnimator);
        _cut = new CutClass(_animalObjTransform,_playerAnimator);
        _sqeeze = new SqeezeClass(_animalObjTransform,_playerAnimator);
        _takeFood = new TakeFeedClass(_holdObject, _animalObjTransform,_playerAnimator);
        _clean = new CleanClass(_nearObject, _playerAnimator);
     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {
        _iOpenClose = _gateObject.GetComponent<IOpenClose>();
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
        //
        if (Input.GetKeyDown(KeyCode.A))
        {
            _hold.Enter();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _put.Enter();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _cut.Enter();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _sqeeze.Enter();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            _takeFood.Enter();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            _takeFood.Enter();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            _clean.Exit();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            _iOpenClose.Open();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            _iOpenClose.Close();
        }
     }
  
    #endregion
}
