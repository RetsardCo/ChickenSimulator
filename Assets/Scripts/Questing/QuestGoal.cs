using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType GoalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void poopCleaned()
    {
        if (GoalType == GoalType.Clean)
        {
            currentAmount++;
            Debug.Log(currentAmount);
        }
    }

    public void foodFed()
    {
        if (GoalType == GoalType.Feed)
            currentAmount++;
    }
}

public enum GoalType
{
    Clean,
    Feed
}
