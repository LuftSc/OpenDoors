using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{
    public bool _isEnabled = false;
    private static string interactText_toEnable = "(E) Включить";
    private static string interactText_toDisable = "(E) Выключить";
    override public string GetInteractText()
    {
        if(_isEnabled)
        {
            return interactText_toDisable;
        }
        else
        {
            return interactText_toEnable;
        }
    }
    override public void InteractAction()
    {
        Light light = this.gameObject.GetComponentInChildren<Light>();
        _isEnabled = !_isEnabled;
        light.enabled = _isEnabled;
        if(_isEnabled)
        {
            this.gameObject.transform.Find("c_low").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.9f, 0.9f, 0.9f));
        }
        else
        {
            this.gameObject.transform.Find("c_low").GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
    }
    private void Start()
    {
        Light light = this.gameObject.GetComponentInChildren<Light>();
        light.enabled = _isEnabled;
        if (_isEnabled)
        {
            this.gameObject.transform.Find("c_low").GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.9f, 0.9f, 0.9f));
        }
        else
        {
            this.gameObject.transform.Find("c_low").GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
    }
}
