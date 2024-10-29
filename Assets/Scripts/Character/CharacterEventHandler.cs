using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEventHandler
{
    // 静态只读实例，确保只有一个实例
    private static readonly CharacterEventHandler instance = new CharacterEventHandler();

    // 私有构造函数，防止从外部创建实例
    private CharacterEventHandler() { }

    // 公有静态属性用于获取实例
    public static CharacterEventHandler Instance
    {
        get
        {
            return instance;
        }
    }
    public event EventHandler<DamageEventArgs> OnCharacterDamage;
    public void CharacterDamage(bool isDamage, float currentHealth)
    {
        OnCharacterDamage?.Invoke(this, new DamageEventArgs(isDamage, currentHealth));
    }

    public event EventHandler<bool> OnCharacterDeath;
    public void CharacterDeath(bool isDeath)
    {
        OnCharacterDeath?.Invoke(this, isDeath);
    }

}

public class DamageEventArgs
{
    public bool isDamage;
    public float currentHealth;

    public DamageEventArgs(bool isDamage, float currentHealth)
    {
        this.isDamage = isDamage;
        this.currentHealth = currentHealth;
    }
}
