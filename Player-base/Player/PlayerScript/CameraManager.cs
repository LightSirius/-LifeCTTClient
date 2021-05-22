using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;


    public Transform targetTraform; // Ä«¸Þ¶ó°¡ µû¶ó´Ù´Ò Å¸°Ù
    public Transform cameraPivot; 
    public Transform cameraTraform;
    private Vector3 cameraFollowVelocity = Vector3.zero;   
    public float cameraFollowSpeed = 0.2f;



    private void Awake() 
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTraform = FindObjectOfType<PlayerMG>().transform;
    }


    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTraform.position,ref cameraFollowVelocity,cameraFollowSpeed);

        transform.position = targetPosition;
    }




}
