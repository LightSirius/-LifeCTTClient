using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputKeyManager inputKeyManager;
    CameraManager cameraManager;

    
    private void Awake() 
    {
        inputKeyManager = GetComponent<InputKeyManager>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    public void HandleAllMovement()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
     
    }
}
