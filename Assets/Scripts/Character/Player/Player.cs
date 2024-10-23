using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("移动速度")]
    public float normalSpeed;
    public float runSpeed;

    /// <summary>
    /// 输入系统
    /// </summary>
    private PlayerInputSystem playerInputSytem;
    /// <summary>
    /// 输入的方向
    /// </summary>
    private Vector2 inputDirection;

    private void Awake()
    {
        playerInputSytem = new PlayerInputSystem();
    }
    private void OnEnable()
    {
        playerInputSytem.Enable();
    }

    private void OnDisable()
    {
        playerInputSytem.Disable();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        inputDirection = playerInputSytem.Player.Move.ReadValue<Vector2>();
        //TODO: wmy 走和跑的转换 在键盘按下shift时，进行强制走路
        Vector2 position = this.transform.position;
        position += inputDirection * normalSpeed * Time.deltaTime;
        transform.position = position;
        Vector3 localScale = transform.localScale;
        if (inputDirection.x > 0)
        {
            localScale.x = 1;
        }
        if (inputDirection.x < 0)
        {
            localScale.x = -1;
        }
        transform.localScale = localScale;
    }
}
