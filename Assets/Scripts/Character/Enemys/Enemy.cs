
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
    [Header("普通状态")]
    public float normalSpeed;

    [Header("特殊状态")]
    public float specialSpeed;
    //TODO: wmy 调试
    // [SerializeField]
    public float currentSpeed;

    protected EnemyState currentState;
    protected EnemyState normalState;
    protected EnemyState specialState;

    protected PhysicsCheckEventHandler physicsEvent;
    protected CharacterEventHandler characterEventHandler;
    protected Rigidbody2D rigidbody2d;
    protected PointEventHandler pointEventHandler;
    protected Animator animator;
    [Header("状态")]
    [SerializeField]
    public bool isWalk;
    [SerializeField]
    public bool isRun;
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
        isWalk = false;
        isRun = false;
        direction = new Vector2(-1, 1);
        physicsEvent = GetComponentInChildren<PhysicsCheckEventHandler>();
        characterEventHandler = GetComponentInChildren<CharacterEventHandler>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pointEventHandler = FindAnyObjectByType<PointEventHandler>();
    }

    protected virtual void Update()
    {
        if (isDeath)
        {
            return;
        }
        animator.SetFloat(CharacterAnim.kCharacterAnimVelocityX, Mathf.Abs(rigidbody2d.velocity.x));
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

    private void OnEnable()
    {
        physicsEvent.OnCharacterByWall += onCharacterByWall;
        physicsEvent.OnPlayerIsClose += OnPlayerIsClose;
        characterEventHandler.OnCharacterDamage += OnEnemyDamage;
        characterEventHandler.OnCharacterDeath += OnEnemyDeath;
    }

    private void OnDisable()
    {
        physicsEvent.OnCharacterByWall -= onCharacterByWall;
        physicsEvent.OnPlayerIsClose -= OnPlayerIsClose;
        characterEventHandler.OnCharacterDamage -= OnEnemyDamage;
        characterEventHandler.OnCharacterDeath -= OnEnemyDeath;
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

    public virtual void OnPlayerIsClose(object sender, bool e)
    {
        SwitchToAState(EnemyStateEnum.Special);
    }


    protected virtual void OnEnemyDamage(object sender, bool isDamage)
    {
        this.isDamage = isDamage;
        if (isDamage)
        {
            Vector2 velocity = rigidbody2d.velocity;
            velocity.x = 0;
            rigidbody2d.velocity = velocity;
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
