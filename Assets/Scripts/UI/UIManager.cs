using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("顶部TopBar")]
    public Image topBar;
    private PlayerEventHandler playerEventHandler;
    private GameStateEventHandler gameStateEventHandler;
    private void Awake()
    {
        playerEventHandler = PlayerEventHandler.Instance;
        gameStateEventHandler = GameStateEventHandler.Instance;
    }
    private void OnEnable()
    {
        playerEventHandler.OnPlayerUpdateHealth += OnPlayerUpdateHealth;
        gameStateEventHandler.OnUpdateGameState += OnUpdateGameState;
    }
    private void OnDisable()
    {
        playerEventHandler.OnPlayerUpdateHealth -= OnPlayerUpdateHealth;
        gameStateEventHandler.OnUpdateGameState -= OnUpdateGameState;
    }

    private void OnUpdateGameState(object sender, GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Open:
                topBar.enabled = false;
                break;
            case GameState.StartGame:
                topBar.enabled = true;
                Debug.Log("开始游戏 - UI");

                break;
            case GameState.PlayerDie:
                topBar.enabled = false;
                break;
        }
    }

    private void OnPlayerUpdateHealth(object sender, DamageEventArgs args)
    {
        TopBarUI topBarUI = topBar.GetComponent<TopBarUI>();
        topBarUI.TopBarUpdate(args);
    }
}
