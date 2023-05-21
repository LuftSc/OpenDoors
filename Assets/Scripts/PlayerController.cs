using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    [Header("Объект звука шагов")]
    public GameObject _footStepsSound;

    public Rigidbody rb;
    void Update()
    {
        GetInput();
    }
    private void Start()
    {
        
    }
    //Осуществляет передвижение игрока,
    // 1 параметр = направление движения, может быть либо transform.forward, либо transform.right
    // 2 параметр = знак, может быть либо 1, либо -1.
    private void MovePlayer(Vector3 direction, int sign)
    {
        AudioSource footsteps = _footStepsSound.GetComponent<AudioSource>();
        footsteps.UnPause();
        // Когда зажат левый шифт, персонаж бежит быстрее
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.localPosition += sign * direction * runSpeed * Time.deltaTime;
            footsteps.pitch = 1.4f;
        }
        else
        {
            footsteps.pitch = 1f;
            transform.localPosition += sign * direction * speed * Time.deltaTime;
        }
    }
        

    private void GetInput()
    {
        // Движение вперёд
        if (Input.GetKey(KeyCode.W)) MovePlayer(transform.forward, 1);
        // Движение назад
        if (Input.GetKey(KeyCode.S)) MovePlayer(transform.forward, -1);
        // Движение влево
        if (Input.GetKey(KeyCode.A)) MovePlayer(transform.right, -1);
        // Движение вправо
        if (Input.GetKey(KeyCode.D)) MovePlayer(transform.right, 1);

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            AudioSource footsteps = _footStepsSound.GetComponent<AudioSource>();
            footsteps.Pause();
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGround = false;
        }
    }
}
