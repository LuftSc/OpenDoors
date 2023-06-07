using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Interactable
{
    private static string interactText = "(E) Выключить";
    private Transform screen;
    override public string GetInteractText()
    {
        if (screen.gameObject.activeSelf == true)
            return interactText;
        else
            return "";
    }
    override public void InteractAction()
    {
        screen.gameObject.SetActive(false);
    }
    void Start()
    {
        screen = this.gameObject.transform.Find("TV_screen_an").Find("TV_screen");
    }

    void Update()
    {
        
    }
}
