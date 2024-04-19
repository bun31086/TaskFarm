// ---------------------------------------------------------  
// SceneMove.cs  
//   シーンの移動
// 作成日:  4/19
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの移動
/// </summary>
public class SceneMove : MonoBehaviour
{

    /// <summary>
    /// ゲーム開始ボタンを押されたときにメインシーンに移動
    /// </summary>
    public void OnGameStart()
    {

        // メインシーンに移動する
        SceneManager.LoadScene("MainScene");

    }

    /// <summary>
    /// リザルトで確認ボタンを押したときにタイトルシーンに移動
    /// </summary>
    public void OnResultFinsh()
    {

        // タイトルシーンに移動する
        SceneManager.LoadScene("TitleScene");

    }

    /// <summary>
    /// タイトルでゲーム終了ボタンが押されたときに終了
    /// </summary>
    public void OnGameEnd()
    {

        // ゲームを終了する
        Application.Quit();

    }

}
