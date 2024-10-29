using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("给一个受伤的冲力")]
    public float damageV;
    private CharacterEventHandler characterEventHandler;
    private Rigidbody2D rigidbody2d;


    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        characterEventHandler = CharacterEventHandler.Instance;
    }

    private void OnEnable()
    {
        characterEventHandler.OnCharacterDamage += OnCharacterDamage;
    }


    private void OnDisable()
    {
        characterEventHandler.OnCharacterDamage += OnCharacterDamage;
    }

    private void OnCharacterDamage(object sender, DamageEventArgs args)
    {
        Vector2 velocity = rigidbody2d.velocity;
        velocity.x = 0;
        rigidbody2d.velocity = velocity;
    }
}
