using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int days = 1;
    [HideInInspector] public int feedScore, drinkScore, cleanScore, medicateScore;
    [HideInInspector] public int feedMax, drinkMax, cleanMax, medicateMax;
    int totalGoalScore;
    int totalMaxScore;
    
    [Header("Daily Missions"), SerializeField]
    GameObject[] questLabels;

    [Header("Poop Goals"), SerializeField]
    SpawnPoop spawnPoop;

    [Header("Skybox and Time Limit"), SerializeField]
    SkyboxManager skyboxManager;

    [Header("Skybox and Time Limit"), SerializeField]
    GameObject missionsBox;

    [Header("Setting Panel"), SerializeField]
    GameObject settingsPanel;

    [Header("Shop Panel"), SerializeField]
    GameObject shopPanel;

    [Header("Inventory Panel"), SerializeField]
    GameObject inventoryPanel;

    [Header("Dev Text"), SerializeField]
    TextMeshProUGUI devLabel;
    [SerializeField] string devVersion;

    [Header("Day Label")]
    [SerializeField] Image bg;
    [SerializeField] TextMeshProUGUI dayLabel;
    [SerializeField] Animator textAnimator;
    [SerializeField] Animator bgAnimator;

    [HideInInspector] public bool isPaused;
    [HideInInspector] public bool isInTransition;
    [HideInInspector] public bool isMenuActive;
    bool isShopActive;
    bool isInventoryActive;

    [SerializeField] StorylineScript storylineScript;
    [SerializeField] PlayerDetectorScript playerDetectorScript;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform playerLocation;

    [SerializeField] bool isInDevelopment;
    bool mouseAppeared;

    [HideInInspector] public int goals;
    [HideInInspector] public bool isDayOneClear;
    [HideInInspector] public bool skipTheDay;

    [HideInInspector] public bool isEveryMissionDone;

    [SerializeField] AudioClip[] gameBGM;
    [SerializeField] AudioSource gameAudioSource;

    FeederScript[] feederScripts;
    DrinkerScript drinkerScript;

    List<string> daily;
    string[] missions;

    private void Start() {
        devLabel.text = devVersion;
        isPaused = false;
        isShopActive = false;
        isInventoryActive = false;
        mouseAppeared = false;
        daily = new List<string>();
        missions = new string[] { "feed", "drink",/* "medicate",*/ "clean"};
        missionsBox.SetActive(false);
        feederScripts = GameObject.FindObjectsOfType<FeederScript>();
        drinkerScript = GameObject.FindObjectOfType<DrinkerScript>();
        isMenuActive = false;
        StartCoroutine(GameCycle());
        StartCoroutine(GameBGMStart());
    }

    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.E)) {
            DayStart();
        }*/

        if (Input.GetKeyDown(KeyCode.M)) {
            if (!isMenuActive) {
                missionsBox.SetActive(true);
                isMenuActive = true;
                if (days == 1 && !storylineScript.menuAccessed) {
                    StartCoroutine(storylineScript.TypeLine());
                }
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (isMenuActive) {
                if (!storylineScript.storyOngoing) {
                    missionsBox.SetActive(false);
                    isMenuActive = false;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused && !storylineScript.storyOngoing) {
                PauseGame();
            }
            else if (isPaused) {
                UnpauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            if (!isInventoryActive && !isPaused) {
                CallInventory();
            }
            else if (isInventoryActive && !isPaused) {
                UncallInventory();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            if (!isShopActive && !isPaused) {
                CallShop();
            }
            else if (isShopActive && !isPaused) {
                UncallShop();
            }
        }

        if (Input.GetKeyDown(KeyCode.F1) && isInDevelopment) {
            SceneManager.LoadScene("MainGame");
        }

        if (Input.GetKeyDown(KeyCode.F2) && isInDevelopment) {
            if (!mouseAppeared) {
                mouseAppeared = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (mouseAppeared) {
                mouseAppeared = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void CallInventory() {
        inventoryPanel.SetActive(true);
        isMenuActive = true;
        Debug.Log("Call Inventory is Called");
        isInventoryActive = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UncallInventory() {
        inventoryPanel.SetActive(false);
        isMenuActive = false;
        Debug.Log("Uncall Inventory is Called");
        isInventoryActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CallShop(){
        shopPanel.SetActive(true);
        isMenuActive = true;
        Debug.Log("Call Shop is Called");
        Time.timeScale = 0f;
        isShopActive = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UncallShop() {
        shopPanel.SetActive(false);
        isMenuActive = false;
        Debug.Log("Uncaall Shop is Called");
        Time.timeScale = 1f;
        isShopActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame() {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void UnpauseGame() {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    private IEnumerator GameCycle() {
        for (; ; ) {
            yield return StartCoroutine(StartOfDay());
            yield return StartCoroutine(EndOfDay());
            yield return new WaitForSeconds(1f);
        }
        
    }

    private IEnumerator StartOfDay() {
        DayStart();
        missionsBox.SetActive(false);
        StartCoroutine(ResetPlayerLocation(0.1f, true));
        dayLabel.text = "Day " + days.ToString();
        isInTransition = true;
        textAnimator.SetTrigger("StartOfDayEnter");
        yield return new WaitForSeconds(5f);
        textAnimator.SetTrigger("StartOfDayExit");
        bgAnimator.SetTrigger("BGFadeOut");
        isInTransition = false;
        StartCoroutine(BGDisappear());
        if (days == 1) {
            yield return new WaitForSeconds(2f);
            storylineScript.whatLinesToDeliver = "tutorial";
            StartCoroutine(storylineScript.TypeLine());
        }
        if (days == 1) {
            StartCoroutine(skyboxManager.DayNight());
            yield return new WaitUntil(() => skipTheDay);
        }
        else {
            yield return StartCoroutine(skyboxManager.DayNight());
        }
        //Time.timeScale = 1f;
    }

    public void SpecialCallEndOfDay() {
        skipTheDay = true;
    }

    private IEnumerator EndOfDay() {
        missionsBox.SetActive(false);
        bg.enabled = true;
        dayLabel.text = "Day " + days.ToString() + " ended.";
        ResetAllScores();
        isInTransition = true;
        textAnimator.SetTrigger("StartOfDayEnter");
        yield return new WaitForSeconds(2.5f);
        bgAnimator.SetTrigger("BGFadeIn");
        yield return new WaitForSeconds(2.5f);
        textAnimator.SetTrigger("StartOfDayExit");
        days++;
    }

    void GoalFind(string goal) {
        foreach (string dailies in daily) {
            if (goal == dailies) {
                Transform scoreTransform = questLabels[daily.IndexOf(goal)].transform.Find("current");
                Transform checkmarkTransform = questLabels[daily.IndexOf(goal)].transform.Find("Checkmark");
                if (scoreTransform != null) {
                    TextMeshProUGUI scoreText = scoreTransform.GetComponent<TextMeshProUGUI>();
                    if (scoreText != null) {
                        if (goal == "clean") {
                            scoreText.text = cleanScore.ToString();
                            if (cleanScore == cleanMax) {
                                checkmarkTransform.gameObject.SetActive(true);
                                goals++;
                            }
                        }
                        else if (goal == "feed") {
                            scoreText.text = feedScore.ToString();
                            if (feedScore == feedMax) {
                                checkmarkTransform.gameObject.SetActive(true);
                                goals++;
                            }
                        }
                        else if (goal == "drink") {
                            scoreText.text = drinkScore.ToString();
                            if (drinkScore == drinkMax) {
                                checkmarkTransform.gameObject.SetActive(true);
                                goals++;
                            }
                        }
                        else if (goal == "medicate") {
                            scoreText.text = medicateScore.ToString();
                            if (medicateScore == medicateMax) {
                                checkmarkTransform.gameObject.SetActive(true);
                                goals++;
                            }
                        }
                    }
                }
                EndOfDayGoalsAchieved();
            }
        }
    }

     public void EndOfDayGoalsAchieved() {
        /*foreach (GameObject qLabels in questLabels) {
            Transform checkmark = qLabels.transform.Find("Checkmark");
            if (checkmark.gameObject.activeSelf) {
                goals++;
                Debug.Log("Current Goal Amount: " + goals);
            }
        }*/
        Debug.Log("Current Goal Amount: " + goals);
        if (goals == 3) {
            isEveryMissionDone = true;
            if (days == 1 && isDayOneClear) {
                storylineScript.lockedDialogue = false;
                storylineScript.whatLinesToDeliver = "tutorial";
                StartCoroutine(storylineScript.TypeLine());
            }
        }
    }

    public IEnumerator ResetPlayerLocation(float waitTime, bool comingFromGameManager) {
        if (!comingFromGameManager) {
            bg.enabled = true;
            bgAnimator.SetTrigger("BGFadeIn");
        }
        yield return new WaitForSeconds(waitTime);
        playerLocation.transform.position = spawnPoint.transform.position;
        if (!comingFromGameManager) {
            bgAnimator.SetTrigger("BGFadeOut");
            StartCoroutine(BGDisappear());
        }
    }
    IEnumerator GameBGMStart() {
        int bgmIndex = Random.Range(0, gameBGM.Length);
        yield return new WaitForSecondsRealtime(2f);

        for (; ; ) {
            gameAudioSource.clip = gameBGM[bgmIndex];
            gameAudioSource.Play();

            while (gameAudioSource.isPlaying) {
                yield return null;
            }

            if (bgmIndex == 1) {
                bgmIndex--;
            }
            else if (bgmIndex == 0) {
                bgmIndex++;
            }

            yield return new WaitForSecondsRealtime(5f);
        }
    }

    public void PoopCount() {
        cleanScore++;
        totalGoalScore++;
        GoalFind("clean");
    }

    public void FeedCount() {
        feedScore++;
        totalGoalScore++;
        GoalFind("feed");
    }

    public void DrinkCount() {
        drinkScore++;
        totalGoalScore++;
        GoalFind("drink");
    }

    void ResetFeederDrinker() {
        for (int i = 0; i < feederScripts.Length; i++) {
            feederScripts[i].hasContent = false;
        }
        drinkerScript.hasContent = false;
    }

    IEnumerator BGDisappear() {
        yield return new WaitForSeconds(1f);
        bg.enabled = false;
    }

    void DayStart() {
        ResetAllScores();
        daily.Clear();
        List<string> holder = new List<string>();
        isEveryMissionDone = false;
        ResetFeederDrinker();
        for(int i = 0; i < 3; i++) {
            /*string checker = AddMissions();
            if (i > 0) {
                foreach (string check in holder){
                    if (check == checker) {
                        while (true) {
                            checker = AddMissions();
                            if (checker != check) {
                                break;
                            }
                        }
                    }
                }
                holder.Add(checker);
                */
            /*int j = i - 1;
                while (true) {
                    if (holder[j] == checker) {
                        checker = AddMissions();
                    }
                    else {
                        holder[i] = checker;
                        break;
                    }
                }*/
            /*
            }
            else {
                holder.Add(checker);
            }*/

            string checker = AddMissions();
            while (holder.Contains(checker)) {
                checker = AddMissions();
            }
            holder.Add(checker);
        }
        foreach (string mission in holder) {
            Debug.Log(mission);
            daily.Add(mission);
        }
        QuestGive();
    }

    void QuestGive() {
        int i = 0;
        foreach (GameObject go in questLabels) {
            Transform titleTransform = go.transform.Find("title");
            if (titleTransform != null) {
                TextMeshProUGUI titleText = titleTransform.GetComponent<TextMeshProUGUI>();
                if (titleText != null) {
                    titleText.text = QuestTitle(daily[i]);
                }
            }

            Transform descriptionTransform = go.transform.Find("Description");
            if (descriptionTransform != null) {
                TextMeshProUGUI descriptionText = descriptionTransform.GetComponent<TextMeshProUGUI>();
                if (descriptionText != null) {
                    descriptionText.text = QuestDescription(daily[i]);
                }
            }

            Transform maxTransform = go.transform.Find("target");
            if (maxTransform != null) {
                TextMeshProUGUI maxText = maxTransform.GetComponent<TextMeshProUGUI>();
                if (maxText != null) {
                    maxText.text = QuestGoal(daily[i]);
                }
            }
            i++;
        }
    }

    string QuestTitle(string questTitle) {
        string title = "";
        if (questTitle == "feed") {
            title = "Feed the Chickens";
        }
        else if (questTitle == "drink") {
            title = "Let the Chicken Drink";
        }
        else if (questTitle == "medicate") {
            title = "Medicate the Chicken";
        }
        else if (questTitle == "clean") {
            title = "Clean Chicken Poops";
        }
        return title;
    }

    string QuestDescription(string questDescription) {
        string description = "";
        if (questDescription == "feed") {
            description = "The chicken feeds are empty. Refill the feeds to make them eat.";
        }
        else if (questDescription == "drink") {
            description = "The chicken drinkers are empty. Refill the chicken drinker to hydrate the chickens.";
        }
        else if (questDescription == "medicate") {
            description = "A chicken is sick. Give them medicine to make them healthy again.";
        }
        else if (questDescription == "clean") {
            description = "The place is littered with chicken poops. Clean them up to make your farm hygenic.";
        }
        return description;
    } 

    string QuestGoal(string goal) {
        string scoreTarget = "";
        if (goal == "feed") {
            feedMax = 2;
            totalMaxScore += feedMax;
            scoreTarget = feedMax.ToString();
        }
        else if (goal == "drink") {
            drinkMax = 1;
            totalMaxScore += drinkMax;
            scoreTarget = drinkMax.ToString();
        }
        else if (goal == "medicate") {
            medicateMax = Random.Range(1, 6);
            totalMaxScore += medicateMax;
            scoreTarget = medicateMax.ToString();
        }
        else if (goal == "clean") {
            /*poopManager.GetComponent<PoopManager>().Spawn();
            cleanScore = poopManager.GetComponent<PoopManager>().Poop();*/
            spawnPoop.PoopSpawn();
            cleanMax = spawnPoop.GivePoopNumber();
            totalMaxScore += cleanMax;
            scoreTarget = spawnPoop.GivePoopNumber().ToString();
            Debug.Log("Number to Spawn: " + scoreTarget);
            Debug.Log(cleanScore);
            //scoreTarget = cleanScore.ToString();
        }
        return scoreTarget;
    }

    void ResetAllScores() {
        feedScore = 0;
        drinkScore = 0;
        cleanScore = 0;
        medicateScore = 0;
        goals = 0;
        totalMaxScore = 0;
        skipTheDay = false;
        foreach (GameObject go in questLabels) {
            Transform scoreTransform = go.transform.Find("current");
            Transform checkmarkTransform = go.transform.Find("Checkmark");
            if (scoreTransform != null) {
                TextMeshProUGUI scoreText = scoreTransform.GetComponent<TextMeshProUGUI>();
                if (scoreText != null) {
                    scoreText.text = 0.ToString();
                }
            }
            if (checkmarkTransform != null) {
                GameObject checkMark = checkmarkTransform.gameObject;
                if (checkMark != null) {
                    checkMark.SetActive(false);
                }
            }
        }
    }

    string AddMissions() {
        int i = Random.Range(0, missions.Length);
        return missions[i];
    }

    public void MissionBoxAppear() {
        missionsBox.SetActive(true);
    }
}
