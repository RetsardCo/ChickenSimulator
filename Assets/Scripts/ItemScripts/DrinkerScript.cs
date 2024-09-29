using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkerScript : MonoBehaviour
{

    [SerializeField] int drinkerID;
    [HideInInspector] public bool hasContent;

    public void DrinkerCalled() {
        Debug.Log("ID of this drinker is: " + drinkerID);
        
    }
}
