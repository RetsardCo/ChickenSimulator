using com.nullproject.project1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MinigameScript : MonoBehaviour
{
    [SerializeField] GameObject feedingMinigameScreen;
    [SerializeField] GameObject drinkingMinigameScreen;

    [SerializeField] Button settingsButton;
    [SerializeField] Button missionsButton;
    [SerializeField] Button inventoryButton;

    [SerializeField] Volume globalVolume;

    DepthOfField depthOfField;

    DrinkerScript drinkerScript;
    FeederScript feederScript;

    [Header("Minigame Parameters")]
    [SerializeField] float minArrowAngle;
    [SerializeField] float maxArrowAngle;
    [SerializeField] float minThresholdAddition;
    [SerializeField] float maxThresholdAddition;
    [SerializeField, Range(2, 5)] float speedMultiplier;

    [Header("Feeder Minigame Assets")]
    [SerializeField] RectTransform arrow;
    [SerializeField] RectTransform threeColors;

    bool isSpacePressed;
    bool isFeederMinigame;
    bool isDrinkerMinigame;

    [HideInInspector] public bool isInMinigame;

    float minigameSpeed = 0.0f;
    float runningSpeed;

    float threshold;
    float minThreshold;
    float maxThreshold;

    private void Awake() {
        if (globalVolume != null && globalVolume.profile != null) {
            if (!globalVolume.profile.TryGet(out depthOfField)) {
                Debug.LogError("Depth of Field not found in the Volume Profile!");
            }
        }
        depthOfField.active = false;
    }

    private void Start() {
        feedingMinigameScreen.SetActive(false);
        //drinkingMinigameScreen.SetActive(false);
        isInMinigame = false;
        //MiniGameCalled("feeder");
    }

    private void Update() {
        if (isFeederMinigame) {
            FeederMinigame();
            if (arrow != null) {
                //Debug.Log(minigameSpeed);
                arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minArrowAngle, maxArrowAngle, minigameSpeed / maxArrowAngle));
            }
        }
        if (isDrinkerMinigame) {
            DrinkerMinigame();
        }
    }

    public void MiniGameCalled(string whatGame) {
        HideButtons();
        isInMinigame = true;
        depthOfField.active = true;
        if (whatGame == "feeder") {
            threshold = Random.Range(maxArrowAngle + maxThresholdAddition, minArrowAngle - minThresholdAddition);
            minThreshold = threshold - 10f;
            maxThreshold = threshold + 10f;
            
            if (minThreshold < maxArrowAngle) {
                minThreshold = maxArrowAngle;
            }
            if (maxThreshold > minArrowAngle + 10) {
                maxThreshold = minArrowAngle;
            }
            threeColors.localEulerAngles = new Vector3(0, 0, threshold);
            isFeederMinigame = true;
            isSpacePressed = false;
            feedingMinigameScreen.SetActive(true);
        }
        //Debug.Log("Threshold: " + threshold + "Minimum Threshold: " + max);
        else if (whatGame == "drinker") {
            isDrinkerMinigame = true;
        }
        StartCoroutine(Waiting());
    }

    void HideButtons() {
        settingsButton.gameObject.SetActive(false);
        missionsButton.gameObject.SetActive(false);
        inventoryButton.gameObject.SetActive(false);
    }

    void UnhideButtons() {
        settingsButton.gameObject.SetActive(true);
        missionsButton.gameObject.SetActive(true);
        inventoryButton.gameObject.SetActive(true);
    }

    void FeederMinigame() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            isSpacePressed = true;
            StartCoroutine(ResultsDisplay());
        }

        if (!isSpacePressed) {
            Debug.Log("I am called");
            runningSpeed += -(Time.deltaTime * speedMultiplier);
            minigameSpeed = runningSpeed * 3.6f;
        }

        if (!isSpacePressed && minigameSpeed <= maxArrowAngle) {
            arrow.localEulerAngles = new Vector3(0, 0, maxArrowAngle);
            StartCoroutine(ResultsDisplay());
        }
    }

    IEnumerator Waiting() {
        if (isFeederMinigame) {
            while (!isSpacePressed || minigameSpeed < maxArrowAngle) {
                yield return null;
            }
        }
    }

    void DrinkerMinigame() {

    }

    IEnumerator ResultsDisplay() {
        yield return null;
        if (arrow.localEulerAngles.z >= threeColors.localEulerAngles.z - minThresholdAddition && arrow.localEulerAngles.z <= threeColors.localEulerAngles.z - maxThresholdAddition) {
            Debug.Log("Just Right");
        }
        else if (arrow.localEulerAngles.z > minThreshold) {
            Debug.Log("Underfed");
        }
        else if (arrow.localEulerAngles.z < maxThreshold) {
            Debug.Log("Overfed");
        }
        ResetValues();
        UnhideButtons();
    }

    void ResetValues() {
        depthOfField.active = false;
        isInMinigame = false;
        isFeederMinigame = false;
        isDrinkerMinigame = false;
        minigameSpeed = 0;
        runningSpeed = 0;
        feedingMinigameScreen.SetActive(false);
    }

    /*IEnumerator FeederMinigame() {
        yield return StartCoroutine(ResultsDisplay());
    }

    IEnumerator DrinkerMinigame() {
        yield return StartCoroutine(ResultsDisplay());
    }*/
}
