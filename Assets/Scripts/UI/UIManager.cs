using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("开始按钮盘")]
    public Image startPan;
    [Header("标题")]
    public Image titlePan;
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
                {
                    startPan.gameObject.SetActive(true);
                    titlePan.gameObject.SetActive(true);
                    topBar.gameObject.SetActive(false);
                    Debug.Log("开局！- UI");
                }

                break;
            case GameState.StartGame:
                {
                    startPan.gameObject.SetActive(false);
                    titlePan.gameObject.SetActive(false);
                    topBar.gameObject.SetActive(true);
                    Debug.Log("开始游戏 - UI");

                }
                break;

            case GameState.PlayerDie:
                {
                    startPan.gameObject.SetActive(false);
                    titlePan.gameObject.SetActive(false);
                    topBar.gameObject.SetActive(false);
                    Debug.Log("游戏结束 - UI");
                }
                break;
        }
    }

    private void OnPlayerUpdateHealth(object sender, DamageEventArgs args)
    {
        TopBarUI topBarUI = topBar.GetComponent<TopBarUI>();
        topBarUI.TopBarUpdate(args);
    }
}
