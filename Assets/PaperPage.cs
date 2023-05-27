using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperPage : Interactable
{
    public string textToShow = "";
    private static string interactText = "(E) Прочесть";
    override public string GetInteractText()
    {
        return interactText;
    }
    override public void InteractAction()
    {
    }
    private void Start()
    {
        TMP_Text tmpText = this.gameObject.GetComponentInChildren<TMP_Text>();
        if (textToShow == "")
        {
            textToShow = tmpText.text;
        }
        else
        {
            tmpText.text = textToShow;
        }
    }
}