using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BoarNormalState : EnemyState
{
    public override void OnEnterState()
    {
        Debug.Log("当前状态为普通状态");
        enemy.currentSpeed = enemy.normalSpeed;
        enemy.isWalk = true;
        enemy.isRun = false;

    }

    public override void OnExitState()
    {
        Debug.Log("退出普通状态");
    }
}

class BoarSpecialState : EnemyState
{
    public override void OnEnterState()
    {
        Debug.Log("当前状态为特殊状态");
        enemy.currentSpeed = enemy.specialSpeed;
        enemy.isWalk = false;
        enemy.isRun = true;
    }

    public override void OnExitState()
    {
        Debug.Log("退出特殊状态");
    }
}

public class Boar : Enemy
{
    [Header("撞墙等待时间")]
    public float waitTime;
    public float currentWaitTime;
    private bool isWaiting;

    [Header("追击持续时间")]
    public float specialTime;
    public float currentSpecialTime;

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

    protected override void Update()
    {
        base.Update();
        CheckWallWaitingTime();
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

    /// <summary>
    /// 检测当前是否在等待时间内
    /// </summary>
    private void CheckWallWaitingTime()
    {
        if (isWaiting)
        {
            currentWaitTime -= Time.deltaTime;
            if (currentWaitTime <= 0)
            {
                // 等待时间一到，就转身往回走
                direction.x = -direction.x;
                isWaiting = false;
                Vector3 localScale = transform.localScale;
                localScale.x = -localScale.x;
                transform.localScale = localScale;
            }
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
        base.onCharacterByWall(sender, e);
        bool isTouchLeftWall = e.arg1;
        bool isTouchRightWall = e.arg2;
        if (isTouchLeftWall || isTouchRightWall)
        {
            if (!isWaiting)
            {
                currentWaitTime = waitTime;
                isWaiting = true;
            }
        }
    }

    public override void OnEnemyStateChange()
    {
        Debug.Log("是这里吗？");
        //TODO: wmy 这里做event操作
        // if (currentState == specialState) {
        //     SwitchToAState(EnemyStateEnum.Normal);
        // }
    }

    public override void OnPlayerIsClose(object sender, bool isCloseToPlayer)
    {
        if (currentState != specialState)
        {
            base.OnPlayerIsClose(sender, isCloseToPlayer);
            currentSpecialTime = specialTime;
        }

    }
}
