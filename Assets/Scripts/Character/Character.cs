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
    [Header("无敌状态时间")]
    public float invincibleStateTime;
    [SerializeField]
    private float currentStateTime;
    public LayerMask canBeDamagedLayer;
    private CharacterEventHandler characterEventHandler;

    private void Awake()
    {
        characterEventHandler = GetComponentInChildren<CharacterEventHandler>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentStateTime > 0)
        {
            currentHealth -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CommonUtils.IsInLayerMask(other.gameObject, canBeDamagedLayer))
        {
            // 将血量减少
            Attack attack = other.gameObject.GetComponent<Attack>();
            float attackValue = attack.attackValue;
            Debug.Log("attackValue = " + attackValue);
            currentHealth -= attackValue;
            if (currentHealth <= 0)
            {
                // 死亡
                Debug.Log(gameObject.name + "已死亡！");
                characterEventHandler.CharacterDeath(true);
            }
            else
            {
                // 受伤
                Debug.Log(gameObject.name + "受伤了!");
                // 播放动画
                characterEventHandler.CharacterDamage(true);
                // 无敌时间
                currentStateTime = invincibleStateTime;
            }

        }

        // other.gameObject.tag = 
        // Debug.Log("OnCollisionStay2D");
    }
}
