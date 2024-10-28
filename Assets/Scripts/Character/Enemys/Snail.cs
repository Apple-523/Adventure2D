using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class SnailNomalState : EnemyState
{
    public override void OnEnterState()
    {
        enemy.currentSpeed = enemy.normalSpeed;

    }

    public override void OnExitState()
    {
    }
}

class SnailSpecialState : EnemyState
{
    public override void OnEnterState()
    {
        enemy.currentSpeed = enemy.specialSpeed;
    }

    public override void OnExitState()
    {
    }
}

public class Snail : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        InitParams();
        // 初始状态下默认为普通状态
        SwitchToAState(EnemyStateEnum.Normal);

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        physicsEvent.OnCharacterGroundChange += OnCharacterGroundChange;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        physicsEvent.OnCharacterGroundChange += OnCharacterGroundChange;
    }
   protected override void Update()
    {
        base.Update();
        CheckSpecialTime();
    }

    private void CheckSpecialTime()
    {
        if (currentState != specialState)
        {
            return;
        }
        currentSpecialTime -= Time.deltaTime;
        if (currentSpecialTime <= 0)
        {
            animator.SetBool(PlayerDection.kPlayerDetionIsClose, false);
            SwitchToAState(EnemyStateEnum.Normal);
        }
    }
    private void InitParams()
    {
        normalState = new SnailNomalState();
        normalState.enemy = this;

        specialState = new SnailSpecialState();
        specialState.enemy = this;
    }
    public override void OnEnemyStateChange()
    {
        //TODO: wmy 这里判断当前为特殊状态，需要
    }
    private void OnCharacterGroundChange(object sender, bool isOnGround)
    {
        if (!isOnGround)
        {
            if (!isWaiting)
            {
                currentWaitTime = waitTime;
                isWaiting = true;
            }
        }
    }

    public override void OnPlayerIsClose(object sender, bool isCloseToPlayer)
    {
        if (currentState != specialState && isCloseToPlayer)
        {
            base.OnPlayerIsClose(sender, isCloseToPlayer);
            currentSpecialTime = specialTime;
            animator.SetBool(PlayerDection.kPlayerDetionIsClose, isCloseToPlayer);
        }
    }
}
