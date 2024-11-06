using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class Bootstrap : MonoBehaviour
{
    private string firstAddressable = "MainScene";

    private void Start()
    {
        LoadMainScene();
    }

    private void LoadMainScene()
    {
        SceneLoadUtils.Instance.LoadScene(firstAddressable, OnBeforeLoadScene, OnAfterLoadScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    private void OnAfterLoadScene(string sceneName)
    {
        Debug.Log("第一个场景加载成功:" + sceneName);
    }

    private void OnBeforeLoadScene(string sceneName)
    {
        Debug.Log("第一个场景加载前:" + sceneName);
    }


}
