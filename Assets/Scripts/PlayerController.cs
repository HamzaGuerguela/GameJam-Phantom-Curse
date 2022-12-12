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

    private Rigidbody2D rb;

    private bool Grounded;

    public bool randomTimer = false;

    private Animator animator;

    private float direction = 0f;

    private float nextTime;
    private float modifier;


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
        playerActionControls.Player.Jump.performed += _ => Jump();
        
        nextTime = 0.0f;
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
       Movement();

       GroundCheck();
       
       Animator();
       
       Timer();
    }

    #endregion

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
        if (Grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private void GroundCheck()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Animator()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("JumpSpeed", (rb.velocity.y));
        animator.SetBool("Grounded", Grounded);
        animator.SetBool("RandomTime", randomTimer);
    }

    private void Timer()
    {
        //Get a random value
        modifier = Random.Range(1.0f,3.0f);
     
        //Set nextTime equal to current run time plus modifier
        nextTime = Time.time + modifier;
 
        //Check if current system time is greater than nextTime
        if(Time.time > nextTime)
        {
            randomTimer = true;
            
        }
    }
}
