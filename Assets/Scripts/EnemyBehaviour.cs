using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private const string LEFT = "reft";
    private const string RIGHT = "right";
    
    #region Inspector
    

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Transform castPos;

    [SerializeField] private float baseCastDist;

    [SerializeField] private int enemyHealth;

    private Rigidbody2D rb2d;

    private string facingDirection;

    private Vector3 baseScale;

    private Animator anim;

    public GameObject deathZoneObj;

    private GameObject enemyObject;

    private SpriteRenderer spriteRendererEnemy;
    

    #endregion

    #region Unity Event Functions

    private void Start()
    {
        baseScale = transform.localScale;
        
        facingDirection = RIGHT;

        rb2d = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        
        deathZoneObj.SetActive(true);

        enemyObject = this.gameObject;

        spriteRendererEnemy = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float vX = moveSpeed;

        if (facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }
        
        // Move the game object.
        rb2d.velocity = new Vector2(vX, rb2d.velocity.y);


        if (IsHittingWall() || IsNearEdge())
        {
            if (facingDirection == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else if (facingDirection == RIGHT)
            {
                ChangeFacingDirection(LEFT);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerAttack"))
        {
            deathZoneObj.SetActive(false);
            
            spriteRendererEnemy.color = Color.red;
            
            anim.Play("ANIM_Enemy_Death");
            
            spriteRendererEnemy.color = Color.white;
        }
    }

    private void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;

    }

    private bool IsHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;
        // Define the cast distance for left and right.
        if (facingDirection == LEFT)
        {
            castDist = -baseCastDist;

        }
        else
        {
            castDist = baseCastDist;
        }

        // Determine the target destination based on the cast distance.
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        
        Debug.DrawLine(castPos.position, targetPos, Color.cyan);
        
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }
        
        return val;
    }
    
    private bool IsNearEdge()
    {
        bool val = true;

        float castDist = baseCastDist;
        // Define the cast distance for left and right.

        // Determine the target destination based on the cast distance.
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        
        Debug.DrawLine(castPos.position, targetPos, Color.red);
        
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }
        
        return val;
    }

    private void DeathAnimationEvent()
    {
        Destroy(enemyObject);
    }
    
    #endregion
}
