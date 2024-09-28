using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederScript : MonoBehaviour
{
    [SerializeField] int feederID;

    public void FeederCalled() {
        Debug.Log("ID of this feeder is: " + feederID); ;
    }
}
