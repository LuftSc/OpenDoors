using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task : MonoBehaviour
{
    private TMP_Text taskText;
    public int Id;
    void Start()
    {
        taskText = this.gameObject.GetComponentInChildren<TMP_Text>();
    }

    public void taskDone()
    {
        Animator animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("isCompleted", true);
    }

    public void setTaskText(string text)
    {
        taskText = this.gameObject.GetComponentInChildren<TMP_Text>();
        this.taskText.text = text;
    }

    void Update()
    {
        
    }
}
