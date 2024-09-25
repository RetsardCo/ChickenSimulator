using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkerScript : MonoBehaviour
{

    [SerializeField] int drinkerID;

    public void DrinkerCalled() {
        Debug.Log("ID of this drinker is: " + drinkerID);
        
    }
}
