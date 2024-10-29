using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterEventHandler : MonoBehaviour
{
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
