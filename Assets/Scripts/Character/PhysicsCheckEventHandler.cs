using System;
using UnityEngine;


public class PhysicsCheckEventHandler : MonoBehaviour
{
    public event EventHandler<bool> OnCharacterGroundChange;
    public void CharacterGroundChange(bool isOnGround)
    {
        OnCharacterGroundChange?.Invoke(this, isOnGround);
    }


    public event EventHandler<EventBOOLArgs> OnCharacterByWall;

    public void CharacterByWall(bool isByLeftWall, bool isByRightWall)
    {
        OnCharacterByWall?.Invoke(this, new EventBOOLArgs(isByLeftWall, isByRightWall));
    }

    /// <summary>
    /// 主人公是否靠近的事件
    /// </summary>
    public event EventHandler<bool> OnPlayerIsClose;
    public void PlayerIsClose(bool isInClose)
    {
        OnPlayerIsClose?.Invoke(this, isInClose);
    }

}

public class EventBOOLArgs
{
    public bool arg1;
    public bool arg2;

    public EventBOOLArgs(bool arg1, bool arg2)
    {
        this.arg1 = arg1;
        this.arg2 = arg2;
    }

    public bool IsAllTrue()
    {
        return arg1 & arg2;
    }
    public bool AtLeastOneTrue()
    {
        return arg1 || arg2;
    }
}