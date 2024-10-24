using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BoarNormalState : EnemyState
{
    public override void OnEnterState()
    {
        enemy.currentSpeed = enemy.normalSpeed;
        enemy.isWalk = true;
        enemy.isRun = false;

    }

    public override void OnExitState()
    {

    }
}

class BoarSpecialState : EnemyState
{
    public override void OnEnterState()
    {
        enemy.currentSpeed = enemy.specialSpeed;
        enemy.isWalk = false;
        enemy.isRun = true;
    }

    public override void OnExitState()
    {

    }
}

public class Boar : Enemy
{
    [Header("等待时间")]
    public float waitTime;
    public float currentWaitTime;

    [Header("方向")]
    [SerializeField]
    private Vector2 direction;

    public override void Awake()
    {
        base.Awake();
        InitParams();
        // 初始状态下默认为普通状态
        SwitchToAState(EnemyStateEnum.Normal);
    }

    private void InitParams()
    {
        direction = new Vector2(-1, 0);

        normalState = new BoarNormalState();
        normalState.enemy = this;

        specialState = new BoarSpecialState();
        specialState.enemy = this;
    }

    private void OnEnable()
    {

    }

    private void FixedUpdate()
    {

    }

    

    protected override void onCharacterByWall(object sender, EventBOOLArgs e)
    {
        bool isTouchLeftWall = e.arg1;
        bool isTouchRightWall = e.arg2;

    }

    public override void OnEnemyStateChange()
    {
        //TODO: wmy 这里做event操作
    }
}
