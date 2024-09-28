using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    bool cleanPoop;

    [SerializeField]
    [Range(0, 1)] float cutoffTime;

    [SerializeField] MinigameScript minigameScript;
    [SerializeField] GameManager gameManager;
    [SerializeField] StorylineScript storylineScript;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        savedSpeed = moveSpeed;
        collidedWithWall = false;
        cleanPoop = false;
    }

    void Update()
    {
        if (!gameManager.isPaused && !minigameScript.isInMinigame && !storylineScript.storyOngoing && !gameManager.isInTransition) {
            Move();
        }
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
        //Debug.Log(desiredMoveDirection);

        // Determine the direction based on camera's forward and right vectors
        if (collidedWithWall && animator.GetFloat("Speed") > cutoffTime) {
            desiredMoveDirection = Vector3.zero;
        }
        else {
            collidedWithWall = false;
        }
        //Debug.Log(moveHorizontal);
        //Debug.Log(moveVertical);

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

    private void OnTriggerEnter(Collider other) {
        
    }

    private void OnTriggerStay(Collider other) {
        /*if (other.CompareTag("Poop")) {
            
            if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log(other.tag);
                Destroy(other.gameObject);
                gameManager.PoopCount();
            }
            //Destroy(other);
        }*/
    }

    IEnumerator CleanPoop() {
        cleanPoop = true;
        yield return new WaitForSeconds(0.25f);
        cleanPoop = false;
    }

    void Animate()
    {
        // Calculate speed
        float speed = 0;

        // Update Animator parameters
        if (minigameScript.isInMinigame) {
            speed = 0f;
        }
        else {
            speed = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        }
        animator.SetFloat("Speed", speed);
        //animator.SetBool("IsMoving", speed > 0);
    }
}
