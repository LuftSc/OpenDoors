using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : Interactable
{
    private static string interactText = "(E) Взять";
    override public string GetInteractText()
    { 
        return interactText;
    }
    override public void InteractAction()
    {
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.setFlashLightState(true);
        this.gameObject.SetActive(false);
        completeTaskOnInteract(taskID);
    }

    private void Start()
    {
    }
}
