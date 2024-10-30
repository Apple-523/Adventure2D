
using UnityEngine;
public enum GameState
{
    Open,
    StartGame,
    PlayerDie
}

public class GameStateManager : MonoBehaviour
{

    private GameState gameState;
    private GameStateEventHandler gameStateEventHandler;
    private void Awake()
    {
        Debug.Log("GameStateManager Awake");
        //TODO: wmy test
        gameState = GameState.StartGame;
        gameStateEventHandler = GameStateEventHandler.Instance;
        gameStateEventHandler.UpdateGameState(gameState);
    }
}
