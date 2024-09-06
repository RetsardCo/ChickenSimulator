using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("GameMusic");
        if(gameObject.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
