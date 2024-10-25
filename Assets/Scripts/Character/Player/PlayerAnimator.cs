using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Player player;

    private Rigidbody2D rigidbody2d;

    private PhysicsCheckEventHandler physicsCheckEventHandler;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        physicsCheckEventHandler = GetComponentInChildren<PhysicsCheckEventHandler>();
    }

    private void OnEnable()
    {
        physicsCheckEventHandler.OnCharacterGroundChange += OnPlayerGroundChage;
        physicsCheckEventHandler.OnCharacterBeginJump += OnPlayerBeginJump;
    }
    private void OnDisable()
    {
        physicsCheckEventHandler.OnCharacterGroundChange -= OnPlayerGroundChage;
        physicsCheckEventHandler.OnCharacterBeginJump -= OnPlayerBeginJump;
    }



    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        SetAnimatorVelocity();
    }

    private void SetAnimatorVelocity()
    {
        float velocityX = rigidbody2d.velocity.x;
        float velocityY = rigidbody2d.velocity.y;
        animator.SetFloat(CharacterAnim.kPlayerAnimVelocityX, Mathf.Abs(velocityX));
        animator.SetFloat(CharacterAnim.kPlayerAnimVelocityY, velocityY);

    }
    #region 事件接收
    private void OnPlayerGroundChage(object sender, bool isOnGround)
    {
        animator.SetBool(CharacterAnim.kPlayerAnimIsOnGround, isOnGround);
    }

    private void OnPlayerBeginJump(object sender, bool isOnGround)
    {
        animator.SetBool(CharacterAnim.kPlayerAnimIsJump,true);
        animator.SetTrigger(CharacterAnim.kPlayerAnimJumpTrig);
    }

    #endregion

}
