
using UnityEngine;
public enum GameState
{
    Open,
    StartGame,
    PlayerDie
}

public class GameStateManager : MonoBehaviour
{
    // 静态只读实例，确保只有一个实例
    private static readonly GameStateManager instance = new GameStateManager();

    // 私有构造函数，防止从外部创建实例
    private GameStateManager() { }

    // 公有静态属性用于获取实例
    public static GameStateManager Instance
    {
        get
        {
            return instance;
        }
    }

    private GameState gameState;
    private GameStateEventHandler gameStateEventHandler;
    private void Awake()
    {
        gameState = GameState.Open;
        //TODO: wmy test
        gameState = GameState.StartGame;
        gameStateEventHandler = GameStateEventHandler.Instance;
        gameStateEventHandler.UpdateGameState(gameState);
    }
}
