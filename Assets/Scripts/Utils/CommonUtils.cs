using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtils
{
    public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // 获取 GameObject 的层
        int layer = obj.layer;
        Debug.Log("layer = " + layer);
        Debug.Log("layerMask = " + (int)layerMask);
        // 使用位运算检查是否在 LayerMask 中
        return (layerMask & (1 << layer)) != 0;
    }
}
