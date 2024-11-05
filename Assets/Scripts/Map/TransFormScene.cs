using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransFormScene : Interoperable
{
    [Header("进入的场景名称")]
    public GameObject sceneName;
    [Header("Player的位置")]
    public Vector2 playerPosition;

    public override void OnTriggerInteroperable(GameObject gameObject)
    {
        //TODO: wmy 触发场景切换
    }
}
