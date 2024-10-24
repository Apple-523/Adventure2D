using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PhysicsCheck : MonoBehaviour
{
    [Header("地面相关参数")]
    [SerializeField]// 允许在Inspector中显示
    public bool isOnGround;
    /// <summary>
    /// 地面检测
    /// </summary>
    public Vector2 groundOffset;
    /// <summary>
    /// 地面检测半径
    /// </summary>
    public float groundRadius;

    private PhysicsCheckEventHandler eventHandler;

    public LayerMask groundLayer;

    private void Awake()
    {
        eventHandler = GetComponentInChildren<PhysicsCheckEventHandler>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        // 检测是否在地面

        bool onGround = Physics2D.OverlapCircle((Vector2)transform.position + groundOffset * transform.localScale, groundRadius, groundLayer);
        if (isOnGround != onGround)
        {
            isOnGround = onGround;
            eventHandler.PlayerGroundChange(onGround);
        }
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + groundOffset, groundRadius);
    }

}
