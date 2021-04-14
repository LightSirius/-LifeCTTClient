using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // private PlayerManager playerManager;

    #region 변수 목록

    [Header("이동 속도")]
    public float moveSpeed = 0f;
    private float rotateSpeed = 15.0f;
    [SerializeField]
    private float jumpPower = 800f; 
    private float forceGravity = 20f;

    [Header ("스태미나")]
    [SerializeField]
    private float stamina = 100f;
    private float recoveryStaminaTime = 0;

    private Transform camVec; // 카메라 벡터
    private Vector3 camDir; // 카메라가 보는 방향
    private Vector3 movement;        
    public Vector3 velocity;
    
    private Rigidbody rigid_player;
    
    private bool isGround;

    #endregion
    #region Awake, Start
    private void Awake() 
    {
       // playerManager = GetComponent<PlayerManager>();
       rigid_player = GetComponent<Rigidbody>();
       rigid_player.velocity = velocity;
    }
    private void Start() 
    {      
        stamina = 100f;
        movement = Vector3.zero;
        camVec = GameObject.FindWithTag("MainCamera").transform;
        camDir = camVec.localRotation * Vector3.forward; 
    }

    #endregion

    #region 물리 이동 함수 목록
    public void Move(Vector2 direction) // 플레이어 이동
    {        
        // if(direction.x == 0 && direction.y == 0)
        // {
        //     rigid_player.velocity = new Vector3(0,0,0);
        // }
        movement.Set(direction.x ,0, direction.y);
        Vector3 moveHorizontal = camVec.right * direction.x;
        Vector3 moveVertical = camVec.forward * direction.y;

        velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;

        rigid_player.MovePosition(transform.position + velocity * Time.deltaTime); 
    }

    public void Turn(Vector2 direction) // 플레이어 회전
    {
        if(direction.x == 0 && direction.y == 0)
           return;

        Quaternion newRotation = Quaternion.LookRotation(camVec.TransformDirection(movement));
        rigid_player.rotation = Quaternion.Slerp(rigid_player.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    public void Jump(bool isJumping) // 점프
    {               
        if(isJumping && isGround)
        {
            rigid_player.AddForce(new Vector3(0f, jumpPower, 0f));    
            isGround = false;                                                            
        }        
    }

    public void Jump() // 점프
    {               
        if(isGround)
        {
            rigid_player.AddForce(new Vector3(0f, jumpPower, 0f));    
            isGround = false;                                                            
        }        
    }

    public void ForceGravity() // 중력 적용
    {
        rigid_player.AddForce(Vector3.down * forceGravity);
    }

    public void Stamina() // 이동 스태미나 
    {
        if(stamina <= 0) 
            stamina = 0;
        if(stamina>=100)
            stamina = 100;


        if(velocity.x != 0 || velocity.z != 0 && stamina != 0)        {
            recoveryStaminaTime = 0f; // 회복 쿨타임 초기화
            moveSpeed = 15f; //속도 증가
            if(stamina > 0)
            stamina -= 0.35f; // 스테미너 감소           
        }        
        else
        {                        
            RecoveryStamina(); // 달리는 중 아닐 때 스테미너 회복 함수 실행
            moveSpeed = 15f; // 이동속도 초기화
        }

        if(stamina == 0)
        {
            moveSpeed = 5f;
        }
    }

    private void RecoveryStamina() // 이동 스태미나 회복 
    {        
        if(stamina >= 100) // 스태미너가 100일땐 회복쿨타임 0으로 리턴
        {
            recoveryStaminaTime = 0;
            return;
        }
        recoveryStaminaTime += Time.fixedDeltaTime; //프레임마다 쿨타임을 올려줌
        if(recoveryStaminaTime > 3) // 3초이상이 되면 스태미너 회복
        stamina += 1.75f;
    }

    #endregion

    #region 충돌 처리
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {                   
            isGround = true;            
        }
    }
    #endregion
   
}


