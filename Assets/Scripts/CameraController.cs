using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    public Transform Player;
    
    [Header("Чувствительность мыши")]
    public float sensitivity = 1f;
    [Tooltip("Ограничение углов камеры по вертикали")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
    Vector2 rotation = Vector2.zero;
    void Start()
    {
        // Отключаем видимость курсора мышки во время игры
        Cursor.lockState = CursorLockMode.Locked;
        transform.Rotate(0, 0, 0);
        Player.Rotate(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Более плавная реализация движения камеры
        rotation.x += Input.GetAxis("Mouse X") * sensitivity;
        rotation.y += Input.GetAxis("Mouse Y") * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        transform.localRotation = yQuat;
        Player.rotation = xQuat;
    }
}
