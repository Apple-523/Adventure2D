using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEventHandler : MonoBehaviour
{
    public event EventHandler<bool> OnCharacterDamage;
    public void CharacterDamage(bool isDamage) {
        OnCharacterDamage?.Invoke(this,isDamage);
    }

    public event EventHandler<bool> OnCharacterDeath;
    public void CharacterDeath(bool isDeath) {
        OnCharacterDeath?.Invoke(this,isDeath);
    }

}
