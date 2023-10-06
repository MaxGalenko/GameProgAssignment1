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
    public float gravity = -9.18f;
    public bool doubleJump = GameManager.Instance.IsJumpPowerUp();
    private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (animator.applyRootMotion == false)
        {
            ProcessMovement();
            ProcessGravity();
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

    void ProcessMovement()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    public void ProcessGravity()
    {
        bool isGrounded = controller.isGrounded;
        animator.SetBool("DoubleJump", false);

        if (isGrounded)
        {
            if (playerVelocity.y < 0.0f)
            {
                playerVelocity.y = -1.0f;
            }

            if (Input.GetButtonDown("Jump")) // Code to jump
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
        else if (!isGrounded && !doubleJump) // if not grounded with power up
        {
            if (Input.GetButtonDown("Jump"))
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
                animator.SetBool("DoubleJump", true);
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