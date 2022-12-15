using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    private PlayerActionControls playerActionControls;
    
    #region Inspector

    [SerializeField] private GameObject fade;

    [SerializeField] private GameObject enemyPrefab, menu;

    private Vector3 enemyRespawnPosition;

    #endregion

    #region Unity Event Functions

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerActionControls.UI.ESC.performed += ESCPressed;
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    private void Start()
    {
        
    }

    #endregion

    public void FadeOut()
    {
        fade.SetActive(true);
        
        fade.GetComponent<Animator>().Play("ANIM_Fade_Out");
        
        
    }

    public void FadeIn()
    {
        fade.SetActive(true);
        
        fade.GetComponent<Animator>().Play("ANIM_Fade_In");
        
        Invoke("FadeObject", 1.2f);
    }

    public void FadeObject()
    {
        fade.SetActive(false);
    }

    public void FadeOutBlackScreen()
    {
        Debug.Log("Enemy Spawn");
        // TODO Make Enemy Set Active in this Event 
        
    }

    private void ESCPressed(InputAction.CallbackContext _)
    {
        
        if (!menu.activeInHierarchy)
        {
            menu.SetActive(true);
            
            Time.timeScale = 0f;
            return;
        }
        
        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);

            Time.timeScale = 1f;
            return;
        }
        
    }
}
