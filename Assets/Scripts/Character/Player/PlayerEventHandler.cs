using System;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    // 静态只读实例，确保只有一个实例
    private static readonly PlayerEventHandler instance = new PlayerEventHandler();

    // 私有构造函数，防止从外部创建实例
    private PlayerEventHandler() { }

    // 公有静态属性用于获取实例
    public static PlayerEventHandler Instance
    {
        get
        {
            return instance;
        }
    }
    public event EventHandler<bool> OnCharacterBeginJump;

    public void CharacterBeginJump(bool isOnGround)
    {
        OnCharacterBeginJump?.Invoke(this, isOnGround);
    }

    public event EventHandler<bool> OnCharacterPressAttack;
    public void CharacterPressAttack(bool isAttack)
    {
        OnCharacterPressAttack?.Invoke(this, isAttack);
    }

    public event EventHandler<bool> OnCharacterEndAttack;
    public void CharacterEndAttack(bool isAttack)
    {
        OnCharacterEndAttack?.Invoke(this, isAttack);
    }

    public event EventHandler<DamageEventArgs> OnPlayerUpdateHealth;
    public void PlayerUpdateHealth(bool isDamage, float currentHealth)
    {
        if (isDamage)
        {
            Debug.Log("Player受伤了！" + currentHealth);
        }
        OnPlayerUpdateHealth?.Invoke(this, new DamageEventArgs(isDamage, currentHealth));

    }

}


