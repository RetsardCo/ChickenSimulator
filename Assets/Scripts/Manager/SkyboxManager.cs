using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] float startTimer = 120f;
    [SerializeField] Material skyboxMaterial;
    [SerializeField] Light directionalLight;

    [SerializeField] MinigameScript minigameScript;

    Color currentSkyColor;
    Color currentFogColor;
    float exposureValue;
    float rotatingValue;

    [HideInInspector] public float timer;


    //Used for testing a possible timer for Day-Night Cycle
    //[SerializeField] Slider testSlider;

    private void Start() {
        skyboxMaterial = RenderSettings.skybox;
        currentSkyColor = skyboxMaterial.GetColor("_Tint");
        exposureValue = skyboxMaterial.GetFloat("_Exposure");
        currentSkyColor = new Color(0.8f, 0.8f, 1.0f, 1.0f);
        currentFogColor = new Color(0.2775f, 0.7642f, 0.6767f, 1.0f);
        directionalLight.intensity = 1.55f;
        exposureValue = 0.85f;
        skyboxMaterial.SetColor("_Tint", currentSkyColor);
        skyboxMaterial.SetFloat("_Exposure", exposureValue);
        //StartCoroutine(DayNight());
        //Debug.Log(skyboxMaterial.name);
    }

    public IEnumerator DayNight() {
        timer = startTimer;
        /*Color changeColor;
        changeColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);*/
        //testSlider.interactable = false;
        /*for (; ; ) {
            do {
                timer -= Time.deltaTime;
                DayNightCycle(timer / startTimer);
                yield return null;
            } while (timer > 0);
            do {
                timer += Time.deltaTime;
                DayNightCycle(timer / startTimer);
                yield return null;
            } while (timer < startTimer);
        }*/

        do {
            if (!minigameScript.isInMinigame) {
                timer -= Time.deltaTime;
                DayNightCycle(timer / startTimer);
                Debug.Log(timer);
            }
            yield return null;
        } while (timer > 0);
    }

    void DayNightCycle(float normalizedTime) {
        Color dayColor = new Color(0.8f, 0.8f, 1.0f, 1.0f);
        Color nightColor = new Color(0.05f, 0.05f, 0.2f, 1.0f);

        Color fogDay = new Color(0.2775f, 0.7642f, 0.6767f, 1.0f);
        Color fogNight = new Color(0.2291f, 0.3396f, 0.3200f, 1.0f);

        currentFogColor = Color.Lerp(fogNight, fogDay, normalizedTime);
        currentSkyColor = Color.Lerp(nightColor, dayColor, normalizedTime);
        exposureValue = Mathf.Lerp(0.25f, 0.85f, normalizedTime);
        rotatingValue = Mathf.Lerp(0, 360, normalizedTime);
        //Debug.Log(exposureValue);

        float rotationValue = Mathf.Lerp(-25f, 50f, normalizedTime);
        float intensityValue = Mathf.Lerp(0.25f, 1.55f, normalizedTime);
        directionalLight.intensity = intensityValue;
        Vector3 rotation = directionalLight.transform.rotation.eulerAngles;
        rotation.x = rotationValue;
        directionalLight.transform.rotation = Quaternion.Euler(rotation);

        RenderSettings.fogColor = currentFogColor;
        skyboxMaterial.SetColor("_Tint", currentSkyColor);
        skyboxMaterial.SetFloat("_Exposure", exposureValue);
        skyboxMaterial.SetFloat("_Rotation", rotatingValue);

        RenderSettings.skybox = skyboxMaterial;
    }
}
