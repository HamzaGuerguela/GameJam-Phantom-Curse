using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject mainPanel;

    #endregion

    #region Unity Event Functions

    private void Start()
    {
        mainPanel.SetActive(true);
    }

    #endregion

    public void StartButton()
    {
        SceneManager.LoadScene("Sandbox 1");
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
