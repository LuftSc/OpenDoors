using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    public Transform Player;
    
    [Header("Чувствительность мыши")]
    public float sensivity = 200f;
    void Start()
    {
        // Отключаем видимость курсора мышки во время игры
        Cursor.lockState = CursorLockMode.Locked;
        transform.Rotate(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Отслеживаем перемещение мыши по оси Х и по оси У
        mouseX = Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;
        
        // Поворот игрока
        Player.Rotate(mouseX * new Vector3(0, 1, 0));
        
        // Поворот камеры
        transform.Rotate(-mouseY * new Vector3(1, 0, 0));
    }
}
