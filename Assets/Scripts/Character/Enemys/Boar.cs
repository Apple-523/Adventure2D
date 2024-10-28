using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BoarNormalState : EnemyState
{
    public override void OnEnterState()
    {
        // Debug.Log("当前状态为普通状态");
        enemy.currentSpeed = enemy.normalSpeed;
        // enemy.isWalk = true;
        // enemy.isRun = false;

    }

    public override void OnExitState()
    {
        // Debug.Log("退出普通状态");
    }
}

class BoarSpecialState : EnemyState
{
    public override void OnEnterState()
    {
        // Debug.Log("当前状态为特殊状态");
        enemy.currentSpeed = enemy.specialSpeed;
        // enemy.isWalk = false;
        // enemy.isRun = true;
    }

    public override void OnExitState()
    {
        // Debug.Log("退出特殊状态");
    }
}

public class Boar : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        InitParams();
        // 初始状态下默认为普通状态
        SwitchToAState(EnemyStateEnum.Normal);

    }

    private void InitParams()
    {
        normalState = new BoarNormalState();
        normalState.enemy = this;

        specialState = new BoarSpecialState();
        specialState.enemy = this;
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
            SwitchToAState(EnemyStateEnum.Normal);
        }
    }



    protected override void FixedUpdate()
    {
        if (!isWaiting)
        {
            base.FixedUpdate();
        }

    }

    protected override void onCharacterByWall(object sender, EventBOOLArgs e)
    {
        if (isDeath)
        {
            return;
        }
        base.onCharacterByWall(sender, e);

    }

    public override void OnEnemyStateChange()
    {

        //TODO: wmy 这里做event操作
    }

    public override void OnPlayerIsClose(object sender, bool isCloseToPlayer)
    {
        if (currentState != specialState && isCloseToPlayer)
        {
            base.OnPlayerIsClose(sender, isCloseToPlayer);
            currentSpecialTime = specialTime;
        }
    }
    protected override void OnEnemyDamage(object sender, bool e)
    {
        base.OnEnemyDamage(sender, e);
        if (currentSpecialTime <= 0)
        {
            // 说明是背向Player的
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            direction.x *= -1;
        }
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
}
