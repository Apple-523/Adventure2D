using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanUI : MonoBehaviour
{
    public void OnClickRetry() {
        GameStateEventHandler.Instance.UpdateGameState(GameState.Open);
    }
}
