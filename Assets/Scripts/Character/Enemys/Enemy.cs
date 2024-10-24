using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateEnum
{
    Normal,
    Special,
}

public abstract class EnemyState
{
    public Enemy enemy;
    /// <summary>
    /// 进入当前状态
    /// </summary>
    public abstract void OnEnterState();
    /// <summary>
    /// 退出当前状态
    /// </summary>
    public abstract void OnExitState();

}

public abstract class Enemy : MonoBehaviour
{
    [Header("普通状态")]
    public float normalSpeed;

    [Header("特殊状态")]
    public float specialSpeed;

    public float currentSpeed;

    protected EnemyState currentState;
    protected EnemyState normalState;
    protected EnemyState specialState;

    protected PhysicsCheckEventHandler physicsEvent;
    public bool isWalk;
    public bool isRun;

    public virtual void Awake()
    {
        isWalk = false;
        isRun = false;
        physicsEvent = GetComponentInChildren<PhysicsCheckEventHandler>();
    }

    private void OnEnable()
    {
        physicsEvent.OnCharacterByWall += onCharacterByWall;
    }

    private void OnDisable()
    {
        physicsEvent.OnCharacterByWall -= onCharacterByWall;
    }

    protected virtual void onCharacterByWall(object sender, EventBOOLArgs e)
    {
        //TODO: wmy todo
    }

    public void SwitchToAState(EnemyStateEnum state)
    {

        EnemyState nextState = currentState;
        switch (state)
        {
            case EnemyStateEnum.Normal:
                nextState = specialState;
                break;
            case EnemyStateEnum.Special:
                nextState = normalState;
                break;
        }
        if (currentState == nextState)
        {
            return;
        }
        currentState?.OnExitState();
        currentState = nextState;
        currentState?.OnEnterState();
        OnEnemyStateChange();
    }

    public abstract void OnEnemyStateChange();
}
