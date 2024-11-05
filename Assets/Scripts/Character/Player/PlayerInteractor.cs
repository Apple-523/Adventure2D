using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("按下的互动")]
    public GameObject pressOObject;
    private PlayerInputSystem playerInputSystem;


    private bool canPressO = false;
    private GameObject openGameObject;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

        playerInputSystem = new PlayerInputSystem();

    }
    private void OnEnable()
    {
        playerInputSystem.Enable();
        playerInputSystem.Player.Interactor.started += OnPlayerInteractor;
    }
    private void OnDisable()
    {
        playerInputSystem.Player.Interactor.started -= OnPlayerInteractor;
        playerInputSystem.Disable();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(MYTag.kTagInteractor))
        {
            canPressO = true;
            openGameObject = other.gameObject;
            pressOObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(MYTag.kTagInteractor))
        {
            canPressO = false;
            openGameObject = null;
            pressOObject.SetActive(false);
        }
    }

    private void OnPlayerInteractor(InputAction.CallbackContext context)
    {
        Debug.Log("OnPlayerInteractor");
        if (canPressO && openGameObject != null)
        {
            Interoperable place = openGameObject.GetComponent<Interoperable>();
            if (place != null)
            {
                place.OnTriggerInteroperable(gameObject);
            }
        }
    }
}
