using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float groundDrag;
    private float turnSmoothVelocity;
    public float turnSmoothTime;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    private bool _readyToJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public float groundCheckRadius;
    public Transform mainCam;
    public LayerMask groundMask;
    public float gravityMultiplier = 2f;

    [Header("Animator")]
    public Animator animator;
    private float animationSmooth = 0.05f;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Input")]
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 direction;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

    public MovementState movementState;

    public enum MovementState
    {
        walking,
        sprinting,
        idle
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    void Update()
    {
        HandleInput();
        SpeedControl();
        MovementStateHangler();
    }

    private void FixedUpdate()
    {
        HandleMovement();    
    }

    private void HandleInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

       _isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);

        if(Input.GetKeyDown(jumpKey) && _readyToJump && _isGrounded)
        {
            _readyToJump = false;
            
            animator.SetBool("isJumping", true);

            HandleJump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(_isGrounded)
        {
            _rigidbody.drag = groundDrag;
        }
        else
        {
            _rigidbody.drag = 0;
        }

    }

    private void MovementStateHangler()
    {
        if(_isGrounded && Input.GetKey(sprintKey) && direction.magnitude >= 0.1f)
        {
            movementState = MovementState.sprinting;
            moveSpeed = runSpeed;
            animator.SetFloat("Speed", 1, animationSmooth, Time.deltaTime);
        }
        else if(_isGrounded && direction.magnitude >= 0.1f)
        {
            movementState = MovementState.walking;
            moveSpeed = walkSpeed;
            animator.SetFloat("Speed", .5f, animationSmooth, Time.deltaTime);
        }
        else if(_isGrounded && direction.magnitude <= 0.1f)
        {
            animator.SetFloat("Speed", 0, animationSmooth, Time.deltaTime);
            movementState = MovementState.idle;
        }
    }

    private void HandleMovement()
    {
        
        direction = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;
        
        Vector3 gravityEffect = Physics.gravity * gravityMultiplier * Time.fixedDeltaTime;
        _rigidbody.velocity += gravityEffect;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_horizontalInput, _verticalInput) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _rigidbody.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }

    private void HandleJump()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        
        if(flatVelocity.magnitude >= moveSpeed)
        {
            Vector3 limitedVel = flatVelocity.normalized * moveSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }

    private void ResetJump()
    {
        _readyToJump = true;

        animator.SetBool("isJumping", false);
    }

}
