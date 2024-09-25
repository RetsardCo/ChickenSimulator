using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int days = 1;
    [HideInInspector] public int feedScore, drinkScore, cleanScore, medicateScore;
    [HideInInspector] public int feedMax, drinkMax, cleanMax, medicateMax;
    
    [Header("Daily Missions"), SerializeField]
    GameObject[] questLabels;

    [Header("Poop Goals"), SerializeField]
    SpawnPoop spawnPoop;

    [Header("Skybox and Time Limit"), SerializeField]
    SkyboxManager skyboxManager;

    [Header("Skybox and Time Limit"), SerializeField]
    GameObject missionsBox;

    [Header("Day Label")]
    [SerializeField] Image bg;
    [SerializeField] TextMeshProUGUI dayLabel;
    [SerializeField] Animator textAnimator;
    [SerializeField] Animator bgAnimator;

    [HideInInspector] public bool isPaused;

    List<string> daily;
    string[] missions;

    bool isMenuActive;

    private void Start() {
        isPaused = false;
        daily = new List<string>();
        missions = new string[] { "feed", "drink", "medicate", "clean"};
        missionsBox.SetActive(false);
        isMenuActive = false;
        StartCoroutine(GameCycle());

    }

    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.E)) {
            DayStart();
        }*/

        if (Input.GetKeyDown(KeyCode.M)) {
            if (!isMenuActive) {
                missionsBox.SetActive(true);
                isMenuActive = true;
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (isMenuActive) {
                missionsBox.SetActive(false);
                isMenuActive = false;
                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) {
                Time.timeScale = 0f;
                isPaused = true;
            }
            else if (isPaused) {
                Time.timeScale = 1f;
                isPaused = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F1)) {
            SceneManager.LoadScene("Layer");
        }
    }

    public void PoopCount() {
        cleanScore++;
        GoalFind("clean");
    }

    public void FeedCount() {
        feedScore++;
        GoalFind("feed");
    }

    public void DrinkCount() {
        drinkScore++;
        GoalFind("drink");
    }

    void GoalFind(string goal) {
        foreach (string dailies in daily) {
            if (goal == dailies) {
                Transform scoreTransform = questLabels[daily.IndexOf(goal)].transform.Find("current");
                if (scoreTransform != null) {
                    TextMeshProUGUI scoreText = scoreTransform.GetComponent<TextMeshProUGUI>();
                    if (scoreText != null) {
                        if (goal == "clean") {
                            scoreText.text = cleanScore.ToString();
                        }
                        else if (goal == "feed") {
                            scoreText.text = feedScore.ToString();
                        }
                        else if (goal == "drink") {
                            scoreText.text = drinkScore.ToString();
                        }
                        else if (goal == "medicate") {
                            scoreText.text = medicateScore.ToString();
                        }
                    }
                }
            }
        }
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
        dayLabel.text = "Day " + days.ToString();
        //textAnimator.SetTrigger("StartOfDayEnter");
        yield return new WaitForSeconds(5f);
        //textAnimator.SetTrigger("StartOfDayExit");
        //bgAnimator.SetTrigger("BGFadeOut");
        StartCoroutine(BGDisappear());
       yield return StartCoroutine(skyboxManager.DayNight());
        //Time.timeScale = 1f;
    }

    private IEnumerator EndOfDay() {
        missionsBox.SetActive(false);
        bg.enabled = true;
        dayLabel.text = "Day " + days.ToString() + " ended.";
        ResetAllScores();
        //textAnimator.SetTrigger("StartOfDayEnter");
        yield return new WaitForSeconds(2.5f);
        //bgAnimator.SetTrigger("BGFadeIn");
        yield return new WaitForSeconds(2.5f);
        //textAnimator.SetTrigger("StartOfDayExit");
        days++;
    }

    IEnumerator BGDisappear() {
        yield return new WaitForSeconds(1f);
        bg.enabled = false;
    }

    void DayStart() {
        daily.Clear();
        List<string> holder = new List<string>();
        ResetAllScores();
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
                *//*int j = i - 1;
                while (true) {
                    if (holder[j] == checker) {
                        checker = AddMissions();
                    }
                    else {
                        holder[i] = checker;
                        break;
                    }
                }*//*
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
            scoreTarget = feedMax.ToString();
        }
        else if (goal == "drink") {
            drinkMax = 1;
            scoreTarget = drinkMax.ToString();
        }
        else if (goal == "medicate") {
            medicateMax = Random.Range(1, 6);
            scoreTarget = medicateMax.ToString();
        }
        else if (goal == "clean") {
            /*poopManager.GetComponent<PoopManager>().Spawn();
            cleanScore = poopManager.GetComponent<PoopManager>().Poop();*/
            spawnPoop.PoopSpawn();
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
        foreach (GameObject go in questLabels) {
            Transform scoreTransform = go.transform.Find("current");
            if (scoreTransform != null) {
                TextMeshProUGUI scoreText = scoreTransform.GetComponent<TextMeshProUGUI>();
                if (scoreText != null) {
                    scoreText.text = 0.ToString();
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
