using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Inspector
    

    private Vector3 checkpointPosition;

    public GameObject lightObj;

    public GameObject latern;

    private Animator laternAnimator;

    #endregion

    #region Unity Event Functions

    private void Start()
    { 

        laternAnimator = latern.GetComponent<Animator>();
        
        checkpointPosition = transform.position;
        
        lightObj.SetActive(false);
    }

    private void Update()
    {
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            laternAnimator.Play("ANIM_Checkpoint");
            
            lightObj.SetActive(true);
            
            GameObject.Find("PlayerController").GetComponent<PlayerController>().respawnPoint = checkpointPosition;;
        }
    }
    
    

    #endregion
}
