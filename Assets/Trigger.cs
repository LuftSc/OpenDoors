using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [System.Serializable]
    public class AnimEntry
    {
        public GameObject objectToAnimate = null;
        public string AnimatorState;
        public int State;
        public bool isBool;
        public float animationDelay = 0f;
    }
    [System.Serializable]
    public class ObjectEntry
    {
        public GameObject gameObj;
        public float switchActiveDelay = 0f;
    }
    [System.Serializable]
    public class TaskEntry
    {
        public string TaskText;
        public int Id;
        public float assignDelay = 0f;
    }
    [System.Serializable]
    public class CompleteTaskEntry
    {
        public int Id;
        public float completeDelay = 0f;
    }

    [System.Serializable]
    public class DoorEntry
    {
        public GameObject door;
        public bool isLocked;
        public bool changeState = false;
        public bool isOpen = false;
    }
    public AnimEntry[] animatorStateList = null;

    public ObjectEntry[] objectsToSwitch = null;

    public GameObject taskPrefab = null;
    public TaskEntry[] tasksToAssign = null;

    public CompleteTaskEntry[] tasksToComplete = null;
    private bool isTriggerFired = false;

    public DoorEntry[] doorsToChangeState;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator startAnimWithDelay(AnimEntry anim)
    {
        yield return new WaitForSeconds(anim.animationDelay);
        Animator animator = anim.objectToAnimate.GetComponent<Animator>();
        if (animator)
        {
            if (anim.isBool)
            {
                animator.SetBool(anim.AnimatorState, anim.State == 1);
            }
            else
            {
                animator.SetInteger(anim.AnimatorState, anim.State);
            }
        }
    }

    IEnumerator switchActiveWithDelay(ObjectEntry obj)
    {
        yield return new WaitForSeconds(obj.switchActiveDelay);
        obj.gameObj.gameObject.SetActive(!obj.gameObj.gameObject.activeSelf);
    }

    IEnumerator completeTaskWithDelay(CompleteTaskEntry compTask)
    {
        yield return new WaitForSeconds(compTask.completeDelay);
        GameObject tasksList = GameObject.FindGameObjectWithTag("Tasks_UI");
        foreach (Transform rTask in tasksList.transform)
        {
            Task curTask = rTask.GetComponent<Task>();
            if (compTask.Id == curTask.Id)
            {
                curTask.taskDone();
            }
        }
    }

    public IEnumerator assignTaskWithDelay(TaskEntry task)
    {
        if(task.assignDelay != 0f)
            yield return new WaitForSeconds(task.assignDelay);
        GameObject tasksList = GameObject.FindGameObjectWithTag("Tasks_UI");
        GameObject newTask = Instantiate(taskPrefab, tasksList.transform);
        Task newTaskScript = newTask.GetComponent<Task>();
        newTaskScript.Id = task.Id;
        newTaskScript.setTaskText(task.TaskText);
    }
    public IEnumerator changeDoorState(DoorEntry doorEntry)
    {   
        Door doorScript = doorEntry.door.gameObject.GetComponentInChildren<Door>();
        doorScript._isLocked = doorEntry.isLocked;
        if(doorEntry.changeState == true)
        {
            doorScript.changeDoorState(doorEntry.isOpen);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggerFired)
        {
            isTriggerFired = true;
            foreach (AnimEntry anim in animatorStateList)
            {
                    StartCoroutine(startAnimWithDelay(anim));
            }
            if (objectsToSwitch.Length != 0)
            {
                foreach (ObjectEntry obj in objectsToSwitch)
                {
                    StartCoroutine(switchActiveWithDelay(obj));
                }
            }
            if (tasksToComplete.Length != 0)
            {
                foreach (CompleteTaskEntry compTask in tasksToComplete)
                {
                    StartCoroutine(completeTaskWithDelay(compTask));
                }
            }
            if (tasksToAssign.Length != 0)
            {
                GameObject tasksList = GameObject.FindGameObjectWithTag("Tasks_UI");
                foreach (TaskEntry task in tasksToAssign)
                {
                    StartCoroutine(assignTaskWithDelay(task));
                }
            }
            if(doorsToChangeState.Length != 0)
            {
                foreach (DoorEntry door in doorsToChangeState)
                {
                    StartCoroutine(changeDoorState(door));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
