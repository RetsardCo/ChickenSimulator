using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpeedometerBase : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float minArrowAngle;
    [SerializeField] float maxArrowAngle;
    [SerializeField] float multiplier;

    [SerializeField] TextMeshProUGUI speedLabel;
    [SerializeField] TextMeshProUGUI minimumThresholdText;
    [SerializeField] TextMeshProUGUI maximumThresholdText;
    [SerializeField] TextMeshProUGUI resultText;

    [SerializeField] RectTransform arrow;

    [SerializeField] bool isHoldMinigame;

    bool isSpacePressed;

    float speed = 0.0f;
    float runningSpeed;

    float threshold;
    float minThreshold;
    float maxThreshold;

    private void Start() {
        threshold = Random.Range(20, maxSpeed - 10);
        minThreshold = threshold - 10f;
        maxThreshold = threshold + 10f;

        if (minThreshold < 20) {
            minThreshold = 20;
        }

        if (maxThreshold > maxSpeed - 10) {
            maxThreshold = maxSpeed;
        }

        resultText.text = "Getting data...";
        minimumThresholdText.text = "Minimum Threshold: " + ((int)minThreshold).ToString() + " km/h";
        maximumThresholdText.text = "Maximum Threshold: " + ((int)maxThreshold).ToString() + " km/h";
        isSpacePressed = false;
    }

    private void Update() {
        if (isHoldMinigame) {
            Accelerate();
        }
        else if (!isHoldMinigame) {
            NonHoldAccelerate();
        }

        if (speedLabel != null) {
            speedLabel.text = ((int)speed).ToString() + " km/h";
        }
        if (arrow != null) {
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minArrowAngle, maxArrowAngle, speed/maxSpeed));
        }

        Debug.Log(speed);
    }

    private void Accelerate() {
        if (Input.GetKey(KeyCode.Space)) {
            runningSpeed += Time.deltaTime * multiplier;
            speed = runningSpeed * 3.6f;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            StartCoroutine(CheckResults());
        }

        if (speed >= maxSpeed) {
            speed = maxSpeed;
            StartCoroutine(CheckResults());
        }
    }

    private void NonHoldAccelerate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(CheckResults());
            isSpacePressed = true;
        }
        
        if(!isSpacePressed){
            runningSpeed += Time.deltaTime * multiplier;
            speed = runningSpeed * 3.6f;
        }

        if (!isSpacePressed && speed >= maxSpeed) {
            speed = maxSpeed;
            StartCoroutine(CheckResults());
        }
    }

    IEnumerator CheckResults() {
        if (speed >= minThreshold && speed <= maxThreshold) {
            resultText.text = "Success!";
        }
        else if (speed < minThreshold) {
            resultText.text = "Under!";
        }
        else if (speed > maxThreshold) {
            resultText.text = "Over!";
        }
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MiniGameExperimentScene");
    }
}
