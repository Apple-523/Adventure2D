using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("开始按钮盘")]
    public Image startPan;
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
                startPan.enabled = true;
                topBar.enabled = false;
                Debug.Log("开局！");
                break;
            case GameState.StartGame:
                startPan.enabled = false;
                topBar.enabled = true;
                Debug.Log("开始游戏 - UI");
                break;
            case GameState.PlayerDie:
                startPan.enabled = false;
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
