using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject fade;

    [SerializeField] private GameObject enemyPrefab;

    private Vector3 enemyRespawnPosition;

    #endregion

    #region Unity Event Functions

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
}
