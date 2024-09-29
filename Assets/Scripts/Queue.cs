using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor.Search;
using UnityEngine;

public class Queue : MonoBehaviour
{
    public Queue<int> EnemyQueue = new Queue<int>();

    private Test test;

    private void Awake()
    {
        GameObject EnemyObj = GameObject.Find("GameObject");
        test = EnemyObj.GetComponent<Test>();
    }

    private void Start()
    {
        Debug.Log(EnemyQueue.Count);
        Debug.Log(EnemyQueue.Peek());
    }
}
