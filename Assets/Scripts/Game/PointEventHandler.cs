using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEventHandler : MonoBehaviour
{
    // 静态只读实例，确保只有一个实例
    private static readonly PointEventHandler instance = new PointEventHandler();

    // 私有构造函数，防止从外部创建实例
    private PointEventHandler() { }

    // 公有静态属性用于获取实例
    public static PointEventHandler Instance
    {
        get
        {
            return instance;
        }
    }
    public event EventHandler<float> OnAddPoint;

    public void AddPoint(float point)
    {
        OnAddPoint?.Invoke(this, point);
    }

}
