using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    #region Inspector
    
    #endregion

    #region Unity Event Functions

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    #endregion

    private void FadeAnimationEvent()
    {
        FindObjectOfType<GameController>().FadeOutBlackScreen();
    }
    
    public void FadeEndScreenAnimationEvent()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
