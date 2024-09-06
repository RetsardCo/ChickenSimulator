using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Queue queue;
    public int EnemyID;

    private void Awake()
    {
        GameObject QueueObj = GameObject.Find("Game");
        queue = QueueObj.GetComponent<Queue>();
        queue.EnemyQueue.Enqueue(EnemyID);
    }

    private void Start()
    {
        
    }
    public void AddToQueue()
    {
        queue.EnemyQueue.Enqueue(EnemyID);
    }

    //attack
    public void updateQueue()
    {
        if (queue.EnemyQueue.Count == 0)
        {
            return;
        }

        if (queue.EnemyQueue.Peek() == EnemyID)
        {
            Debug.Log(EnemyID);

            queue.EnemyQueue.Dequeue();
        } else
        {
            Debug.Log("You are not in queue");
        }
    }
}
