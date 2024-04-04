// ---------------------------------------------------------  
// GameManagerClass.cs  
// ゲームを管理する
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using System.Collections.Generic;
using UniRx;
using UnityEngine;
/// <summary>
/// ゲームを管理する
/// </summary>
public class GameManagerClass : MonoBehaviour
{

    #region プロパティ

    public IReadOnlyReactiveProperty<float> TimeLimetMinutes => _timeLimetMinutes;
    public IReadOnlyReactiveProperty<float> TimeLimetSeconds => _timeLimetSeconds;
    public IReadOnlyReactiveProperty<int> TargetMonay => _targetMonay;
    public IReadOnlyReactiveProperty<int> MoneyPossession => _moneyPossession;
    public IReadOnlyReactiveProperty<int> ChainBonus => _chainBonus;

    #endregion
    #region 変数  

    [Header("スクリプタブルオブジェクト")]
    [SerializeField, Tooltip("ゲームマネジャーのデータ")]
    private GameManageData _gameManageData = default;
    [Header("スクリプト")]
    [SerializeField, Tooltip("提出された製品を知るために取得")]
    private Iteble _tableClass = default;

    /// <summary>
    /// 求める製品が入るリスト
    /// </summary>
    private ReactiveProperty<List<ProductState>> _productsStateList = new ReactiveProperty<List<ProductState>>(new List<ProductState> { }) ;
    /// <summary>
    /// 制限時間（分）
    /// </summary>
    private ReactiveProperty<float> _timeLimetMinutes = new ReactiveProperty<float>(default);
    /// <summary>
    /// 制限時間（秒）
    /// </summary>
    private ReactiveProperty<float> _timeLimetSeconds = new ReactiveProperty<float>(default);
    /// <summary>
    /// 目標金額
    /// </summary>
    private ReactiveProperty<int> _targetMonay = new ReactiveProperty<int>(default);
    /// <summary>
    /// 所持金
    /// </summary>
    private ReactiveProperty<int> _moneyPossession = new ReactiveProperty<int>(default);
    /// <summary>
    /// 連鎖ボーナス
    /// </summary>
    private ReactiveProperty<int> _chainBonus = new ReactiveProperty<int>(default);
    /// <summary>
    /// ゲームマネージャーのステート
    /// </summary>
    private GameManagerSutatus _gameManagerSutatus = GameManagerSutatus.Main;
    /// <summary>
    /// 累計連鎖ボーナス
    /// </summary>
    private int _sumChainBonus = default;
    /// <summary>
    /// 連鎖数を数える
    /// </summary>
    private int _chainCount = default;
    #endregion
    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {

        //スクリプタブルオブジェクトからのデータの読み込み
        _timeLimetMinutes.Value = _gameManageData.TimeLimetMinutes;
        _timeLimetSeconds.Value = _gameManageData.TimeLimetSeconds;
        _targetMonay.Value = _gameManageData.TargetMonay;
        //リストの定義
        //_productsStateList.Value = new List<ProductState> { };

     }

     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {

        //現在のステートで処理分岐
        switch (_gameManagerSutatus)
        {

            case GameManagerSutatus.Title:

                break;
            case GameManagerSutatus.Main:

                //求める製品を追加
                AddTargetProduct();
                //非同期で提出されているオブジェクトが変わった時に実行
                _tableClass.CollisionObj
                    .Subscribe
                    (

                        collisionObj =>
                        {

                            //中身がないとき
                            if (collisionObj == null)
                            {

                                return;

                            }
                            AddMonay(collisionObj);

                        }

                    ).AddTo(this);

                break;
            case GameManagerSutatus.Result:

                break;

        }

    }

     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {

        //現在のステートで処理分岐
        switch (_gameManagerSutatus)
        {

            case GameManagerSutatus.Title:

                break;
            case GameManagerSutatus.Main:

                //制限時間更新
                _timeLimetSeconds.Value -= Time.deltaTime;
                //秒が0になった時
                if (_timeLimetSeconds.Value <= 0)
                {

                    //分を更新
                    _timeLimetMinutes.Value -= 1;
                    //秒を初期化
                    _timeLimetSeconds.Value = 60;

                }

                break;
            case GameManagerSutatus.Result:

                //目標金額まで達したかを調べる
                ClearCheck();

                break;

        }
        //スペースを押した時
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //求める製品を追加
        //    AddTargetProduct();
        //    //リストの数を出力
        //    print(_productsStateList.Value.Count);
        //    foreach (ProductState product in _productsStateList.Value)
        //    {

        //        //リストの中を出力
        //        print(product);
            
        //    }
        
        //}
        
    }

    /// <summary>
    /// 農産物を置いたときに提出してほしい
    /// 農産物とあっていれば
    /// 所持金を増やす
    /// </summary>
    /// <param name="collisionObj">提出されたオブジェクト</param>
    private void AddMonay(GameObject collisionObj)
    {

        //現在求めている製品をリストから取得
        ProductState targetProduct = _productsStateList.Value[0];
        //求めている製品と提出されたオブジェクトがあった時
        if (targetProduct.ToString() == collisionObj.name)
        {

            //価格が入る
            int price = default;
            //提出されたオブジェクトにより処理分岐
            switch (collisionObj.name)
            {

                case "Egg":

                    //卵の価格にする
                    price = _gameManageData.EggPrice;

                    break;
                case "Milk":

                    //牛乳の価格にする
                    price = _gameManageData.MilkPrice;

                    break;
                case "Wool":

                    //ウールの価格にする
                    price = _gameManageData.WoolPrice;

                    break;

            }
            //比べた求めている製品を削除
            _productsStateList.Value.Remove(targetProduct);
            //求めている製品が１以下になった時
            if (_productsStateList.Value.Count <= 1)
            {

                //求めている製品の追加
                AddTargetProduct();
            
            }
            //提出されたオブジェクトの金額分を足す
            _moneyPossession.Value += price;
            //連鎖を数える
            ++_chainCount;
            ///今の金額段階を調べる
            int bonusStep = _chainCount / _gameManageData.UpBonusLine;
            //今の段階分を足す
            _chainBonus.Value += bonusStep;
            

        }
        //合わなかったとき
        else
        {

            //累計に足す
            _sumChainBonus = _chainBonus.Value;
            //初期化
            _chainCount = 0;
            _chainBonus.Value = 0;

            return;
        
        }

    
    }

    /// <summary>
    /// 所持金が目標の金額まで達したかを調べる
    /// </summary>
    private void ClearCheck()
    {

        //所持金が目標金額を超えた時
        if (_targetMonay.Value <= _moneyPossession.Value)
        {

            //リザルトにステートを変更
            _gameManagerSutatus = GameManagerSutatus.Result;

        }

    }

    /// <summary>
    /// ゲーム開始ボタンを押されたときにメインにステートを変更
    /// </summary>
    public void OnGameStart()
    {

        //メインにステートを変更
        _gameManagerSutatus = GameManagerSutatus.Main;
    
    }

    /// <summary>
    /// リザルト確認ボタンを押したときにタイトルにステートを変更
    /// </summary>
    private void OnResultFinsh()
    {

        //タイトルにステートを変更
        _gameManagerSutatus = GameManagerSutatus.Title;
    
    }

    /// <summary>
    /// 求める製品をリストに追加
    /// </summary>
    private void AddTargetProduct()
    {

        //enum型の要素数を取得
        int maxCount = ProductState.GetNames(typeof(ProductState)).Length;

        //追加量分ループ
        for (int i = 0; _gameManageData.AddProductValue > i; ++i)
        {
        
            //要素数内のランダムな値を取得
            int number = Random.Range(0, maxCount);
            //値に対応したステートを取得
            ProductState chooseProduct = (ProductState)number;
            //取得したステートをリストに格納
            _productsStateList.Value.Add(chooseProduct);

        }
       
    
    }
  
    #endregion

}
