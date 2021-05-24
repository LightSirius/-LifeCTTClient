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
    private bool isNear = false;
    private bool isInteract = true;
    [SerializeField]
    private Vector3 targetVec;
    [SerializeField]
    private GameObject interactTarget;
    private GameObject myPlayer;
    #endregion

    void Update() {        
        InteractionMove();
    }

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
        if (!isGround){
            rigid_player.AddForce(Vector3.down * forceGravity);
        }
    }
    #endregion

    #region  ���¹̳� �� ��ȣ�ۿ�
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

    private void InteractionMove()
    {
        if(Input.GetKeyDown(KeyCode.E) && isNear && isInteract)
        {
            
            StartCoroutine(InteractionMove(myPlayer, targetVec));
        }
    }

    #endregion

    #region �ڷ�ƾ


    // ��ȣ�ۿ� ���� �ڷ�ƾ
    IEnumerator InteractionMove(GameObject player, Vector3 targetPos)
    {
        Debug.Log("��ȣ�ۿ� �ڷ�ƾ ����");                
        isInteract = false; //��ȣ �ۿ� �� Ȱ��ȭ

        // 1. �ش� ������Ʈ���� �����Ÿ����� �ٰ���
        float count = 0;
        Vector3 wasPos = player.transform.position;

        // Ÿ�� �ٶ󺸱�
        Vector3 relativePos = targetPos - transform.position;   
        Quaternion targetViewRotation = Quaternion.LookRotation(relativePos);
        player.transform.rotation = targetViewRotation;

        while (true)
        {
            count += Time.deltaTime; // 0 ~ 1 ���� Time.deltaTime�� ������
                player.transform.position = Vector3.Lerp(wasPos,targetPos ,count);
                    float distance = Vector3.Distance(targetPos, player.transform.position);                      
                    //�÷��̾�� Ÿ���� ���� �Ÿ� ���
            if(distance <= 1)   // 1��ŭ ��������� ����
            {                                
                break;
            }            
            yield return null;            
            isInteract = true;                 // �ٽ� ��ȣ�ۿ��� �� �ִ� ���·� Ȱ��ȭ
        }                
    }

    // ��ȣ�ۿ� ���� �ڷ�ƾ
    public IEnumerator InteractionMove(Vector3 targetPos)
    {
        Debug.Log("��ȣ�ۿ� �ڷ�ƾ ����");                
        isInteract = false; //��ȣ �ۿ� �� Ȱ��ȭ

        // 1. �ش� ������Ʈ���� �����Ÿ����� �ٰ���
        float count = 0;
        Vector3 wasPos = this.transform.position;

        // Ÿ�� �ٶ󺸱�
        Vector3 relativePos = targetPos - transform.position;   
        Quaternion targetViewRotation = Quaternion.LookRotation(relativePos);
        this.transform.rotation = targetViewRotation;

        while (true)
        {
            count += Time.deltaTime; // 0 ~ 1 ���� Time.deltaTime�� ������
                this.transform.position = Vector3.Lerp(wasPos,targetPos ,count);
                    float distance = Vector3.Distance(targetPos, this.transform.position);                      
                    //�÷��̾�� Ÿ���� ���� �Ÿ� ���
            if(distance <= 1)   // 1��ŭ ��������� ����
            {                                
                break;
            }            
            yield return null;            
            isInteract = true;                 // �ٽ� ��ȣ�ۿ��� �� �ִ� ���·� Ȱ��ȭ
        }                
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

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag=="GameController")
        {
            Debug.Log("���� ����");
            isNear = true;                        
            interactTarget = other.gameObject;
            targetVec = new Vector3(
            interactTarget.transform.position.x,
            interactTarget.transform.position.y,
            interactTarget.transform.position.z);  
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag=="GameController")
        {
            Debug.Log("������ ����");
            isNear = false;        
            interactTarget = null;
            
        }
    }
    
    #endregion
   
}


