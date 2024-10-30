using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BeeNormalState : EnemyState
{
    public override void OnEnterState()
    {
        enemy.currentSpeed = enemy.normalSpeed;
    }

    public override void OnExitState()
    {
    }
}

class BeeSpecialState : EnemyState
{
    public override void OnEnterState()
    {
        enemy.currentSpeed = enemy.specialSpeed;

    }

    public override void OnExitState()
    {
    }
}

public class Bee : Enemy
{
    [Header("蜜蜂飞舞的范围")]
    public BoxCollider2D boxCollider2D;

    protected override void Awake()
    {
        base.Awake();
        ChangeDirection();
        InitParams();
        SwitchToAState(EnemyStateEnum.Normal);
    }
    private void InitParams()
    {
        normalState = new BeeNormalState();
        normalState.enemy = this;

        specialState = new BeeNormalState();
        specialState.enemy = this;
    }
    public override void OnEnemyStateChange()
    {

    }

    protected override void ChangeDirection()
    {
        // base.ChangeDirection(); 
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 randomVector = new Vector2(x, y);
        direction = randomVector;
        //TODO: wmy 调整
        // Debug.Log("随机生成的Vector2: " + randomVector);
    }

}
