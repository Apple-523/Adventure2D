using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("最大血量")]
    public float maxHealth;
    [Header("当前血量")]
    [SerializeField]
    private float currentHealth;
    // 公开的只读属性，允许外部读取但不允许修改
    public float CurrentHealth
    {
        get { return currentHealth; }
    }
    [Header("无敌状态时间")]
    public float invincibleStateTime;
    [SerializeField]
    private float currentStateTime;
    public LayerMask canBeDamagedLayer;
    private CharacterEventHandler characterEventHandler;
    private PlayerEventHandler playerEventHandler;

    private void Awake()
    {
        characterEventHandler = GetComponentInChildren<CharacterEventHandler>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentStateTime > 0)
        {
            currentStateTime -= Time.deltaTime;
        }
    }

    public void UpdateAddHealth(float addHealth)
    {
        currentHealth += addHealth;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        characterEventHandler.CharacterHealthChange(false, currentHealth, maxHealth);
        // playerEventHandler.PlayerUpdateHealth(false, currentHealth, maxHealth);

    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CommonUtils.IsInLayerMask(other.gameObject, canBeDamagedLayer) &&
        currentStateTime <= 0)
        {
            // 将血量减少
            Attack attack = other.gameObject.GetComponent<Attack>();
            Debug.Log("other.gameObject = " + other.gameObject);
            float attackValue = attack.attackValue;
            currentHealth -= attackValue;
            if (currentHealth <= 0)
            {
                // 死亡
                Debug.Log(gameObject.name + "已死亡！");
                currentHealth = 0;
                characterEventHandler.CharacterDeath(true);
            }
            else
            {
                // 受伤
                Debug.Log(gameObject.name + "受伤了!");
                // 播放动画
                characterEventHandler.CharacterHealthChange(true, currentHealth, maxHealth);
                // 无敌时间
                currentStateTime = invincibleStateTime;
            }
        }
    }

    public void InitCharacter()
    {
        currentHealth = maxHealth;
    }
}
