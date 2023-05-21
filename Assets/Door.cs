using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool _isOpened = false;
    private Animator _animator;
    private static readonly int IsOpened = Animator.StringToHash("isOpened");
    private static string interactText_toOpen = "(E) Открыть";
    private static string interactText_toClose = "(E) Закрыть";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    override public void InteractAction()
    {
        _isOpened = !_isOpened;
        _animator.SetBool(IsOpened, _isOpened);
    }

    override public string GetInteractText()
    {
        if (_isOpened == true)
            return interactText_toClose;
        else
            return interactText_toOpen;
    }
}
