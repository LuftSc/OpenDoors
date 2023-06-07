using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool compTaskOnInteract = false;
    public int taskID = 0;
    virtual public string GetInteractText()
    {
        return "";
    }

    virtual public void InteractAction()
    {
    }
    virtual public void completeTaskOnInteract(int Id)
    {
        if (compTaskOnInteract)
        {
            GameObject tasksList = GameObject.FindGameObjectWithTag("Tasks_UI");
            foreach (Transform rTask in tasksList.transform)
            {
                Task curTask = rTask.GetComponent<Task>();
                if (Id == curTask.Id)
                {
                    curTask.taskDone();
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
