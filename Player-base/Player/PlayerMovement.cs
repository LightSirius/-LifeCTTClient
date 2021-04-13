using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private PlayerManager playerManager;

    #region 변수 목록

    public float moveSpeed = 0f;
    private float rotateSpeed = 15.0f;
    private float jumpPower = 800f; 
    private float forceGravity = 20f;

    [SerializeField]
    private float stamina = 100f;
    private float recoveryStaminaTime = 0;

    private float horizontal_Move;
    private float vertical_Move;
    private Transform camVec; // 카메라 벡터
    private Vector3 camDir; // 카메라가 보는 방향
    private Vector3 movement;        
    public Vector3 velocity;
    
    private Rigidbody rigid_player;

    private bool isJumping = false;
    private bool isGround = false;

    #endregion
    #region Awake, Start, Update, FixedUpdate
    private void Awake() 
    {
       playerManager = GetComponent<PlayerManager>();
        rigid_player = GetComponent<Rigidbody>();
        rigid_player.velocity = velocity;
    }
    private void Start() 
    {      
        stamina = 100f;
        movement = Vector3.zero;
        camVec = GameObject.Find("CameraVector").transform;
        camDir = camVec.localRotation * Vector3.forward; 
    }

    private void Update() 
    {
        KeyboardInput();
    }
    private void FixedUpdate()
    {        
        Turn();        
        Stamina();
    }
    #endregion
    void KeyboardInput()
    {
        horizontal_Move = Input.GetAxisRaw("Horizontal");
        vertical_Move = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {                        
            isJumping = true;
        }
    }

    #region 물리 이동 함수 목록
    public void Move(float horizontal_Move,float vertical_Move)
    {        
        movement.Set(horizontal_Move ,0, vertical_Move);
        Vector3 moveHorizontal = camVec.right * horizontal_Move;
        Vector3 moveVertical = camVec.forward * vertical_Move;

        velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;

        rigid_player.MovePosition(transform.position + velocity * Time.deltaTime); 
    }

    public void Move(Vector2 move_direction)
    {        
        movement.Set(move_direction.x ,0, move_direction.y);
        Vector3 moveHorizontal = camVec.right * move_direction.x;
        Vector3 moveVertical = camVec.forward * move_direction.y;

        velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;

        rigid_player.MovePosition(transform.position + velocity * Time.deltaTime); 
    }

    public void Turn()
    {
        if(horizontal_Move == 0 && vertical_Move == 0)
           return;

        Quaternion newRotation = Quaternion.LookRotation(camVec.TransformDirection(movement));
        rigid_player.rotation = Quaternion.Slerp(rigid_player.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    public void Jump(float jumping)
    {
        
        if(jumping == 1 && isJumping && isGround)
        {
            rigid_player.AddForce(new Vector3(0f, jumpPower, 0f));
            isGround = false;
            isJumping = false;
        }
        rigid_player.AddForce(Vector3.down * forceGravity);
    }

       private void Stamina()
    {
        if(stamina <= 0) 
            stamina = 0;
        if(stamina>=100)
            stamina = 100;


        if(velocity.x != 0 || velocity.z != 0 && stamina != 0 ) // "멈춰!" 있을 때만 
        {
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
            moveSpeed = 0f;
        }
    }

    private void RecoveryStamina()
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


