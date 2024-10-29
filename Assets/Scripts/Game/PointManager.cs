using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{

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
