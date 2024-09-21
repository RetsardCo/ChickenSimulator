using UnityEngine;
using TMPro;

public class GrassPerformanceManager : MonoBehaviour
{
    public Terrain terrain; // Reference to your terrain
    public float duration = 10f; // Time to calculate average FPS
    public int targetFPS = 30; // The FPS threshold to disable grass
    public float fadeDuration = 3f; // Time to fade out the grass
    public TextMeshProUGUI notificationText; // Reference to the TextMeshPro UI component
    private float fpsSum = 0f;
    private int frameCount = 0;
    private bool grassChecked = false; // To ensure grass check happens only once

    private void Start()
    {
        if (terrain == null)
        {
            terrain = Terrain.activeTerrain; // Get the active terrain
        }

        Invoke(nameof(CheckFPS), duration); // Start checking after 10 seconds
        notificationText.gameObject.SetActive(false); // Hide the notification text initially
    }

    private void Update()
    {
        if (!grassChecked)
        {
            // Accumulate FPS data during the first 10 seconds
            fpsSum += (1f / Time.deltaTime);
            frameCount++;
        }
    }

    private void CheckFPS()
    {
        float averageFPS = fpsSum / frameCount;
        Debug.Log("Average FPS after 10 seconds: " + averageFPS); // Log the average FPS

        if (averageFPS < targetFPS)
        {
            Debug.Log("FPS is below threshold. Starting to fade out the grass.");
            StartCoroutine(FadeOutGrass()); // Start fading out the grass
            ShowNotification(); // Show the warning message
        }

        grassChecked = true; // Ensure we don't check again
    }

    private System.Collections.IEnumerator FadeOutGrass()
    {
        float startDensity = terrain.detailObjectDensity;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            terrain.detailObjectDensity = Mathf.Lerp(startDensity, 0f, elapsedTime / fadeDuration); // Smooth fade out
            yield return null;
        }

        terrain.detailObjectDensity = 0f; // Ensure grass is fully disabled at the end
        Debug.Log("Grass fade-out completed.");
    }

private void ShowNotification()
{
    notificationText.gameObject.SetActive(true); // Show the notification text
    notificationText.text = "Unstable frame rate, switching to low graphics setting.";
    StartCoroutine(FadeOutNotification()); // Start fading out the text
}

private System.Collections.IEnumerator FadeOutNotification()
{
    float fadeDuration = 3f; // Duration for the text to fade out
    Color textColor = notificationText.color;
    float elapsedTime = 0f;

    while (elapsedTime < fadeDuration)
    {
        elapsedTime += Time.deltaTime;
        textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Fade to transparent
        notificationText.color = textColor;
        yield return null;
    }

    notificationText.gameObject.SetActive(false); // Hide the text after fading out
}

}
