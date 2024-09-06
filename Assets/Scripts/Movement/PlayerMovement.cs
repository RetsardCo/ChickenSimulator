using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;
    public Camera playerCamera;

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Animate();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Get the forward direction of the camera and remove the y component to avoid upward/downward movement
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        right.Normalize();

        // Determine the direction based on camera's forward and right vectors
        Vector3 desiredMoveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        // Move the character
        rb.MovePosition(transform.position + desiredMoveDirection * moveSpeed * Time.deltaTime);

        // Rotate the character towards the movement direction
        if (desiredMoveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    void Animate()
    {
        // Calculate speed
        float speed = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        // Update Animator parameters
        animator.SetFloat("Speed", speed);
        //animator.SetBool("IsMoving", speed > 0);
    }
}
