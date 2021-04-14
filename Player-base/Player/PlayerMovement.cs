using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // private PlayerManager playerManager;

    #region ���� ���

    [Header("�̵� �ӵ�")]
    public float moveSpeed = 0f;
    private float rotateSpeed = 15.0f;
    [SerializeField]
    private float jumpPower = 800f; 
    private float forceGravity = 20f;

    [Header ("���¹̳�")]
    [SerializeField]
    private float stamina = 100f;
    private float recoveryStaminaTime = 0;

    private Transform camVec; // ī�޶� ����
    private Vector3 camDir; // ī�޶� ���� ����
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

    #region ���� �̵� �Լ� ���
    public void Move(Vector2 direction) // �÷��̾� �̵�
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

    public void Turn(Vector2 direction) // �÷��̾� ȸ��
    {
        if(direction.x == 0 && direction.y == 0)
           return;

        Quaternion newRotation = Quaternion.LookRotation(camVec.TransformDirection(movement));
        rigid_player.rotation = Quaternion.Slerp(rigid_player.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    public void Jump(bool isJumping) // ����
    {               
        if(isJumping && isGround)
        {
            rigid_player.AddForce(new Vector3(0f, jumpPower, 0f));    
            isGround = false;                                                            
        }        
    }

    public void Jump() // ����
    {               
        if(isGround)
        {
            rigid_player.AddForce(new Vector3(0f, jumpPower, 0f));    
            isGround = false;                                                            
        }        
    }

    public void ForceGravity() // �߷� ����
    {
        rigid_player.AddForce(Vector3.down * forceGravity);
    }

    public void Stamina() // �̵� ���¹̳� 
    {
        if(stamina <= 0) 
            stamina = 0;
        if(stamina>=100)
            stamina = 100;


        if(velocity.x != 0 || velocity.z != 0 && stamina != 0)        {
            recoveryStaminaTime = 0f; // ȸ�� ��Ÿ�� �ʱ�ȭ
            moveSpeed = 15f; //�ӵ� ����
            if(stamina > 0)
            stamina -= 0.35f; // ���׹̳� ����           
        }        
        else
        {                        
            RecoveryStamina(); // �޸��� �� �ƴ� �� ���׹̳� ȸ�� �Լ� ����
            moveSpeed = 15f; // �̵��ӵ� �ʱ�ȭ
        }

        if(stamina == 0)
        {
            moveSpeed = 5f;
        }
    }

    private void RecoveryStamina() // �̵� ���¹̳� ȸ�� 
    {        
        if(stamina >= 100) // ���¹̳ʰ� 100�϶� ȸ����Ÿ�� 0���� ����
        {
            recoveryStaminaTime = 0;
            return;
        }
        recoveryStaminaTime += Time.fixedDeltaTime; //�����Ӹ��� ��Ÿ���� �÷���
        if(recoveryStaminaTime > 3) // 3���̻��� �Ǹ� ���¹̳� ȸ��
        stamina += 1.75f;
    }

    #endregion

    #region �浹 ó��
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {                   
            isGround = true;            
        }
    }
    #endregion
   
}


