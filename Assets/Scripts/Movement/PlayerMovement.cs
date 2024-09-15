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

    Vector3 desiredMoveDirection;
    bool collidedWithWall;
    float moveHorizontal;
    float moveVertical;
    float savedSpeed;

    [SerializeField]
    [Range(0, 1)] float cutoffTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        savedSpeed = moveSpeed;
        collidedWithWall = false;
    }

    void Update()
    {
        Move();
        Animate();
        Debug.Log(collidedWithWall);
    }

    void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        // Get the forward direction of the camera and remove the y component to avoid upward/downward movement
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        right.Normalize();

        desiredMoveDirection = (forward * moveVertical + right * moveHorizontal).normalized;
        Debug.Log(desiredMoveDirection);

        // Determine the direction based on camera's forward and right vectors
        if (collidedWithWall && animator.GetFloat("Speed") > cutoffTime) {
            desiredMoveDirection = Vector3.zero;
        }
        else {
            collidedWithWall = false;
        }
        Debug.Log(moveHorizontal);
        Debug.Log(moveVertical);

        // Move the character
        rb.MovePosition(rb.position + (desiredMoveDirection * moveSpeed * Time.deltaTime));

        // Rotate the character towards the movement direction
        if (desiredMoveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        /*else if(desiredMoveDirection == Vector3.zero && collidedWithWall){
            collidedWithWall = false;
        }*/
    }

    private void OnCollisionEnter(Collision collision) {
        /*if (collision.gameObject.CompareTag("WallLeft")) {
            if (moveHorizontal < 0) {
                moveSpeed = 0;
                collidedWithWall = true;
            }
            else if ((moveHorizontal > 0 || moveVertical > 0 || moveVertical < 0) && collidedWithWall) {
                collidedWithWall = false;
                moveSpeed = savedSpeed;
            }
        }
        else if (collision.gameObject.CompareTag("WallRight")) {
            if (moveHorizontal > 0) {
                moveSpeed = 0;
            }
            else if (moveHorizontal < 0 || moveVertical > 0 || moveVertical < 0) {
                moveSpeed = savedSpeed;
            }
        }
        else if (collision.gameObject.CompareTag("WallTop")) {
            if (moveVertical > 0) {
                moveSpeed = 0;
            }
            else if (moveHorizontal > 0 || moveHorizontal < 0 || moveVertical < 0) {
                moveSpeed = savedSpeed;
            }
        }
        else if (collision.gameObject.CompareTag("WallBottom")) {
            if (moveVertical < 0) {
                moveSpeed = 0;
            }
            else if (moveHorizontal > 0 || moveHorizontal < 0 || moveVertical > 0) {
                moveSpeed = savedSpeed;
            }
        }*/

        if (collision.gameObject.CompareTag("Wall")) {
            collidedWithWall = true;
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
