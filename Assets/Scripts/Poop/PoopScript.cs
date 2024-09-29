using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopScript : MonoBehaviour
{
    Rigidbody rb;
    GameManager gameManager;
    StorylineScript storylineScript;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        /*gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        storylineScript = GameObject.Find("Dialogue System").GetComponent<StorylineScript>();*/
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
        Destroy(gameObject);
    }
}
