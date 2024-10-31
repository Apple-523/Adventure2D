
using System;
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
        gameState = GameState.Open;
        gameStateEventHandler = GameStateEventHandler.Instance;
        gameStateEventHandler.UpdateGameState(gameState);
    }

    private void OnEnable()
    {
        gameStateEventHandler.OnUpdateGameState += OnUpdateGameState;
    }

    private void OnDisable()
    {
        gameStateEventHandler.OnUpdateGameState -= OnUpdateGameState;
    }

    private void OnUpdateGameState(object sender, GameState gameState)
    {
        this.gameState = gameState;
    }
}
