using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEventHandler : MonoBehaviour
{
    public event EventHandler<float> OnAddPoint;

    public void AddPoint(float point)
    {
        OnAddPoint?.Invoke(this, point);
    }

}
