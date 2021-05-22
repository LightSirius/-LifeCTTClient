using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;


    public Transform cameraObject;
    private Transform cameraPivot;

    public Vector3 moveDirection;
    public Vector3 movement;
    public Vector3 velocity;
    Rigidbody player_Rigid;


    public float moveSpeed = 5;
    public float rotationSpeed = 15;    

    [Header("Movement Flags")]
    public bool isJumping;
    public bool isGrounded;

    
    private void Awake() 
    {
        inputManager = GetComponent<InputManager>();
        player_Rigid = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        
    }
    private void Start() 
    {
        cameraPivot = GameObject.Find("CameraPivot").transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement(inputManager.movementInput);    
        HandleRotation(inputManager.movementInput);
    }

    private void HandleMovement(Vector2 direction)
    {
        /*  점프 상태거나 땅에 안닿아 있을 때 이동불가 시키고 싶을 때 사용
        if(isJumping || !isGrounded)
            return;
        */

        movement.Set(direction.x , 0, direction.y);
        Vector3 moveHorizontal = cameraPivot.right * direction.x;
        Vector3 moveVertical = cameraPivot.forward * direction.y;

        velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;

        player_Rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
   
    private void HandleRotation(Vector2 direction)
    {
        if(direction.x == 0 && direction.y == 0)
           return;

        Quaternion newRotation = Quaternion.LookRotation(cameraPivot.TransformDirection(movement));
        player_Rigid.rotation = Quaternion.Slerp(player_Rigid.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }
    public void HandleJumping()
    {
        Debug.Log("점프");
    }
}
