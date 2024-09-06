using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int rewards;

    public Quest quest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(quest.questGoal.currentAmount);
        /*if (quest.questGoal.IsReached())
        {
            rewards += quest.reward;
            quest.Complete();
        }*/
    }

    
}
