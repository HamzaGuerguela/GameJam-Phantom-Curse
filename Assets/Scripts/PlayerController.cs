using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerActionControls playerActionControls;

   
    #region Inspector

    [SerializeField] private float movementSpeed;

    [SerializeField] private float jumpSpeed;

    [SerializeField] private LayerMask ground;

    private Rigidbody2D rb;

    private Collider2D col;

    private bool facingRight = false;

    private Animator animator;
    
    
    #endregion

    #region Unity Event Functions

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();

        rb = GetComponent<Rigidbody2D>();

        col = GetComponent<Collider2D>();

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
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
       
    }

    #endregion

    private void Movement()
    {
        // Read the movement value
        float movementInput = playerActionControls.Player.Move.ReadValue<float>();
        // Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * movementSpeed * Time.deltaTime;
        transform.position = currentPosition;

        
        // Flipping Player
        if (movementInput < 0f)
        {
            facingRight = true;
        }
        else if (movementInput > 0f)
        {
            facingRight = false;
        }

        if (!facingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (facingRight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
        // Animation
        if (movementInput != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            Debug.Log("test");
            animator.SetBool("Jump", true);
        }
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x * 0.9f;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += col.bounds.extents.x * 0.9f;
        bottomRight.y -= col.bounds.extents.y;
        
        return Physics2D.OverlapArea(topLeftPoint, bottomRight, ground);
        
    }
    
}
