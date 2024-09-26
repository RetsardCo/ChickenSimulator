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

    [Header("Feeder Minigame Parameters")]
    [SerializeField] float minArrowAngle;
    [SerializeField] float maxArrowAngle;
    [SerializeField] float minThresholdAddition;
    [SerializeField] float maxThresholdAddition;
    [SerializeField, Range(2, 5)] float speedMultiplier;
    float minigameSpeed = 0.0f;
    float runningSpeed;

    float threshold;
    float minThreshold;
    float maxThreshold;

    [Header("Feeder Minigame Assets")]
    [SerializeField] RectTransform arrow;
    [SerializeField] RectTransform threeColors;

    [Header("Drinker Minigame Parameters")]
    [SerializeField] float minimumWater;
    [SerializeField] float maximumWater;
    [SerializeField] float minimumRandom;
    [SerializeField] float maximumRandom;
    [SerializeField] float addMinus;
    [SerializeField, Range(10, 20)] float drinkerMultiplier;
    [SerializeField] float targetPositionX;
    float sliderSpeed = 0.0f;
    float sliderIncreasing;
    float minimumTarget;
    float maximumTarget;
    float targetAmount;
    bool maxReached;

    [Header("Drinker Asset")]
    [SerializeField] Slider drinkerSlider;
    [SerializeField] RectTransform drinkerTarget;

    bool isSpacePressed;
    bool isFeederMinigame;
    bool isDrinkerMinigame;

    [HideInInspector] public bool isInMinigame;

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
        drinkingMinigameScreen.SetActive(false);
        drinkerSlider.minValue = minimumWater;
        drinkerSlider.maxValue = maximumWater;
        ResetValues();
        isInMinigame = false;
        //MiniGameCalled("drinker");
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
            if (!maxReached) {
                drinkerSlider.value = Mathf.Lerp(minimumWater, maximumWater, sliderSpeed / maximumWater);
            }
        }
    }

    public void MiniGameCalled(string whatGame) {
        HideButtons();
        isInMinigame = true;
        depthOfField.active = true;
        if (whatGame == "feeder") {
            isFeederMinigame = true;
            isSpacePressed = false;
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
            feedingMinigameScreen.SetActive(true);
        }
        //Debug.Log("Threshold: " + threshold + "Minimum Threshold: " + max);
        else if (whatGame == "drinker") {
            isDrinkerMinigame = true;
            targetAmount = Random.Range(minimumRandom, maximumRandom);

            if (targetAmount >= maximumRandom) {
                targetAmount = maximumRandom;
            }
            minimumTarget = targetAmount - addMinus;
            maximumTarget = targetAmount + addMinus;
            drinkerTarget.localPosition = new Vector3(targetPositionX, targetAmount, 0);
            drinkingMinigameScreen.SetActive(true);
        }
        //StartCoroutine(Waiting());
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
            runningSpeed += -(Time.deltaTime * speedMultiplier);
            minigameSpeed = runningSpeed * 3.6f;
        }

        if (!isSpacePressed && minigameSpeed <= maxArrowAngle) {
            arrow.localEulerAngles = new Vector3(0, 0, maxArrowAngle);
            StartCoroutine(ResultsDisplay());
        }
    }

    /*IEnumerator Waiting() {
        if (isFeederMinigame) {
            while (!isSpacePressed || minigameSpeed < maxArrowAngle) {
                yield return null;
            }
        }
        if (isDrinkerMinigame) {
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space) || maxReached);
        }
    }*/

    void DrinkerMinigame() {
        if (Input.GetKey(KeyCode.Space)) {
            //Debug.Log(sliderSpeed);
            sliderIncreasing += Time.deltaTime * drinkerMultiplier;
            sliderSpeed = sliderIncreasing * 3.6f;
            
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            StartCoroutine(ResultsDisplay());
        }

        if (sliderSpeed >= drinkerSlider.maxValue) {
            sliderSpeed = drinkerSlider.maxValue;
            maxReached = true;
            StartCoroutine(ResultsDisplay());
        }
    }

    IEnumerator ResultsDisplay() {
        if (isFeederMinigame) {
            if (arrow.localEulerAngles.z >= threeColors.localEulerAngles.z - minThresholdAddition && arrow.localEulerAngles.z <= threeColors.localEulerAngles.z - maxThresholdAddition) {
                Debug.Log("Just Right");
            }
            else if (arrow.localEulerAngles.z > minThreshold) {
                Debug.Log("Underfed");
            }
            else if (arrow.localEulerAngles.z < maxThreshold) {
                Debug.Log("Overfed");
            }
        }
        else if (isDrinkerMinigame) {
            if (drinkerSlider.value >= minimumTarget && drinkerSlider.value <= maximumTarget) {
                Debug.Log("Just Right");
            }
            else if (drinkerSlider.value < minimumTarget) {
                Debug.Log("Underhydrated");
            }
            else if (drinkerSlider.value > maximumTarget) {
                Debug.Log("Overhydrated");
            }
            
        }
        yield return new WaitForSeconds(3.5f);
        Debug.Log("Press Space");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        ResetValues();
        UnhideButtons();
    }

    void ResetValues() {
        depthOfField.active = false;
        isInMinigame = false;
        isFeederMinigame = false;
        isDrinkerMinigame = false;
        maxReached = false;
        minigameSpeed = 0;
        runningSpeed = 0;
        minimumTarget = 0;
        maximumTarget = 0;
        targetAmount = 0;
        sliderSpeed = 0;
        sliderIncreasing = 0;
        drinkerSlider.value = drinkerSlider.minValue;
        Debug.Log(drinkerSlider.value);
        feedingMinigameScreen.SetActive(false);
        drinkingMinigameScreen.SetActive(false);
    }

    /*IEnumerator FeederMinigame() {
        yield return StartCoroutine(ResultsDisplay());
    }

    IEnumerator DrinkerMinigame() {
        yield return StartCoroutine(ResultsDisplay());
    }*/
}
