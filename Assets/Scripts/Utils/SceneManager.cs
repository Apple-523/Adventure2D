using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneEventHandler
{
    // 静态只读实例，确保只有一个实例
    private static readonly SceneEventHandler instance = new SceneEventHandler();

    // 私有构造函数，防止从外部创建实例
    private SceneEventHandler() { }
    public static SceneEventHandler Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// 加载场景完成
    /// </summary>
    public event EventHandler<string> OnSceneLoadComplete;
    public void SceneLoadComplete(string sceneName)
    {
        OnSceneLoadComplete?.Invoke(this, sceneName);
    }

    /// <summary>
    /// 在加载场景之前
    /// </summary>
    public event EventHandler<string> OnSceneBeforeLoad;
    public void SceneBeforeLoad(string sceneName)
    {
        OnSceneBeforeLoad?.Invoke(this, sceneName);
    }

    /// <summary>
    /// 卸载场景之前
    /// </summary>
    public event EventHandler<string> OnSceneBeforeUnLoad;
    public void SceneBeforeUnLoad(string sceneName)
    {
        OnSceneBeforeUnLoad?.Invoke(this, sceneName);
    }

    /// <summary>
    /// 卸载场景之后
    /// </summary>
    public event EventHandler<string> OnSceneUnLoadComplete;
    public void SceneUnLoadComplete(string sceneName)
    {
        OnSceneUnLoadComplete?.Invoke(this, sceneName);
    }

}

public delegate void OnBeforeLoadScene(string sceneName);
public delegate void OnAfterLoadScene(string sceneName);
public class SceneLoadUtils
{
    // 静态只读实例，确保只有一个实例
    private static readonly SceneLoadUtils instance = new SceneLoadUtils();

    private SceneInstance lastSceneInstance;

    // 私有构造函数，防止从外部创建实例
    private SceneLoadUtils()
    {

    }
    public static SceneLoadUtils Instance
    {
        get
        {
            return instance;
        }
    }
    public void LoadScene(string sceneName, OnBeforeLoadScene onBeforeLoadScene, OnAfterLoadScene onAfterLoadScene, LoadSceneMode loadSceneMode)
    {
        if (lastSceneInstance.Scene == null)
        {
            Debug.Log("lastSceneInstance.Scene == null");
        }
        else
        {
            Addressables.UnloadSceneAsync(lastSceneInstance).Completed += delegate (AsyncOperationHandle<SceneInstance> handle)
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    ExecLoadScene(sceneName, onBeforeLoadScene, onAfterLoadScene);
                }

            };
        }
        ExecLoadScene(sceneName, onBeforeLoadScene, onAfterLoadScene);
    }

    private void ExecLoadScene(string sceneName, OnBeforeLoadScene onBeforeLoadScene, OnAfterLoadScene onAfterLoadScene)
    {
        if (lastSceneInstance.Scene.name != null && sceneName.Equals(lastSceneInstance.Scene.name))
        {
            Debug.Log("场景名称相同");
            return;
        }
        if (lastSceneInstance.Scene.name != null)
        {
            SceneEventHandler.Instance.SceneUnLoadComplete(lastSceneInstance.Scene.name);
        }

        SceneEventHandler.Instance.SceneBeforeUnLoad(lastSceneInstance.Scene.name);
        Debug.Log("加载场景:" + sceneName);
        SceneEventHandler.Instance.SceneBeforeLoad(sceneName);
        onBeforeLoadScene(sceneName);
        AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        asyncOperationHandle.Completed += delegate (AsyncOperationHandle<SceneInstance> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onAfterLoadScene(sceneName);
                SceneEventHandler.Instance.SceneLoadComplete(sceneName);
                lastSceneInstance = handle.Result;
            }
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                Debug.Log("load failed:" + sceneName);
            }

        };
    }

}

public class SceneManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    [Header("场景中Player的初始位置")]
    public ScenePlayerLocationList scenePlayerLocationList;

    [Header("Player")]
    public GameObject player;

    private void Start()
    {
        // 启动之后，使用EventHandler来加载场景    
        SceneLoadUtils.Instance.LoadScene("OpenScene", OnBeforeLoadScene, OnAfterLoadScene, LoadSceneMode.Additive);
    }

    private void OnAfterLoadScene(string sceneName)
    {
        Debug.Log("OnAfterLoadScene : " + sceneName);
        // 在加载完成后将主摄像机的范围改好
        ChangeCameraBounds();
        TransFormPlayerLocation(sceneName);
    }

    private void TransFormPlayerLocation(string sceneName)
    {
        foreach (ScenePlayerLocation item in scenePlayerLocationList.items)
        {
            if (item.sceneName.Equals(sceneName))
            {
                Vector2 playerLocation = item.playerLocation;
                if (player != null)
                {
                    player.transform.position = playerLocation;
                }
                break;
            }
        }
    }
    private void ChangeCameraBounds()
    {
        GameObject bounds = GameObject.FindGameObjectWithTag(MYTag.kTagBounds);
        PolygonCollider2D cameraBounds = bounds.GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = virtualCamera.GetComponent<CinemachineConfiner>();

        if (confiner != null && cameraBounds != null)
        {
            confiner.m_BoundingShape2D = cameraBounds;
        }
        confiner.InvalidatePathCache();
    }

    private void OnBeforeLoadScene(string sceneName)
    {
        Debug.Log("OnBeforeLoadScene : " + sceneName);
    }
}
