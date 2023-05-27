using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool _isOpened = false;

    public AudioClip[] audioClips;
    /*
     0 - OpenSound
     1 - CloseSound
     2 - LockedSound_TryOpen
     */

    private Animator _animator;
    private static readonly int IsOpened = Animator.StringToHash("isOpened");
    private static string interactText_toOpen = "(E) Открыть";
    private static string interactText_toClose = "(E) Закрыть";
    private static bool _isLocked = false;
    private AudioSource doorAudioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        doorAudioSource = this.gameObject.AddComponent<AudioSource>();
        doorAudioSource.Stop();
        doorAudioSource.volume = 0.5f;
        doorAudioSource.loop = false;
        doorAudioSource.spatialBlend = 1.0f;
        doorAudioSource.maxDistance = 25;
    }

    override public void InteractAction()
    {
        doorAudioSource.Stop();
        if (_isLocked)
        {
            doorAudioSource.clip = audioClips[2];
            doorAudioSource.time = 0f;
        }
        else if (_isOpened) //Closing door
        {
            doorAudioSource.clip = audioClips[1];
            doorAudioSource.time = 0.2f;
        }
        else  //Opening door
        {
            doorAudioSource.clip = audioClips[0];
            doorAudioSource.time = 0.65f;
        }
        doorAudioSource.Play();
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
