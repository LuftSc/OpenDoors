using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatableExtension : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string name)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(string.Concat("Sounds/" ,name));
        if (!audioClip)
            return;
        AudioSource audioSource =  this.gameObject.AddComponent<AudioSource>();
        audioSource.Stop();
        audioSource.volume = 1f;
        audioSource.loop = false;
        audioSource.spatialBlend = 1.0f;
        audioSource.maxDistance = 25;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
