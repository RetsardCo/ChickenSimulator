using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandlerScript : MonoBehaviour
{
    public void CallGame() {
        SceneManager.LoadScene(1);
    }
}
