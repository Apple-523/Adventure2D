using System;
using UnityEngine;


public class PhysicsCheckEventHandler
{
    // 静态只读实例，确保只有一个实例
    private static readonly PhysicsCheckEventHandler instance = new PhysicsCheckEventHandler();

    // 私有构造函数，防止从外部创建实例
    private PhysicsCheckEventHandler() { }

    // 公有静态属性用于获取实例
    public static PhysicsCheckEventHandler Instance
    {
        get
        {
            return instance;
        }
    }
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