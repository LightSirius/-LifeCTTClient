using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    

    public Transform targetTransform; // 카메라가 따라갈 타겟
    public Transform cameraPivot;
    public Transform cameraTransform;

    public Vector3 cameraFollowVelocity = Vector3.zero;

    public float cameraFollowSpeed = 0.2f;

    private void Awake() 
    {
        targetTransform = FindObjectOfType<PlayerMgr>().transform;
    }

    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position,ref cameraFollowVelocity,cameraFollowSpeed);

        transform.position = targetPosition;
    }
}
