using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();
    public int rewards;

    public GameObject questWindow;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text targetText;
    public TMP_Text currentAmountText;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);

        if (activeQuests.Count > 0)
        {
            Quest firstQuest = activeQuests[0];
            titleText.text = firstQuest.title;
            descriptionText.text = firstQuest.description;
            targetText.text = firstQuest.questGoal.requiredAmount.ToString();
            currentAmountText.text = firstQuest.questGoal.currentAmount.ToString();
        }
        else
        {
            titleText.text = "No Quest Available";
            descriptionText.text = "Please come back later.";
            targetText.text = "A";
            currentAmountText.text = "N";
        }
    }

    void Update()
    {
        if (activeQuests.Count > 0)
        {
            Quest firstQuest = activeQuests[0];

            if (firstQuest.questGoal.IsReached())
            {
                rewards += firstQuest.reward;
                firstQuest.Complete();
                activeQuests.RemoveAt(0); // Optionally remove completed quest
            }

            if (Click.poopClicked)
            {
                firstQuest.questGoal.poopCleaned();
                Click.poopClicked = false;
            }
        }
    }
}
