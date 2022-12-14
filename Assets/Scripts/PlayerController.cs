using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    private PlayerActionControls playerActionControls;

   
    #region Inspector

    [Header("Movement")]
    [Min(1f)]
    [SerializeField] private float movementSpeed;

    [Min(1f)]
    [SerializeField] private float jumpSpeed;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private float groundCheckRadius;

    [SerializeField] private GameObject fallDetector;

    private Rigidbody2D rb;

    private bool Grounded;

    private bool idleBool = false;

    private bool canMove = true;

    private bool canAttack = true;

    private Animator animator;

    private float direction = 0f;

    private int randomInt;

    private float nextTime;
    private float modifier;

    public Vector3 respawnPoint;
    public Vector3 playerDeathPoint;

    public Vector3 playerPosition;
    
    
    #endregion

    #region Unity Event Functions

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
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
        playerActionControls.Player.Jump.started += _ => Jump();
        playerActionControls.Player.Jump.canceled += _ => LazyJump();
        
        playerActionControls.Player.Attack.performed += _ => Attack();
        
        nextTime = Random.Range(2f, 6f);

        respawnPoint = transform.position;
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (canMove)
        {
            Movement();
        }

        GroundCheck();
       
        Animator();
       
        FallDetectorPosition();

        playerPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            FallRespawn();
            
            
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
            
        }
        else if (collision.tag == "InstaDeath")
        {
            
            
            canAttack = false;
            playerDeathPoint = transform.position;
            CheckpointRespawn();
            
        }
    }

    #endregion

    #region Movement

    private void Movement()
    {
        direction = playerActionControls.Player.Move.ReadValue<float>();

        if (direction > 0f)
        {
            rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (direction < 0f)
        {
            rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    private void Jump()
    {
        if (Grounded && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private void LazyJump()
    {

        if (rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2f);
            
        }
    }

    private void GroundCheck()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }


    #endregion

    #region Animation

    private void Animator()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("JumpSpeed", (rb.velocity.y));
        animator.SetBool("Grounded", Grounded);
        animator.SetBool("RandomTime", idleBool);
    }

    private void IdleAnimation()
    {
        
        randomInt = Random.Range(1, 35);
        if (randomInt <= 3)
        {
            idleBool = true;
        }
        else
        {
            return;
        }
    }

    private void EndIdleAnimation()
    {
        idleBool = false;

    }

    private void Attack()
    {
        if (Grounded && rb.velocity.x == 0f && canAttack)
        {
            animator.Play("ANIM_Player_Attack_Basic");
        }
        
    }
    
    #endregion

    #region Respawn

    private void FallDetectorPosition()
    {
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void FallRespawn()
    {
        canMove = false;
        
        FindObjectOfType<GameController>().FadeOut();

        Invoke("FallRespawnFadeIn", 2f);
    }

    private void FallRespawnFadeIn()
    {
        FindObjectOfType<GameController>().FadeIn();
        
        transform.position = respawnPoint;

        canMove = true;
    }
    
    private void CheckpointRespawn()
    {
        
        canMove = false;

        rb.velocity = new Vector2(0, 0);
        
        animator.SetTrigger("DeathTrigger");

        Invoke("DelayedFadeOut", 2f);

        Invoke("CheckpointRespawnFadeIn", 3.3f);
        
    }

    private void CheckpointRespawnFadeIn()
    {
        animator.Play("ANIM_Player_Idle");
        
        transform.position = respawnPoint;
        
        FindObjectOfType<GameController>().FadeIn();

        canMove = true;

        canAttack = true;
    }

    private void DelayedFadeOut()
    {
        FindObjectOfType<GameController>().FadeOut();
    }

    #endregion
  
}
