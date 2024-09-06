using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    [SerializeField]
    private GameObject IntroCanvas;

    public void IntroCanvasDisable()
    {
        IntroCanvas.SetActive(false);
    }
}
