using com.nullproject.project1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class MinigameScript : MonoBehaviour
{
    [SerializeField] GameObject feedingMinigameScreen;
    [SerializeField] GameObject drinkingMinigameScreen;

    [SerializeField] StorylineScript storylineScript;
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerDetectorScript playerDetectorScript;

    [SerializeField] Button settingsButton;
    [SerializeField] Button missionsButton;
    //[SerializeField] Button inventoryButton;

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
    [SerializeField] float plusMinusThreshold;
    float minigameSpeed = 0.0f;
    float runningSpeed;
    float threshold;
    float minThreshold;
    float maxThreshold;

    [Header("Feeder Minigame Assets")]
    [SerializeField] RectTransform arrow;
    [SerializeField] RectTransform threeColors;
    [SerializeField] TextMeshProUGUI feederMinigameText;
    [SerializeField] TextMeshProUGUI feederInstructions;

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
    bool drinkerStop;

    [Header("Drinker Asset")]
    [SerializeField] Slider drinkerSlider;
    [SerializeField] RectTransform drinkerTarget;
    [SerializeField] TextMeshProUGUI drinkerMinigameText;
    [SerializeField] TextMeshProUGUI drinkerInstructions;

    [SerializeField]float beforeMinigameStarts;

    bool isSpacePressed;
    bool isFeederMinigame;
    bool isDrinkerMinigame;
    bool isFeederReady;
    bool isDrinkerReady;

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
        if (isFeederMinigame && isFeederReady) {
            FeederMinigame();
            if (arrow != null) {
                //Debug.Log(minigameSpeed);
                arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minArrowAngle, maxArrowAngle, minigameSpeed / maxArrowAngle));
            }
        }
        if (isDrinkerMinigame && isDrinkerReady) {
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
            threshold = Random.Range(maxArrowAngle + 25, minArrowAngle - 25);
            minThreshold = threshold - minThresholdAddition;
            maxThreshold = threshold + maxThresholdAddition;
            
            if (minThreshold < maxArrowAngle) {
                minThreshold = maxArrowAngle;
            }
            if (maxThreshold > minArrowAngle + 10) {
                maxThreshold = minArrowAngle;
            }
            threeColors.localEulerAngles = new Vector3(0, 0, threshold);
            feedingMinigameScreen.SetActive(true);
            if (gameManager.days == 1 && !storylineScript.feederTriggeredOnce) {
                storylineScript.whatLinesToDeliver = "feeder";
                StartCoroutine(storylineScript.TypeLine());
            }
        }
        else if (whatGame == "drinker") {
            isDrinkerMinigame = true;
            drinkerStop = false;
            targetAmount = Random.Range(minimumRandom, maximumRandom);

            if (targetAmount >= maximumRandom) {
                targetAmount = maximumRandom;
            }
            minimumTarget = targetAmount - addMinus;
            maximumTarget = targetAmount + addMinus;
            drinkerTarget.localPosition = new Vector3(targetPositionX, targetAmount, 0);
            drinkingMinigameScreen.SetActive(true);
            if (gameManager.days == 1 && !storylineScript.drinkerTriggeredOnce) {
                storylineScript.whatLinesToDeliver = "drinker";
                StartCoroutine(storylineScript.TypeLine());
            }
        }
        StartCoroutine(Waiting());
    }

    void HideButtons() {
        settingsButton.gameObject.SetActive(false);
        missionsButton.gameObject.SetActive(false);
        //inventoryButton.gameObject.SetActive(false);
    }

    void UnhideButtons() {
        settingsButton.gameObject.SetActive(true);
        missionsButton.gameObject.SetActive(true);
        //inventoryButton.gameObject.SetActive(true);
    }

    void FeederMinigame() {
        if (Input.GetKeyDown(KeyCode.Space) && isFeederReady) {
            Debug.Log("I am Called");
            isSpacePressed = true;
            StartCoroutine(ResultsDisplay());
        }

        if (!isSpacePressed) {
            runningSpeed += -(Time.deltaTime * speedMultiplier);
            minigameSpeed = runningSpeed * 3.6f;
        }

        if (!isSpacePressed && minigameSpeed <= maxArrowAngle) {
            Debug.Log("I am Called");
            isSpacePressed = true;
            StartCoroutine(ResultsDisplay());
        }
    }

    IEnumerator Waiting() {
        if (isFeederMinigame) {
            feederMinigameText.text = "Ready!";
            feederInstructions.text = "Fill the Feeder with just the right amount.";
            if (storylineScript.storyOngoing) {
                while (storylineScript.storyOngoing) {
                    yield return null;
                }
            }
            yield return new WaitForSeconds(beforeMinigameStarts);
            feederMinigameText.text = "Filling the Feeder...";
            feederInstructions.text = "Press the [Space] key at the right time.";
            isFeederReady = true;
        }
        if (isDrinkerMinigame) {
            drinkerMinigameText.text = "Ready!";
            drinkerInstructions.text = "Fill the Drinker with just the right amount.";
            if (storylineScript.storyOngoing) {
                while (storylineScript.storyOngoing) {
                    yield return null;
                }
            }
            yield return new WaitForSeconds(beforeMinigameStarts);
            drinkerMinigameText.text = "Filling the Drinker...";
            drinkerInstructions.text = "Hold the [Space] key and release it at the right time.";
            isDrinkerReady = true;
        }
    }

    void DrinkerMinigame() {
        if (Input.GetKey(KeyCode.Space) && !drinkerStop) {
            //Debug.Log(sliderSpeed);
            sliderIncreasing += Time.deltaTime * drinkerMultiplier;
            sliderSpeed = sliderIncreasing * 3.6f;
            
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            drinkerStop = true;
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
            feederInstructions.text = "";
            if (arrow.localEulerAngles.z > 0 && arrow.localEulerAngles.z < 90) {
                if (threeColors.localEulerAngles.z > 0 && threeColors.localEulerAngles.z < 90) {
                    if (arrow.localEulerAngles.z >= threeColors.localEulerAngles.z - minThresholdAddition && arrow.localEulerAngles.z <= threeColors.localEulerAngles.z - maxThresholdAddition) {
                        feederMinigameText.text = "Just Right!";
                    }
                    else if (arrow.localEulerAngles.z < threeColors.localEulerAngles.z - minThresholdAddition) {
                        feederMinigameText.text = "Way Too Much!";
                    }
                    else if ((arrow.localEulerAngles.z > threeColors.localEulerAngles.z + maxThresholdAddition)) {
                        feederMinigameText.text = "Way Too Few!";
                    }
                }
                else if ((threeColors.localEulerAngles.z > 270 && threeColors.localEulerAngles.z < 360)) {
                    feederMinigameText.text = "Way Too Few!";
                }
            }

            else if (arrow.localEulerAngles.z > 270 && arrow.localEulerAngles.z < 360) {
                if (threeColors.localEulerAngles.z > 0 && threeColors.localEulerAngles.z < 90) {
                    feederMinigameText.text = "Way Too Much!";
                }
                else if (threeColors.localEulerAngles.z > 270 && threeColors.localEulerAngles.z < 360) {
                    if (arrow.localEulerAngles.z >= threeColors.localEulerAngles.z - minThresholdAddition && arrow.localEulerAngles.z <= threeColors.localEulerAngles.z - maxThresholdAddition) {
                        feederMinigameText.text = "Just Right!";
                    }
                    else if (arrow.localEulerAngles.z < threeColors.localEulerAngles.z - minThresholdAddition) {
                        feederMinigameText.text = "Way Too Much!";
                    }
                    else if ((arrow.localEulerAngles.z > threeColors.localEulerAngles.z + maxThresholdAddition)) {
                        feederMinigameText.text = "Way Too Few!";
                    }
                }
            }

            if (gameManager.days == 1 && !storylineScript.feederTriggeredOnce) {
                if (feederMinigameText.text == "Just Right!") {
                    storylineScript.whatLinesToDeliver = "feederRight";
                }
                else if (feederMinigameText.text == "Way Too Much!") {
                    storylineScript.whatLinesToDeliver = "feederHigh";
                }
                else if (feederMinigameText.text == "Way Too Few!") {
                    storylineScript.whatLinesToDeliver = "feederLow";
                }
                yield return StartCoroutine(storylineScript.TypeLine());
            }
            isFeederReady = false;
            playerDetectorScript.feederScript.hasContent = true;
            gameManager.FeedCount();
            if (gameManager.days == 1 && storylineScript.storyOngoing) {
                yield return new WaitUntil(() => !storylineScript.storyOngoing);
            }
            yield return new WaitForSeconds(3.5f);
            feederInstructions.text = "Press Space to Continue...";
            //Debug.Log(isSpacePressed);
        }
        else if (isDrinkerMinigame) {
            drinkerInstructions.text = "";
            if (drinkerSlider.value >= minimumTarget && drinkerSlider.value <= maximumTarget) {
                drinkerMinigameText.text = "Just Right!";
            }
            else if (drinkerSlider.value < minimumTarget) {
                drinkerMinigameText.text = "Way Too Few!";
            }
            else if (drinkerSlider.value > maximumTarget) {
                drinkerMinigameText.text = "Way Too Much!";
            }
            if (gameManager.days == 1 && !storylineScript.drinkerTriggeredOnce) {
                if (drinkerMinigameText.text == "Just Right!") {
                    storylineScript.whatLinesToDeliver = "drinkerRight";
                }
                else if (drinkerMinigameText.text == "Way Too Much!") {
                    storylineScript.whatLinesToDeliver = "drinkerHigh";
                }
                else if (drinkerMinigameText.text == "Way Too Few!") {
                    storylineScript.whatLinesToDeliver = "drinkerLow";
                }
                yield return StartCoroutine(storylineScript.TypeLine());
            }
            playerDetectorScript.drinkerScript.hasContent = true;
            gameManager.DrinkCount();
            if (gameManager.days == 1 && storylineScript.storyOngoing) {
                yield return new WaitUntil(() => !storylineScript.storyOngoing);
            }
            yield return new WaitForSeconds(3.5f);
            drinkerInstructions.text = "Press Space to Continue...";
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        if (gameManager.goals == 3 && gameManager.days == 1) {
            gameManager.isDayOneClear = true;
            gameManager.EndOfDayGoalsAchieved();
        }
        //Debug.Log("Arrow Speed: " + minigameSpeed + " Max Arrow Angle: " + maxArrowAngle + " Is Arrow Speed <= maxArrowAngle?: " + (minigameSpeed <= maxArrowAngle));
        ResetValues();
        UnhideButtons();
    }

    void ResetValues() {
        depthOfField.active = false;
        isInMinigame = false;
        isFeederMinigame = false;
        isDrinkerMinigame = false;
        isDrinkerReady = false;
        isSpacePressed = false;
        maxReached = false;
        minigameSpeed = 0;
        runningSpeed = 0;
        minimumTarget = 0;
        maximumTarget = 0;
        targetAmount = 0;
        sliderSpeed = 0;
        sliderIncreasing = 0;
        arrow.localEulerAngles = new Vector3 (0 ,0 ,90);
        drinkerSlider.value = drinkerSlider.minValue;
        feedingMinigameScreen.SetActive(false);
        drinkingMinigameScreen.SetActive(false);
    }
}
