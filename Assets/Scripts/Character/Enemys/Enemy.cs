
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
    protected Rigidbody2D rigidbody2d;
    protected Animator animator;
    [Header("状态")]
    [SerializeField]
    public bool isWalk;
    [SerializeField]
    public bool isRun;
    [Header("方向")]
    [SerializeField]
    protected Vector2 direction;

    protected virtual void Awake()
    {
        isWalk = false;
        isRun = false;
        direction = new Vector2(-1, 1);
        physicsEvent = GetComponentInChildren<PhysicsCheckEventHandler>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        animator.SetFloat(CharacterAnim.kPlayerAnimVelocityX, Mathf.Abs(rigidbody2d.velocity.x));
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {

        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = currentSpeed * direction.x;
        // Debug.Log("direction = " +direction );
        rigidbody2d.velocity = velocity;
    }

    private void OnEnable()
    {
        physicsEvent.OnCharacterByWall += onCharacterByWall;
        physicsEvent.OnPlayerIsClose += OnPlayerIsClose;
    }

    private void OnDisable()
    {
        physicsEvent.OnCharacterByWall -= onCharacterByWall;
        physicsEvent.OnPlayerIsClose -= OnPlayerIsClose;
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

    public virtual void OnPlayerIsClose(object sender, bool e) {
        Debug.Log("OnPlayerIsClose");
        SwitchToAState(EnemyStateEnum.Special);
    }

}
