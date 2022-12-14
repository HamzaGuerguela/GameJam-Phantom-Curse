using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneEnemySpawn : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject enemyObject;

    [SerializeField] private GameObject spriteObject;
    
    [SerializeField] private float timeUntilSpawn;

    private Collider2D thisCollider;

    public bool enemyCanSpawn = false;

    #endregion

    #region Unity Event Functions

    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().PlayerDelayedDeath();
            
            FindObjectOfType<PlayerController>().FreezePlayerOnDeath();
            
            spriteObject.SetActive(false);

            Invoke("EnemySpawn", timeUntilSpawn);

            thisCollider.enabled = !thisCollider.enabled;
            
            Debug.Log("Collided");
        }
    }

    private void EnemySpawn()
    {
        
        enemyObject.SetActive(true);
        
    }

    #endregion
}
