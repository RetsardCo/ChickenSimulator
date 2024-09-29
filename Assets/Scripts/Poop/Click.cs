using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    public static bool poopClicked = false;
   // public QuestGoal questGoal;
    //public Quest quest;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Your code here (e.g., print the name of the clicked object)
                Debug.Log("Clicked on: " + hit.collider.name);
                Destroy(this.gameObject);
            }
        }*/
    }

    void FixedUpdate()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("naClicked");
        //Destroy(gameObject);
        //quest.questGoal.poopCleaned();
        
        //quest.questGoal.currentAmount++;

        Destroy(gameObject);
        /*if (quest.isActive)
        {
            Debug.Log("active na ni");
           
        }*/
        
    }

    private void OnDestroy()
    {
        poopClicked = true;
        //GameObject.Find("Game Manager").GetComponent<GameManager>().PoopCount();
        Debug.Log("destroyed");
    }

    private void OnMouseOver()
    {
        //Debug.Log("Nakatutok");
    }

    private void OnMouseUp()
    {
        //Debug.Log("Naclicked na");
    }
}
