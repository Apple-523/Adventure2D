using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("开局音乐")]
    public AudioSource openGameAudioSource;

    [Header("开始游戏音乐")]
    public AudioSource startGameAudioSource;

    [Header("暂停游戏音乐")]
    public AudioSource pauseGameAudioSource;

    [Header("主角受伤音乐")]
    public AudioSource playerDamageAudioSource;

    [Header("主角死亡音乐")]
    public AudioSource playerDieAudioSource;
}
