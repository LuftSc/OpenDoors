using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool _isOpened = false;
    public bool _isLocked = false;
    public string lockedDoorInteractText = "";

    public AudioClip[] audioClips;
    /*
     0 - OpenSound
     1 - CloseSound
     2 - LockedSound_TryOpen
     */


    [System.Serializable]
    public class ObjectEntry
    {
        public GameObject gameObj;
        public bool isActive;
        public float switchActiveDelay = 0f;
    }
    public ObjectEntry[] objectsToSwitch = null;
    private Animator _animator;
    private static readonly int IsOpened = Animator.StringToHash("isOpened");
    private static string interactText_toOpen = "(E) Открыть";
    private static string interactText_toClose = "(E) Закрыть";
    private AudioSource doorAudioSource;

    IEnumerator switchActiveWithDelay(ObjectEntry obj)
    {
        yield return new WaitForSeconds(obj.switchActiveDelay);
        obj.gameObj.gameObject.SetActive(obj.isActive);
    }

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

    private void doEvents()
    {
        if (objectsToSwitch.Length != 0)
        {
            foreach (ObjectEntry go in objectsToSwitch)
            {
                StartCoroutine(switchActiveWithDelay(go));
            }
        }
    }

    override public void InteractAction()
    {
        doEvents();
        doorAudioSource.Stop();
        if (_isLocked)
        {
            doorAudioSource.clip = audioClips[2];
            doorAudioSource.time = 0f;
            doorAudioSource.Play();
            return;
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

    public void changeDoorState(bool toOpen)
    {
        _animator.SetBool(IsOpened, toOpen);
        if (_isOpened == toOpen)
            return;
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
        _isOpened = toOpen;
    }

    override public string GetInteractText()
    {
        if(_isLocked && lockedDoorInteractText != "")
            return lockedDoorInteractText;
        if (_isOpened == true)
            return interactText_toClose;
        else
            return interactText_toOpen;
    }
}
