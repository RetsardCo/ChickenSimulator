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
    [HideInInspector] public bool menuAccessed;
    int currentStory;

    [Header("Poop Cleaning Tutorial Dialogue")]
    [SerializeField] string[] poopCleanupDialogue;
    [SerializeField] string[] poopPickupDialogue;
    //[HideInInspector] public bool needToCleanPoop;
    [HideInInspector] public int poopStory;
    [HideInInspector] public bool poopPickupTriggeredOnce;

    [Header("Feeder Tutorial Dialogue")]
    [SerializeField] string[] feederTutorialDialogue;
    [SerializeField] string[] feederJustRightDialogue;
    [SerializeField] string[] feederTooMuchDialogue;
    [SerializeField] string[] feederTooLittleDialogue;
    [HideInInspector] public int feederStory;
    [HideInInspector] public bool feederTriggeredOnce;

    [Header("Drinker Tutorial Dialogue")]
    [SerializeField] string[] drinkerTutorialDialogue;
    [SerializeField] string[] drinkerJustRightDialogue;
    [SerializeField] string[] drinkerTooLittleDialogue;
    [SerializeField] string[] drinkerTooMuchDialogue;
    [HideInInspector] public int drinkerStory;
    [HideInInspector] public bool drinkerTriggeredOnce;

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
        lockedDialogue = false;
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
                    else if (currentStory == 7) {
                        HideDialogue();
                        currentDialogue = string.Empty;
                        storyText.text = string.Empty;
                        currentStory++;
                    }
                    else if (currentStory == 9) {
                        if (!menuAccessed) {
                            HideDialogue();
                            currentDialogue = string.Empty;
                            storyText.text = string.Empty;
                            currentStory++;
                            menuAccessed = true;
                            lockedDialogue = true;
                        }
                    }
                    else if(!lockedDialogue){
                        NextLine();
                    }
                }
                else if (whatLinesToDeliver == "feeder") {
                    if (feederStory == 3) {
                        HideDialogue();
                        currentDialogue = string.Empty;
                        storyText.text = string.Empty;
                        feederStory++;
                    }
                    else {
                        NextLine();
                    }
                }
                else if (whatLinesToDeliver == "drinker") {
                    if (drinkerStory == 2) {
                        HideDialogue();
                        currentDialogue = string.Empty;
                        storyText.text = string.Empty;
                        drinkerStory++;
                    }
                    else {
                        NextLine();
                    }
                }
                else {
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
        poopPickupTriggeredOnce = false;
        feederTriggeredOnce = false;

    }

    public IEnumerator TypeLine() {
        if (!gameManager.isPaused) {
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
                if (!poopPickupTriggeredOnce) {
                    poopStory = 0;
                    poopPickupTriggeredOnce = true;
                }
                currentDialogue = poopPickupDialogue[poopStory];
            }
            else if (whatLinesToDeliver == "feederRight") {
                if (!feederTriggeredOnce) {
                    feederStory = 0;
                    feederTriggeredOnce = true;
                }
                currentDialogue = feederJustRightDialogue[feederStory];
            }
            else if (whatLinesToDeliver == "feederLow") {
                if (!feederTriggeredOnce) {
                    feederStory = 0;
                    feederTriggeredOnce = true;
                }
                currentDialogue = feederTooLittleDialogue[feederStory];
            }
            else if (whatLinesToDeliver == "feederHigh") {
                if (!feederTriggeredOnce) {
                    feederStory = 0;
                    feederTriggeredOnce = true;
                }
                currentDialogue = feederTooMuchDialogue[feederStory];
            }
            else if (whatLinesToDeliver == "drinkerRight") {
                if (!drinkerTriggeredOnce) {
                    drinkerStory = 0;
                    drinkerTriggeredOnce = true;
                }
                currentDialogue = drinkerJustRightDialogue[drinkerStory];
            }
            else if (whatLinesToDeliver == "drinkerLow") {
                if (!drinkerTriggeredOnce) {
                    drinkerStory = 0;
                    drinkerTriggeredOnce = true;
                }
                currentDialogue = drinkerTooLittleDialogue[drinkerStory];
            }
            else if (whatLinesToDeliver == "drinkerHigh") {
                if (!drinkerTriggeredOnce) {
                    drinkerStory = 0;
                    drinkerTriggeredOnce = true;
                }
                currentDialogue = drinkerTooMuchDialogue[drinkerStory];
            }
            stopClicking = true;
            foreach (char c in currentDialogue.ToCharArray()) {
                storyText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            stopClicking = false;
        }
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
                return;
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
                return;
            }
        }
        else if (whatLinesToDeliver == "feeder") {
            if (feederStory < feederTutorialDialogue.Length - 1) {
                feederStory++;
            }
            else {
                HideDialogue();
                return;
            }
        }
        else if (whatLinesToDeliver == "drinker") {
            if (drinkerStory < drinkerTutorialDialogue.Length - 1) {
                drinkerStory++;
            }
            else {
                HideDialogue();
                return;
            }
        }
        else if (whatLinesToDeliver == "escape") {
            if (whereStory < whereAreYouGoingDialogue.Length - 1) {
                whereStory++;
            }
            else {
                currentDialogue = string.Empty;
                storyText.text = string.Empty;
                HideDialogue();
                StartCoroutine(gameManager.ResetPlayerLocation(2.5f, false));
                return;
                //detector.enabled = true;
            }
        }
        else if (whatLinesToDeliver == "poopCleanUp") {
            if (poopStory < poopPickupDialogue.Length - 1) {
                poopStory++;
            }
            else {
                HideDialogue();
                return;
            }
        }
        else if (whatLinesToDeliver == "feederRight") {
            if (feederStory < feederJustRightDialogue.Length - 1) {
                feederStory++;
            }
            else {
                HideDialogue();
                return;
            }
        }
        else if (whatLinesToDeliver == "feederLow") {
            if (feederStory < feederTooLittleDialogue.Length - 1) {
                feederStory++;
            }
            else {
                HideDialogue();
                return;
            }
        }
        else if (whatLinesToDeliver == "feederHigh") {
            if (feederStory < feederTooMuchDialogue.Length - 1) {
                feederStory++;
            }
            else {
                HideDialogue();
                return;
            }
        }
        else if (whatLinesToDeliver == "drinkerRight") {
            if (drinkerStory < drinkerJustRightDialogue.Length - 1) {
                drinkerStory++;
            }
            else {
                HideDialogue();
                return;
            }
        }
        else if (whatLinesToDeliver == "drinkerLow") {
            if (drinkerStory < drinkerTooLittleDialogue.Length - 1) {
                drinkerStory++;
            }
            else {
                HideDialogue();
                return;
            }
        }
        else if (whatLinesToDeliver == "drinkerHigh") {
            if (drinkerStory < drinkerTooMuchDialogue.Length - 1) {
                drinkerStory++;
            }
            else {
                HideDialogue();
                return;
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
        if (!gameManager.isPaused) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            storyText.text = string.Empty;
            currentDialogue = string.Empty;
            storylineCanvas.SetActive(false);
            storyOngoing = false;
        }
    }
}
