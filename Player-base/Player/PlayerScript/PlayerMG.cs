using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMG : MonoBehaviour
{
    Controller controller;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;

    private void Awake() 
    {
        controller = GetComponent<Controller>();   
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Update() 
    {
        inputManager.HandleAllInput();   
    }

    private void FixedUpdate() 
    {
        playerLocomotion.HandleAllMovement();        
    }

    private void LateUpdate() 
    {
        cameraManager.FollowTarget();
    }
}
