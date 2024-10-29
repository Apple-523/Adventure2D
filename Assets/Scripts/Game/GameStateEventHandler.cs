using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateEventHandler : MonoBehaviour
{
    // 静态只读实例，确保只有一个实例
    private static readonly GameStateEventHandler instance = new GameStateEventHandler();

    // 私有构造函数，防止从外部创建实例
    private GameStateEventHandler() { }

    // 公有静态属性用于获取实例
    public static GameStateEventHandler Instance
    {
        get
        {
            return instance;
        }
    }
    public event EventHandler<GameState> OnUpdateGameState;
    public void UpdateGameState(GameState gameState)
    {
        OnUpdateGameState?.Invoke(this, gameState);
    }
}
