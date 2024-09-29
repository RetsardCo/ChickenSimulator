using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StorylineScript : MonoBehaviour
{
    [SerializeField] GameObject storylineCanvas;
    [SerializeField] GameObject nameReveal;
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField, Range(0.01f, 0.1f)] float textSpeed;
    [SerializeField] GameManager gameManager;

    [HideInInspector] public bool storyOngoing;
    [HideInInspector] public bool allTutorialsFinished;
    [HideInInspector] public bool lockedDialogue;
    [HideInInspector] public bool dialogueEnding;
    bool stopClicking;

    [SerializeField] string[] dayOneTutorialDialogues;
    int currentStory;

    [Header("Poop Cleaning Tutorial Dialogue")]
    [SerializeField] string[] poopCleanupDialogue;
    [SerializeField] string[] poopPickupDialogue;
    [HideInInspector] public bool needToCleanPoop;
    [HideInInspector] public int poopStory;

    [Header("Feeder Tutorial Dialogue")]
    [SerializeField] string[] feederTutorialDialogue;
    [SerializeField] string[] feederJustRightDialogue;
    [SerializeField] string[] feederTooMuchDialogue;
    [SerializeField] string[] feederTooLittleDialogue;
    [HideInInspector] public int feederStory;

    [Header("Drinker Tutorial Dialogue")]
    [SerializeField] string[] drinkerTutorialDialogue;
    [SerializeField] string[] drinkerJustRightDialogue;
    [SerializeField] string[] drinkerTooLittleDialogue;
    [SerializeField] string[] drinkerTooMuchDialogue;
    [HideInInspector] public int drinkerStory;

    [Header("Going Beyond Dialogue")]
    [SerializeField] string[] whereAreYouGoingDialogue;
    [HideInInspector] public int whereStory;

    string currentDialogue;

    [HideInInspector] public string whatLinesToDeliver;

    [SerializeField] SphereCollider detector;

    //string currentName;

    private void Awake() {
        nameReveal.SetActive(false);
        storyText.text = "";
        //acceptButton.gameObject.SetActive(false);
        //nameInput.gameObject.SetActive(false);
        storylineCanvas.SetActive(false);
        //gameOverScreen.SetActive(false);
    }

    private void Start() {
        ResetAllLines();
        storyOngoing = false;
        lockedDialogue = true;
        currentDialogue = string.Empty;
        nameReveal.SetActive(false);
        //StartCoroutine(TutorialStory());
    }

    private void Update() {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !gameManager.isInTransition && !stopClicking) {
            if (storyText.text == currentDialogue) {
                if (whatLinesToDeliver == "tutorial") {
                    if (currentStory == 5) {
                        StartCoroutine(MovementTutorial());
                        currentDialogue = string.Empty;
                        storyText.text = string.Empty;
                        currentStory++;
                    }
                    else if (currentStory == 7 || currentStory == 9) {
                        HideDialogue();
                        currentDialogue = string.Empty;
                        storyText.text = string.Empty;
                        currentStory++;
                    }
                    else if (currentStory > 9 && lockedDialogue) {
                        allTutorialsFinished = false;
                    }
                    else {
                        NextLine();
                    }
                }
                else if(whatLinesToDeliver == "poop"){
                    if (poopStory > poopCleanupDialogue.Length - 1) {
                        HideDialogue();
                    }
                    else {
                        //Debug.Log("I am Called.");
                        NextLine();
                    }
                }
                else {
                    Debug.Log("I am Called.");
                    NextLine();
                }
            }
            else {
                StopAllCoroutines();
            }
        }
    }

    void ResetAllLines() {
        currentStory = 0;
        poopStory = 0;
        drinkerStory = 0;
        feederStory = 0;
        whereStory = 0;
    }

    public IEnumerator TypeLine() {
        storyOngoing = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        storylineCanvas.SetActive(true);
        if (whatLinesToDeliver == "tutorial") {
            currentDialogue = dayOneTutorialDialogues[currentStory];
        }
        else if (whatLinesToDeliver == "poop") {
            currentDialogue = poopCleanupDialogue[poopStory];
        }
        else if (whatLinesToDeliver == "feeder") {
            currentDialogue = feederTutorialDialogue[feederStory];
        }
        else if (whatLinesToDeliver == "drinker") {
            currentDialogue = drinkerTutorialDialogue[drinkerStory];
        }
        else if (whatLinesToDeliver == "escape") {
            currentDialogue = whereAreYouGoingDialogue[whereStory];
        }
        else if (whatLinesToDeliver == "poopCleanUp") {
            poopStory = 0;
            currentDialogue = poopPickupDialogue[poopStory];
        }
        else if(whatLinesToDeliver == "feederRight") {
            feederStory = 0;
            currentDialogue = feederJustRightDialogue[feederStory];
        }
        else if (whatLinesToDeliver == "feederLow") {
            feederStory = 0;
            currentDialogue = feederTooLittleDialogue[feederStory];
        }
        else if (whatLinesToDeliver == "feederHigh") {
            feederStory = 0;
            currentDialogue = feederTooMuchDialogue[feederStory];
        }
        else if (whatLinesToDeliver == "drinkerRight") {
            drinkerStory = 0;
            currentDialogue = drinkerJustRightDialogue[feederStory];
        }
        else if (whatLinesToDeliver == "drinkerLow") {
            drinkerStory = 0;
            currentDialogue = drinkerTooLittleDialogue[feederStory];
        }
        else if (whatLinesToDeliver == "drinkerHigh") {
            drinkerStory = 0;
            currentDialogue = drinkerTooMuchDialogue[feederStory];
        }
        stopClicking = true;
        foreach (char c in currentDialogue.ToCharArray()) {
            storyText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        stopClicking = false;
    }

    void NextLine() {
        if (whatLinesToDeliver == "tutorial") {
            if (currentStory < dayOneTutorialDialogues.Length - 1) {
                if (lockedDialogue && !allTutorialsFinished) {

                }
                currentStory++;
            }
            else {
                HideDialogue();
                gameManager.SpecialCallEndOfDay();
            }

            if (currentStory == 2) {
                nameReveal.SetActive(true);
            }
            
        }
        else if (whatLinesToDeliver == "poop") {
            if (poopStory < poopCleanupDialogue.Length - 1) {
                poopStory++;
            }
            else {
                HideDialogue();
            }
        }
        else if (whatLinesToDeliver == "feeder") {
            if (feederStory < feederTutorialDialogue.Length - 1) {
                feederStory++;
            }
            else {
                HideDialogue();
            }

            if (feederStory == 3) {
                HideDialogue();
                //feederStory++;
            }
        }
        else if (whatLinesToDeliver == "drinker") {
            if (drinkerStory < drinkerTutorialDialogue.Length - 1) {
                drinkerStory++;
            }
            else {
                HideDialogue();
            }

            if (drinkerStory == 2) {
                HideDialogue();
                //drinkerStory++;
            }
        }
        else if (whatLinesToDeliver == "escape") {
            if (whereStory < whereAreYouGoingDialogue.Length - 1) {
                whereStory++;
            }
            else {
                currentDialogue = string.Empty;
                HideDialogue();
                //StartCoroutine(gameManager.ResetPlayerLocation(2.5f, false));
               //detector.enabled = true;
            }
        }
        else if (whatLinesToDeliver == "poopCleanUp") {
            if (poopStory < poopPickupDialogue.Length - 1) {
                poopStory++;
            }
            else {
                HideDialogue();
            }
        }
        else if (whatLinesToDeliver == "feederRight") {
            if (feederStory < feederJustRightDialogue.Length - 1) {
                feederStory++;
            }
            else {
                HideDialogue();
            }
        }
        else if (whatLinesToDeliver == "feederLow") {
            if (feederStory < feederTooLittleDialogue.Length - 1) {
                feederStory++;
            }
            else {
                HideDialogue();
            }
        }
        else if (whatLinesToDeliver == "feederHigh") {
            if (feederStory < feederTooMuchDialogue.Length - 1) {
                feederStory++;
            }
            else {
                HideDialogue();
            }
        }
        else if (whatLinesToDeliver == "drinkerRight") {
            if (drinkerStory < drinkerJustRightDialogue.Length - 1) {
                drinkerStory++;
            }
            else {
                HideDialogue();
            }
        }
        else if (whatLinesToDeliver == "drinkerLow") {
            if (drinkerStory < drinkerTooLittleDialogue.Length - 1) {
                drinkerStory++;
            }
            else {
                HideDialogue();
            }
        }
        else if (whatLinesToDeliver == "drinkerHigh") {
            if (drinkerStory < drinkerTooMuchDialogue.Length - 1) {
                drinkerStory++;
            }
            else {
                HideDialogue();
            }
        }
        storyText.text = string.Empty;
        StartCoroutine(TypeLine());
        
    }

    IEnumerator MovementTutorial() {
        HideDialogue();
        yield return new WaitForSeconds(5f);
        storyOngoing = true;
        storylineCanvas.SetActive(true);
        StartCoroutine(TypeLine());
    }

    void HideDialogue() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        storylineCanvas.SetActive(false);
        storyOngoing = false;
    }
}
