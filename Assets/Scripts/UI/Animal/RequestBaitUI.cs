// ---------------------------------------------------------  
// RequestBaitUI.cs  
// 動物の要求餌を表示
// 作成日:  4/18
// 作成者:  竹村綾人
// ---------------------------------------------------------  
using UnityEngine;
/// <summary>
/// 動物の要求餌を表示
/// </summary>
public class RequestBaitUI : MonoBehaviour
{

    #region 変数 

    private SpriteRenderer _spriteRenderer = default;
    [SerializeField]
    private Sprite _redBait = default;
    [SerializeField]
    private Sprite _blueBait = default;
    [SerializeField]
    private Sprite _greenBait = default;
    [SerializeField]
    private Sprite _bucket = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Awake()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }


    public void SpriteChange(TakeType takeType)
    {
        switch (takeType)
        {
            case TakeType.RedBait:
                _spriteRenderer.sprite = _redBait;
                break;
            case TakeType.BlueBait:
                _spriteRenderer.sprite = _blueBait;
                break;
            case TakeType.GreenBait:
                _spriteRenderer.sprite = _greenBait;
                break;
            case TakeType.Bucket:
                _spriteRenderer.sprite = _bucket;
                break;
        }
    }

    #endregion
}
