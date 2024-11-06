using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransFormScene : Interoperable
{
    [Header("进入的场景名称")]
    public string sceneName;

    public override void OnTriggerInteroperable(GameObject gameObject)
    {
        SceneLoadUtils.Instance.LoadScene(sceneName, OnBeforeLoadScene, OnAfterLoadScene, LoadSceneMode.Additive);
    }

    private void OnBeforeLoadScene(string sceneName)
    {

    }
    private void OnAfterLoadScene(string sceneName)
    {

    }
}
