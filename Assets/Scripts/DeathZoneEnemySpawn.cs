using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneEnemySpawn : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject enemyObject;

    [SerializeField] private GameObject spriteObject;

    private Collider2D thisCollider;

    #endregion

    #region Unity Event Functions

    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().PlayerDelayedDeath();
            
            spriteObject.SetActive(false);

            thisCollider.enabled = !thisCollider.enabled;
            
            enemyObject.SetActive(true);
            
            Debug.Log("Collided");
        }
    }

    #endregion
}
