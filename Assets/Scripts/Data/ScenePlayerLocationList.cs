using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenePlayerLocationList", menuName = "Data/ScenePlayerLocationList", order = 0)]
public class ScenePlayerLocationList : ScriptableObject
{
    public List<ScenePlayerLocation> items = new List<ScenePlayerLocation>();
}

[System.Serializable]
public class ScenePlayerLocation
{
    public string sceneName;
    public Vector2 playerLocation;
}