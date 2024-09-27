using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StorylineScript : MonoBehaviour
{
    /*[SerializeField] PlayerScript playerScript;
    [SerializeField] StatsController statsController;
    [SerializeField] ProteinAmountController proteinAmountController;*/

    [SerializeField] GameObject storylineCanvas;
    [SerializeField] GameObject nameReveal;
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] Button nextButton;
    [SerializeField] Button acceptButton;
    [SerializeField] TMP_InputField nameInput;

    [SerializeField] GameObject gameOverScreen;

    bool storyNext;
    bool storyOngoing;
    int currentStory;

    string currentName;

    private void Awake() {
        nameReveal.SetActive(false);
        storyText.text = "";
        acceptButton.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(false);
        storylineCanvas.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    private void Start() {
        StartCoroutine(StoryStart());
    }

    IEnumerator StoryStart() {
        currentStory = 0;
        yield return new WaitForSeconds(2f);
        storylineCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        storyOngoing = true;
        while (storyOngoing) {
            if (currentStory == 0) {
                storyText.text = "Hi! Welcome!";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 1) {
                storyText.text = "You must be a newcomer.";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 2) {
                storyText.text = "Well, you're in Mukimuki City, the largest gym in the City.";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 3) {
                storyText.text = "What's your name?";
                nextButton.gameObject.SetActive(false);
                yield return new WaitForSeconds(3f);
                nameInput.gameObject.SetActive(true);
                acceptButton.gameObject.SetActive(true);
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 4) {
                nameInput.gameObject.SetActive(false);
                acceptButton.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(true);
                //storyText.text = "So, you're " + statsController.playerName + ", huh?";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 5) {
                storyText.text = "My name is Dionathan, the most popular guy in this gym.";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 6) {
                nameReveal.SetActive(true);
                storyText.text = "Feel free to do whatever you want around here.";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 7) {
                storyText.text = "You can take a shot at my popularity, but who are we kidding here?";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 8) {
                storyText.text = "You? Taking a shot at my popularity?";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 9) {
                storyText.text = "...";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 10) {
                storyText.text = "I'm just bantering with you.";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 11) {
                storyText.text = "As a sign of good gesture, take these.";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 12) {
                storyText.text = "5 Regular Protein, and a Steroid. On the house.";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 13) {
                storyText.text = "Anyway, let's talk again later!";
                yield return new WaitForSeconds(1f);
            }
            else {
                storylineCanvas.SetActive(false);
                break;
            }
            yield return new WaitUntil(() => storyNext);
            currentStory++;
        }
    }

    public IEnumerator EndingStory() {
        storyText.text = "";
        currentStory = 0;
        yield return new WaitForSeconds(2f);
        storylineCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        storyOngoing = true;
        while (storyOngoing) {
            if (currentStory == 0) {
                //storyText.text = "You have come a long way, " + statsController.playerName + ".";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 1) {
                storyText.text = "Look at you! You have improved!";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 2) {
                storyText.text = "And you have out-popularized me!";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 3) {
                storyText.text = "Great job!";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 4) {
                storyText.text = "With that said, I'm stepping down my throne.";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 5) {
                storyText.text = "Enjoy your stay as the king of Mukimuki City!";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 6) {
                storyText.text = "Again, great job!";
                yield return new WaitForSeconds(1f);
            }
            else if (currentStory == 7) {
                storyText.text = "And Congratulations!";
                yield return new WaitForSeconds(1f);
            }
            else if(currentStory == 8) {
                storylineCanvas.SetActive(false);
                gameOverScreen.SetActive(true);
                break;
            }
            yield return new WaitUntil(() => storyNext);
            currentStory++;
        }
    }

    public void NextCalled() {
        StartCoroutine(ProceedStory());
    }

    public void BackToMainMenuCalled() {
        SceneManager.LoadScene("MainMenu");
    }

    public void AcceptCalled() {
        GrabFromInputField();
        if (currentName == "") {
            storyText.text = "I'm sure you have a name, right?";
            return;
        }
        else {
            //statsController.playerName = currentName;
        }
        StartCoroutine(ProceedStory());
    }

    private void GrabFromInputField() {
        currentName = nameInput.text;
    }

    IEnumerator ProceedStory() {
        storyNext = true;
        yield return new WaitForSeconds(0.5f);
        storyNext = false;
    }
}
