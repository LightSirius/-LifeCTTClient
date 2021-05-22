using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{    

    public PlayerMovement playerMovement;
    private PlayerLifeForce PlayerLifeForce; 
    private Vector2 direction;   // ���� , �� �Լ��� �Ѱ��� �Ű� ����
    private bool isJumping; // ���� �Լ��� �Ѱ��� �Ű� ���� 

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
        if(Input.GetKeyDown(KeyCode.F)) // �Ƿε� ���� �׽�Ʈ
        {
            PlayerLifeForce.DecreaseFatigue();
        }
    }
    private void FixedUpdate() 
    {        
        // PlayerMovement ��ũ��Ʈ�� Move Turn Jump ForceGravity Stamina Fatigue ����
        playerMovement.Move(direction);
        playerMovement.Turn(direction);
        playerMovement.Jump(isJumping);        
        playerMovement.ForceGravity();

        playerMovement.Stamina();
        PlayerLifeForce.Fatigue();
    }
}
