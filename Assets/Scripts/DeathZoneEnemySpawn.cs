using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneEnemySpawn : MonoBehaviour
{
    #region Inspector

    [SerializeField] private GameObject enemyObject;

    [SerializeField] private float timeUntilSpawn;

    private Collider2D thisCollider;

    private Animator animator;
    
    #endregion

    #region Unity Event Functions

    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();

       animator = gameObject.GetComponent<Animator>();
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

            DeactivateSprite();

            Invoke("EnemySpawn", timeUntilSpawn);

            thisCollider.enabled = !thisCollider.enabled;
            
            Debug.Log("Collided");
        }
    }

    private void EnemySpawn()
    {
        
        enemyObject.SetActive(true);
        
    }

    private void DeactivateSprite()
    {
        animator.Play("ANIM_Enemy_Collider_Spawner");
        
        
    }

    public void AnimationEvent()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    
    #endregion
}
