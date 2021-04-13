using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : MonoBehaviour
{
    public Quaternion TargetRotation;
    public Transform CameraVector;

    public float rotationSpeed; // 회전 스피드
    public float zoomSpeed; // 줌 스피드
    public float distance; // 서로간의 거리

    private Vector3 AxisVec;
    private Vector3 Gap; // 회전 축적 값
    private Transform MainCamera;

    void Start()
    {
        MainCamera = Camera.main.transform;
    }

    void Update()
    {
        Zoom();
        CameraRotation();
    }

    // 카메라 줌
    void Zoom()
    {
        distance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1; // -1을 곱하는 이유는 휠을 위로 올렸을 때 거리가 내려가야하기 때문
        distance = Mathf.Clamp(distance, 1.0f, 10.0f); // Clamp를 쓴 이유는 줌의 최대 최소값을 지정할 것이기 때문

        AxisVec = transform.forward * -1;
        AxisVec *= distance;
        MainCamera.position = transform.position + AxisVec;
    }

    // 카메라 회전
    void CameraRotation()
    {        
        transform.rotation = TargetRotation;
        // 마우스 좌클릭 시
        if(Input.GetMouseButton(1))
        {
            // 값을 축적 시킴
            Gap.x += Input.GetAxis("Mouse Y") * rotationSpeed * -1; // -1을 곱한이유는 위로끌었을 때 아래를 봐야하기 때문            
            Gap.y += Input.GetAxis("Mouse X") * rotationSpeed;

            // 카메라 회전범위 제한
            Gap.x = Mathf.Clamp(Gap.x, -5.0f, 85.0f);
            // 회전 값을 변수에 저장
            TargetRotation = Quaternion.Euler(Gap); // 목표 회전 값에 추적된 회전 값을 대입시킴

            // Axis객체에 x,z회전 값을 제외한 y값만 넘김
            Quaternion q = TargetRotation;
            q.x = q.z = 0;
            CameraVector.transform.rotation = q;
        }
    }
}