using System;
using UnityEngine;


public class PhysicsCheckEventHandler : MonoBehaviour
{
    public event EventHandler<bool> OnPlayerGroundChange;
    public void PlayerGroundChange(bool isOnGround)
    {
        OnPlayerGroundChange?.Invoke(this, isOnGround);
    }

    public event EventHandler<bool> OnPlayerBeginJump;

    public void PlayerBeginJump(bool isOnGround)
    {
        OnPlayerBeginJump?.Invoke(this, isOnGround);
    }
}