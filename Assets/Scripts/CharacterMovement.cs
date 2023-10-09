using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    Vector3 playerVelocity;
    Vector3 move;
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpHeight = 2;
    private float gravity = -9.18f;
    private bool doubleJump;
    private int jumpCount = 0;
    public float mouseSensitivity = 2.0f;
    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    public void Update()
    {
        if (animator.applyRootMotion == false)
        {
            ProcessMovement();
            ProcessGravity();
            UpdateRotation();
            Moving();
            doubleJump = GameManager.Instance.PowerUp;
        }
    }

    public void LateUpdate()
    {
        UpdateAnimator();
    }

    void DisableRootMotion()
    {
        animator.applyRootMotion = false;
    }

    void UpdateAnimator()
    {
        bool isGrounded = controller.isGrounded;
        if (move != Vector3.zero)
        {
            if (GetMovementSpeed() == runSpeed)
            {
                animator.SetFloat("Speed", 1.0f);
            }
            else
            {
                animator.SetFloat("Speed", 0.5f);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }

        animator.SetBool("isGrounded", isGrounded);
    }

    void UpdateRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0, Space.Self);
    }

    void ProcessMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        move = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;
    }

    void Moving()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 horizontalMovement = transform.forward * move.z + transform.right * move.x;

        transform.position = transform.position + horizontalMovement * GetMovementSpeed() * Time.deltaTime;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    public void ProcessGravity()
    {
        bool isGrounded = controller.isGrounded;
        animator.SetBool("DoubleJump", doubleJump);
        if (isGrounded)
        {
            jumpCount = 0;

            if (playerVelocity.y < 0.0f)
            {
                playerVelocity.y = -1.0f;
            }

            if (Input.GetButtonDown("Jump")) // Code to jump
            {
                jumpCount = 1;
                animator.SetInteger("JumpCount", jumpCount);
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
        else if (!isGrounded && doubleJump && jumpCount < 2) // if not grounded with power up
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpCount = 2;
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
                animator.SetBool("DoubleJump", doubleJump);
                animator.SetInteger("JumpCount", jumpCount);
                GameManager.Instance.JumpPowerUpOff();
            }
            else
            {
                playerVelocity.y += gravity * Time.deltaTime;
            }
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        controller.Move(move * Time.deltaTime * GetMovementSpeed() + playerVelocity * Time.deltaTime);
    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Fire3"))// Left shift
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }
}