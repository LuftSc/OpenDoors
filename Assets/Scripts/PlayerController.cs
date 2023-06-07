using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Update is called once per frame
    [Header("Скорость перемещения персонажа")]
    public float speed = 4f;
    [Header("Скорость ускоренного бега")]
    public float runSpeed = 8f;
    [Header("Сила прыжка")]
    public float jumpPower = 200f;
    [Header("Находимся ли мы сейчас на полу?")]
    public bool isGround;

    [Header("Объект фонарика")]
    public GameObject _flashLight;
    [Header("Объект фонарика(UI)")]
    public GameObject _flashLight_UI;
    [Header("Объект звука шагов")]
    public GameObject _footStepsSound;

    public Rigidbody rb;

    private Coroutine FadeCouroutine = null;
    private float footStepsInitialVolume = 0.2f;
    public bool isImmobile = false;

    private Vector3 prevPosition; //Animator fix
    private bool isSynced = false;
    void LateUpdate()
    {
        GetInput();
    }
    private void Start()
    {
        AudioSource footsteps = _footStepsSound.GetComponent<AudioSource>();
        footStepsInitialVolume = footsteps.volume;
        footsteps.Pause();
    }
    //Осуществляет передвижение игрока,
    // 1 параметр = направление движения, может быть либо transform.forward, либо transform.right
    // 2 параметр = знак, может быть либо 1, либо -1.

    public static class FadeAudioSource
    {
        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                if(audioSource.volume == 0)
                    audioSource.Pause();
                yield return null;
            }
            yield break;
        }
    }

    private void MovePlayer(Vector3 direction, int sign)
    {
        if (FadeCouroutine != null)
        {
            StopCoroutine(FadeCouroutine);
            FadeCouroutine = null;
        }
        AudioSource footsteps = _footStepsSound.GetComponent<AudioSource>();
        footsteps.volume = footStepsInitialVolume;
        footsteps.UnPause();
        // Когда зажат левый шифт, персонаж бежит быстрее
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.localPosition = prevPosition + sign * direction * runSpeed * Time.deltaTime;
            footsteps.pitch = 1.4f;
            prevPosition = transform.localPosition;
        }
        else
        {
            footsteps.pitch = 1f;
            transform.localPosition = prevPosition + sign * direction * speed * Time.deltaTime;
            prevPosition = transform.localPosition;
        }
    }
        

    private void GetInput()
    {
        if (isImmobile && !this.transform.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("End"))
        {
            isSynced = false;
            AudioSource footsteps = _footStepsSound.GetComponent<AudioSource>();
            footsteps.Pause();
        }
        else if(isImmobile && this.transform.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("End") && isSynced)
        {
            transform.localPosition = prevPosition;
        }
        else if(isSynced == false && isImmobile == false)
        {
            prevPosition = transform.localPosition;
            isSynced = true;
        }
        else if(isSynced == true && isImmobile == false)
        {
            transform.localPosition = prevPosition;

            // Движение вперёд
            if (Input.GetKey(KeyCode.W))
            {
                MovePlayer(transform.forward, 1);
            }
            // Движение назад
            if (Input.GetKey(KeyCode.S))
            {
                MovePlayer(transform.forward, -1);
            }
            // Движение влево
            if (Input.GetKey(KeyCode.A))
            {
                MovePlayer(transform.right, -1);
            }
            // Движение вправо
            if (Input.GetKey(KeyCode.D))
            {
                MovePlayer(transform.right, 1);
            }
            Physics.autoSimulation = false;
            Physics.SyncTransforms();
            Physics.Simulate(1f);
            Physics.autoSimulation = true;
            prevPosition = transform.localPosition;

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && FadeCouroutine == null)
            {
                AudioSource footsteps = _footStepsSound.GetComponent<AudioSource>();
                FadeCouroutine = StartCoroutine(FadeAudioSource.StartFade(footsteps, 0.4f, 0f));
            }
        }
        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                rb.AddForce(transform.up * jumpPower);
            }
        }

    }

    public void setFlashLightState(bool state)
    {
        _flashLight.SetActive(state);
        _flashLight_UI.SetActive(state);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = true;
            return;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGround = false;
            return;
        }
    }
}
