using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;

public class Interactive : MonoBehaviour
{

    [SerializeField] private Camera _fpcCamera;
    [SerializeField] private float _maxDistOfRay;
    [SerializeField] private TextMeshProUGUI _interactText;

    private Ray _ray;
    private RaycastHit _hit;

    private void Update()
    {
        Ray();
        DrawRay();
        Interact();
    }

    private void Ray()
    {
        _ray = _fpcCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        
    }

    private void DrawRay()
    {
        if (Physics.Raycast(_ray, out _hit, _maxDistOfRay))
        {
            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistOfRay, Color.blue);
            if (_interactText.transform.gameObject.activeSelf) 
                _interactText.transform.gameObject.SetActive(false);
                
        }

        if (_hit.transform == null)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistOfRay, Color.red);
            if (_interactText.transform.gameObject.activeSelf) 
                _interactText.transform.gameObject.SetActive(false);
        }
    }

    private void Interact()
    {
        if (_hit.transform != null && _hit.transform.GetComponent(typeof(Interactable)))
        {
            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistOfRay, Color.green);
            Interactable interObj = _hit.transform.gameObject.GetComponent<Interactable>();
            _interactText.text = interObj.GetInteractText();
            _interactText.transform.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                _hit.transform.GetComponent<Interactable>().InteractAction();
                _interactText.transform.gameObject.SetActive(false);
            }
        }
    }
}
