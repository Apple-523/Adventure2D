using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitlePan : MonoBehaviour
{
    [Header("标题")]
    public TextMeshProUGUI title;

    private void Awake()
    {
        Debug.Log("TitlePan");
    }
}
