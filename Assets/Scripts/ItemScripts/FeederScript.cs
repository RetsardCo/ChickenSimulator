using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederScript : MonoBehaviour
{
    [SerializeField] int feederID;
    [HideInInspector] public bool hasContent;

    public void FeederCalled() {
        Debug.Log("ID of this feeder is: " + feederID);
    }
}
