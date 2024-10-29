
using System;
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
    [Header("撞墙等待时间")]
    public float waitTime;
    public float currentWaitTime;
    protected bool isWaiting;

    [Header("追击持续时间")]
    public float specialTime;
    public float currentSpecialTime;
    [Header("普通状态")]
    public float normalSpeed;

    [Header("特殊状态")]
    public float specialSpeed;
    [SerializeField]
    public float currentSpeed;

    protected EnemyState currentState;
    protected EnemyState normalState;
    protected EnemyState specialState;

    protected PhysicsCheckEventHandler physicsEvent;
    protected CharacterEventHandler characterEventHandler;
    protected Rigidbody2D rigidbody2d;
    protected PointEventHandler pointEventHandler;
    protected Animator animator;
    [Header("方向")]
    [SerializeField]
    protected Vector2 direction;
    [Header("是否受伤了")]
    [SerializeField]
    protected bool isDamage;
    [Header("是否已死亡")]
    [SerializeField]
    protected bool isDeath;

    [Header("死亡奖励分")]
    public float point;

    protected virtual void Awake()
    {
        // isWalk = false;
        // isRun = false;
        direction = new Vector2(-1, 1);
        physicsEvent = PhysicsCheckEventHandler.Instance;
        characterEventHandler = CharacterEventHandler.Instance;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pointEventHandler = PointEventHandler.Instance;
    }

    protected virtual void Update()
    {
        if (isDeath)
        {
            return;
        }
        CheckTrunAroundWaitingTime();
        animator.SetFloat(CharacterAnim.kCharacterAnimVelocityX, Mathf.Abs(rigidbody2d.velocity.x));
    }
    /// <summary>
    /// 检测当前是否在等待时间内
    /// </summary>
    private void CheckTrunAroundWaitingTime()
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
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        if (!isDamage && !isDeath)
        {
            Move();
        }

    }
    private void Move()
    {
        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = currentSpeed * direction.x;
        rigidbody2d.velocity = velocity;
    }

    protected virtual void OnEnable()
    {
        physicsEvent.OnCharacterByWall += onCharacterByWall;
        physicsEvent.OnPlayerIsClose += OnPlayerIsClose;
        characterEventHandler.OnCharacterDamage += OnEnemyDamage;
        characterEventHandler.OnCharacterDeath += OnEnemyDeath;
    }


    protected virtual void OnDisable()
    {
        physicsEvent.OnCharacterByWall -= onCharacterByWall;
        physicsEvent.OnPlayerIsClose -= OnPlayerIsClose;
        characterEventHandler.OnCharacterDamage -= OnEnemyDamage;
        characterEventHandler.OnCharacterDeath -= OnEnemyDeath;
    }


    protected virtual void onCharacterByWall(object sender, EventBOOLArgs e)
    {
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

    public void SwitchToAState(EnemyStateEnum state)
    {

        EnemyState nextState = currentState;
        switch (state)
        {
            case EnemyStateEnum.Normal:
                nextState = normalState;
                break;
            case EnemyStateEnum.Special:
                nextState = specialState;
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

    public virtual void OnPlayerIsClose(object sender, bool isCloseToPlayer)
    {
        SwitchToAState(EnemyStateEnum.Special);
    }

    

    protected virtual void OnEnemyDamage(object sender, DamageEventArgs arg)
    {
        this.isDamage = arg.isDamage;
        if (isDamage)
        {
            animator.SetTrigger(CharacterAnim.kCharacterAnimDamageTrig);
        }

    }

    protected virtual void OnEnemyDeath(object sender, bool isDeath)
    {
        this.isDeath = isDeath;
        if (isDeath)
        {
            animator.SetBool(CharacterAnim.kCharacterAnimIsDead, isDeath);
            animator.SetTrigger(CharacterAnim.kCharacterAnimDeadTrig);
            //TODO: wmy 通知加分
            pointEventHandler.AddPoint(point);
        }
    }

   
}
