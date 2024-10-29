using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    // 静态只读实例，确保只有一个实例
    private static readonly PointManager instance = new PointManager();

    // 私有构造函数，防止从外部创建实例
    private PointManager() { }

    // 公有静态属性用于获取实例
    public static PointManager Instance
    {
        get
        {
            return instance;
        }
    }
    private PointEventHandler pointEventHandler;
    [Header("当前的分数")]
    private float currentPoint;

    private void Awake()
    {
        currentPoint = 0;
        pointEventHandler = PointEventHandler.Instance;
    }
    private void OnEnable()
    {
        pointEventHandler.OnAddPoint += OnAddPoint;
    }

    private void OnDisable()
    {
        pointEventHandler.OnAddPoint -= OnAddPoint;
    }

    private void OnAddPoint(object sender, float point)
    {
        currentPoint += point;
        Debug.Log("currentPoint = " + currentPoint);
        //TODO: wmy 通知UI更新
    }
}
