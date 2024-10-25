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
    private PlayerEventHandler playerEventHandler;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        physicsCheckEventHandler = GetComponentInChildren<PhysicsCheckEventHandler>();
        playerEventHandler = GetComponentInChildren<PlayerEventHandler>();
    }

    private void OnEnable()
    {
        physicsCheckEventHandler.OnCharacterGroundChange += OnPlayerGroundChage;
        playerEventHandler.OnCharacterBeginJump += OnPlayerBeginJump;
        playerEventHandler.OnCharacterPressAttack += OnPlayerPressAttack;
    }
    private void OnDisable()
    {
        physicsCheckEventHandler.OnCharacterGroundChange -= OnPlayerGroundChage;
        playerEventHandler.OnCharacterBeginJump -= OnPlayerBeginJump;
        playerEventHandler.OnCharacterPressAttack -= OnPlayerPressAttack;
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
        animator.SetFloat(CharacterAnim.kCharacterAnimVelocityX, Mathf.Abs(velocityX));
        animator.SetFloat(CharacterAnim.kCharacterAnimVelocityY, velocityY);

    }
    #region 事件接收
    private void OnPlayerGroundChage(object sender, bool isOnGround)
    {
        animator.SetBool(CharacterAnim.kCharacterAnimIsOnGround, isOnGround);
    }

    private void OnPlayerBeginJump(object sender, bool isOnGround)
    {
        animator.SetBool(PlayerAnim.kPlayerAnimIsJump, true);
        animator.SetTrigger(PlayerAnim.kPlayerAnimJumpTrig);
    }

    private void OnPlayerPressAttack(object sender,bool isAttack)
    {
        animator.SetBool(PlayerAnim.kPlayerAnimIsAttack,isAttack);   
        animator.SetTrigger(PlayerAnim.kPlayerAnimAttackTrig);
    }

    #endregion

}
