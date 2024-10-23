using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Player player;
    private void Awake() {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }




}
