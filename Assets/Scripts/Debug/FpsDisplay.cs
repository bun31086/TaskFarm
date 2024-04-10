// ---------------------------------------------------------  
// FpsDisplay.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class FpsDisplay : MonoBehaviour
{

    #region 変数  

    [Header("テキスト")]
    [SerializeField, Tooltip("Fpsを表示するためのテキスト")]
    private Text _fpsText = default;

    #endregion

    /// <summary>
    /// FPSを表示を更新するまでの時間
    /// </summary>
    private const float UPDATA_TIME = 1f;


    #region メソッド  

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {

        //1フレームに対しての時間
        float fps = 1f / Time.deltaTime;
        //Fps表示
        _fpsText.text = "FPS : " + Mathf.Round(fps);
        //コルーチン開始
        StartCoroutine(Display());

    }

    IEnumerator Display()
    {

        //10秒待つ
        yield return new WaitForSeconds(UPDATA_TIME);
        //1フレームに対しての時間
        float fps = 1f / Time.deltaTime;
        //Fps表示
        _fpsText.text = "FPS : " + Mathf.Round(fps);
        //再起呼び出し
        StartCoroutine(Display());

    }
  
    #endregion

}
