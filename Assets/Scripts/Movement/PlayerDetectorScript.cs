using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetectorScript : MonoBehaviour
{
    [SerializeField]GameManager gameManager;
    [SerializeField]MinigameScript gameScript;

    FeederScript feederScript;
    DrinkerScript drinkerScript;
    PoopScript poopScript;

    bool feederDetected;
    bool drinkerDetected;
    bool poopDetected;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (feederDetected) {
                feederScript.FeederCalled();
                gameScript.MiniGameCalled("feeder");
            }
            else if (drinkerDetected) {
                drinkerScript.DrinkerCalled();
                gameScript.MiniGameCalled("drinker");
            }
            else if (poopDetected) {
                poopScript.DeletePoop();
                poopScript = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Feed")) {
            feederDetected = true;
            feederScript = other.GetComponent<FeederScript>();
        }

        if (other.CompareTag("Drinker")) {
            drinkerDetected = true;
            drinkerScript = other.GetComponent<DrinkerScript>();
        }

        if (other.CompareTag("Poop")) {
            poopDetected = true;
            poopScript = other.GetComponent<PoopScript>();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Feed")) {
            feederDetected = false;
            feederScript = null;
        }

        if (other.CompareTag("Drinker")) {
            drinkerDetected = false;
            drinkerScript = null;
        }

        if (other.CompareTag("Poop")) {
            poopDetected = false;
            poopScript = null;
        }
    }

    /*private void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (other.CompareTag("Poop")) {
                Debug.Log(other.tag);
                Destroy(other.gameObject);
                gameManager.PoopCount();
            }

            else if (other.CompareTag("Feed")) {
                Debug.Log("Feeding Minigame");
            }

            else if (other.CompareTag("Drinker")) {
                Debug.Log("Drinking Minigame");
            }
        }

        
    }*/

}
