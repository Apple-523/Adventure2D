using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterEventHandler : MonoBehaviour
{
    public event EventHandler<DamageEventArgs> OnCharacterDamage;
    public void CharacterDamage(bool isDamage, float currentHealth, float maxHealth)
    {
        OnCharacterDamage?.Invoke(this, new DamageEventArgs(isDamage, currentHealth, maxHealth));
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
    public float maxHealth;

    public DamageEventArgs(bool isDamage, float currentHealth, float maxHealth)
    {
        this.isDamage = isDamage;
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }
}
