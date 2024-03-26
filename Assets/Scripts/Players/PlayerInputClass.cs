// ---------------------------------------------------------  
// PlayerInputClass.cs  
// プレイヤーの入力受け取り
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
/// <summary>
/// プレイヤーの入力受け取り
/// </summary>
public class PlayerInputClass : MonoBehaviour
{
    public float _horizontalInput;

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
    }
}
