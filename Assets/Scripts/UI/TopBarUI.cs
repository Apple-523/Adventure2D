using UnityEngine;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{
    [Header("血量")]
    public Image greenSlider;
    [Header("红色血量")]
    public Image redSlider;
    [Header("精力条")]
    public Image yellowSlider;

    [Header("事件")]
    public float distanceTime;

    public float currentGreenTime;
    public float currentRedTime;
    public float destValue;
    public float speed;

    private PlayerEventHandler playerEventHandler;
    private void Awake()
    {
        destValue = 1;
        currentGreenTime = 0;
        currentRedTime = 0;
        playerEventHandler = PlayerEventHandler.Instance;
    }
    private void OnEnable()
    {
        playerEventHandler.OnPlayerUpdateHealth += OnPlayerUpdateHealth;
    }

    private void OnDisable()
    {
        playerEventHandler.OnPlayerUpdateHealth -= OnPlayerUpdateHealth;
    }

    private void Update()
    {

        GreenUpdateValue();
        RedUpdateValue();
    }

    private void OnPlayerUpdateHealth(object sender, DamageEventArgs args)
    {

        TopBarUpdate(args);
    }



    public void TopBarUpdate(DamageEventArgs args)
    {
        if (currentGreenTime > 0)
        {
            return;
        }
        float currentHealth = args.currentHealth;
        float maxHealth = args.maxHealth;
        if (maxHealth == currentHealth && currentHealth != 0)
        {
            redSlider.fillAmount = 1;
            greenSlider.fillAmount = 1;
            Debug.Log("currentHealth = " + currentHealth);
            Debug.Log("maxHealth = " + maxHealth);
            return;
        }

        destValue = currentHealth / maxHealth;
        Debug.Log("destValue = " + destValue);
        float diffValue = Mathf.Abs(destValue - greenSlider.fillAmount);
        speed = diffValue / distanceTime;
        currentGreenTime = distanceTime;
    }

    private void GreenUpdateValue()
    {
        if (currentGreenTime <= 0)
        {
            if (greenSlider.fillAmount != destValue)
            {
                currentRedTime = distanceTime;
                greenSlider.fillAmount = destValue;
            }
            return;
        }
        currentGreenTime -= Time.deltaTime;
        greenSlider.fillAmount -= Time.deltaTime * speed;

    }
    private void RedUpdateValue()
    {
        if (currentRedTime <= 0)
        {
            if (greenSlider.fillAmount == destValue)
            {
                redSlider.fillAmount = destValue;
            }
            return;
        }
        currentRedTime -= Time.deltaTime;
        redSlider.fillAmount -= Time.deltaTime * speed;
    }

}
