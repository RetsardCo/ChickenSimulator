using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopScript : MonoBehaviour
{
    Rigidbody rb;

    //public bool playerDetected;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        //playerDetected = false;
    }

    private void Update() {
       /* if (Input.GetKeyDown(KeyCode.E) && playerDetected) {
            Debug.Log("Deleting Poop");
            Destroy(gameObject);
        }*/
    }

    private void OnDestroy() {
        GameObject.Find("Game Manager").GetComponent<GameManager>().PoopCount();
        //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().PoopCount();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ground")) {
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }

        /*if (other.gameObject.CompareTag("PlayerDetector")) {
            Debug.Log("Player Entered Poop");
            playerDetected = true;
        }*/
    }

    /*private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("PlayerDetector")) {
            Debug.Log("Player Exited Poop");
            playerDetected = true;
        }
    }*/

    public void DeletePoop() {
        Destroy(gameObject);
    }
}
