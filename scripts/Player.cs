using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 12f;
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    Vector2 prevMovement;
    Vector2 movement;
    bool enableMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        enableMovement = true;
    }

    private void FixedUpdate()
    {
        if(enableMovement)
        {
            if (movement != null)
            {
                prevMovement = movement;
            }
            Move();
            UpdateAnimator();
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        movement.x = horizontalInput;
        movement.y = verticalInput;
        playerRigidBody.MovePosition(playerRigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateAnimator()
    {
        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Vertical", movement.y);
        playerAnimator.SetFloat("Speed", movement.sqrMagnitude);

        if (prevMovement.y > Mathf.Epsilon)
        {
            playerAnimator.SetBool("IsFacingUp", true);
        }
        else
        {
            playerAnimator.SetBool("IsFacingUp", false);
        }
    }

    public void Surrender()
    {
        enableMovement = false;
        playerAnimator.SetTrigger("Surrender");
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
