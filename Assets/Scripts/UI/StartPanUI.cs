using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanUI : MonoBehaviour
{
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void OnClickStartGame()
    {
        GameStateEventHandler.Instance.UpdateGameState(GameState.StartGame);
    }
    public void OnClickSettings()
    {
        Debug.Log("OnClickSettings");
    }

    public void OnClickAbout()
    {
        Debug.Log("OnClickAbout");
    }
    public void OnClickQuit()
    {
        Debug.Log("OnClickQuit");
    }
}
