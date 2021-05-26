using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    
    CameraManager cameraManager;

    private void Awake() 
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        
    }
    private void LateUpdate() 
    {
        cameraManager.FollowTarget();
    }
}
