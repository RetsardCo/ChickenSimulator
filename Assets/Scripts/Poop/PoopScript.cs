using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopScript : MonoBehaviour
{
    Rigidbody rb;
    GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    StorylineScript storylineScript = GameObject.Find("Dialogue System").GetComponent<StorylineScript>();
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
       
    }

    private void OnDestroy() {
        GameObject.Find("Game Manager").GetComponent<GameManager>().PoopCount();
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ground")) {
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }

    public void DeletePoop() {
        if (gameManager.days == 1) {
            storylineScript.whatLinesToDeliver = "poopCleanUp";
            StartCoroutine(storylineScript.TypeLine());
        }
        Destroy(gameObject);
    }
}
