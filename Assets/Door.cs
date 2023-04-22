using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _isOpened;
    private Animator _animator;
    private static readonly int IsOpened = Animator.StringToHash("isOpened");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetBool(IsOpened, _isOpened);
        _isOpened = !_isOpened;
    }
}
