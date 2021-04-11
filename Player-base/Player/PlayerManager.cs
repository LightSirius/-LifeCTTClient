using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{    

    public PlayerMovement playerMovement;
    private PlayerLifeForce PlayerLifeForce;
    private float horizontal_Move;
    private float vertical_Move;

    private float jumping;
    private void Awake() {
        
    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        PlayerLifeForce = GetComponent<PlayerLifeForce>();
    }

    void Update()
    {
        horizontal_Move = Input.GetAxisRaw("Horizontal");
        vertical_Move = Input.GetAxisRaw("Vertical");

        playerMovement.Move(horizontal_Move,vertical_Move);

        jumping = Input.GetAxisRaw("Jump");    
        playerMovement.Jump(jumping);

        if(Input.GetKeyDown(KeyCode.F)) // 피로도 감소 테스트
        {
            PlayerLifeForce.DecreaseFatigue();
        }
    }
    private void FixedUpdate() 
    {
        playerMovement.Turn();
        playerMovement.Stamina();
        PlayerLifeForce.Fatigue();
    }
    
}
