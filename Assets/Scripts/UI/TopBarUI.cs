using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{
    [Header("血量")]
    public Slider greenSlider;
    [Header("红色血量")]
    public Slider redSlider;
    [Header("精力条")]
    public Slider yellowSlider;

    private PlayerEventHandler playerEventHandler;
    private void Awake()
    {
        playerEventHandler = PlayerEventHandler.Instance;
    }



}
