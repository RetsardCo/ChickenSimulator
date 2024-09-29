using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetectorScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] MinigameScript gameScript;
    [SerializeField] StorylineScript storylineScript;

    [HideInInspector]public FeederScript feederScript;
    [HideInInspector]public DrinkerScript drinkerScript;
    PoopScript poopScript;

    bool feederDetected;
    bool drinkerDetected;
    bool poopDetected;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (feederDetected && !feederScript.hasContent && !storylineScript.storyOngoing) {
                feederScript.FeederCalled();
                gameScript.MiniGameCalled("feeder");
            }
            else if (drinkerDetected && !drinkerScript.hasContent && !storylineScript.storyOngoing) {
                drinkerScript.DrinkerCalled();
                gameScript.MiniGameCalled("drinker");
            }
            else if (poopDetected && !storylineScript.storyOngoing) {
                poopScript.DeletePoop();
                poopScript = null;
                if (gameManager.days == 1 && !storylineScript.poopPickupTriggeredOnce) {
                    storylineScript.whatLinesToDeliver = "poopCleanUp";
                    StartCoroutine(storylineScript.TypeLine());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (storylineScript.storyOngoing) {
            return;
        }

        if (other.CompareTag("Feed")) {
            feederDetected = true;
            feederScript = other.GetComponent<FeederScript>();
            if (gameManager.days == 1 && !storylineScript.feederTriggeredOnce) {
                storylineScript.whatLinesToDeliver = "feeder";
                StartCoroutine(storylineScript.TypeLine());
            }
        }

        if (other.CompareTag("Drinker")) {
            drinkerDetected = true;
            drinkerScript = other.GetComponent<DrinkerScript>();
            if (gameManager.days == 1 && !storylineScript.drinkerTriggeredOnce) {
                storylineScript.whatLinesToDeliver = "drinker";
                StartCoroutine(storylineScript.TypeLine());
            }
        }

        if (other.CompareTag("Poop")) {
            poopDetected = true;
            poopScript = other.GetComponent<PoopScript>();
            if (gameManager.days == 1 && !storylineScript.poopPickupTriggeredOnce) {
                Debug.Log("I am Called");
                storylineScript.whatLinesToDeliver = "poop";
                StartCoroutine(storylineScript.TypeLine());
            }
        }

        if (other.CompareTag("ExitArea")) {
            //gameObject.GetComponent<SphereCollider>().enabled = false;
            storylineScript.whatLinesToDeliver = "escape";
            StartCoroutine(storylineScript.TypeLine());
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
