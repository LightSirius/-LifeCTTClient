using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{    

    public PlayerMovement playerMovement;
    private PlayerLifeForce PlayerLifeForce; 
    private Vector2 direction;   // 무브 , 턴 함수에 넘겨줄 매개 변수
    private bool isJumping; // 점프 함수에 넘겨줄 매개 변수 

    void Awake() 
    {
        playerMovement = GetComponent<PlayerMovement>();
        PlayerLifeForce = GetComponent<PlayerLifeForce>();   
    }

    void Update()
    {
        KeyInput();
    }
    void KeyInput()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;        
        }
        else if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;            
        }                        
        if(Input.GetKeyDown(KeyCode.F)) // 피로도 감소 테스트
        {
            PlayerLifeForce.DecreaseFatigue();
        }
    }
    private void FixedUpdate() 
    {        
        // PlayerMovement 스크립트의 Move Turn Jump ForceGravity Stamina Fatigue 실행
        playerMovement.Move(direction);
        playerMovement.Turn(direction);
        playerMovement.Jump(isJumping);        
        playerMovement.ForceGravity();

        playerMovement.Stamina();
        PlayerLifeForce.Fatigue();
    }
}
