using System;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{

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

}


