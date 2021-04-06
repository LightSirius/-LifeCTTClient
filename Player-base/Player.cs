using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeContent;

public class Player : MonoBehaviour {

    //�÷��̾� ��ũ��Ʈ
    private enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        LifeSkill,
    }


    private StateMachine stateMachine;
    //private StateMachine LifeStateMachine;
    public PlayerStatus playerStatus;

    private Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

    // ��Ȱ���� - ���������� ���� 
    private Dictionary<LifeType, Dictionary<Enum, IState>> lifeStateDic = new Dictionary<LifeType, Dictionary<Enum, IState>>();

    public bool isFarming = false;      // ��Ȱ(ä��, ���� ��)�� �ϰ����� ��� true ���ϰ� ���� ��� false

    private Transform myTransform;
    private InteractionObject nearObject;



    #region  ���� ���

    private float h;
    private float v;
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;
  
    private Vector3 movement;
    private Transform camVec; // ī�޶� ����
    private Vector3 camDir; // ī�޶� ���� ����
    private Animator animator;
    
    private Rigidbody rb;    
    private bool isJumping = false;
    public float jumpPower = 800f; 
    public float forceGravity = 20f;
    private bool isGround = false;

    [SerializeField]
    private float stamina = 100f;
    private float recoveryStaminaTime = 0;



    #endregion

    private void Awake() {
                rb = GetComponent<Rigidbody>();
    }

    private void Start() {

        myTransform = transform;

        IState idle = new IdleState();
        IState walk = new WalkState();
        IState jump = new JumpState();

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Walk, walk);
        dicState.Add(PlayerState.Jump, jump);

        InitLifeState();
        
        // �⺻���´� idle ���·� ����        
        stateMachine = new StateMachine(idle);   

    
        movement = Vector3.zero;
        camVec = GameObject.Find("CameraVector").transform;
        camDir = camVec.localRotation * Vector3.forward; 

        animator = GetComponent<Animator>();
    }
    void Update() {
        // Ű�Է�
        if (Input.GetKeyDown(KeyCode.G)){
            // ���࿡ �ָ������� �ٰ�����
            // ������ ������ �ٷ� ä��
            if (nearObject){
                // 
                StartCoroutine(PlayerInteraction());
            }
        }

        KeyboardInput();
        stateMachine.DoOperateUpdate();

        
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJumping = true;
        }

        animator.SetFloat("TimmyMove", new Vector3(h,v).magnitude);
       // LifeStateMachine.DoOperateUpdate();

       
    }
    void KeyboardInput()
    {
        //test
          if(Input.GetKeyDown(KeyCode.L))
          {
              //test
          }
    }

    void Move()
    {
        movement.Set(h, 0, v);
        if (h == 0 && v == 0)
        {
            // ���⶧ IdleState�� ��ȯ
            stateMachine.SetState(dicState[PlayerState.Idle]);            
            return;
        }
        else
        { // �����϶� WalkState�� ��ȯ
            stateMachine.SetState(dicState[PlayerState.Walk]);
            transform.Translate(camDir * moveSpeed * Time.deltaTime);
            
            if (isFarming){
                StopAllCoroutines();        // �ӽ÷� �� ��
                isFarming = false;
                UIMgr.Instance.SetLifeUI(false);        // ���߿� ���⼭ dictionary�� IState.OperatorExitȣ�� �ٶ�                
            }
        }
      
    }

    
    void Turn()
    {
        if (h == 0 && v == 0) // ������ ���� �� ȸ������ ���ϰ� ���Ƶδ� ��
            return;
        Quaternion newRotation = Quaternion.LookRotation(camVec.TransformDirection(movement));

        rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.deltaTime);

        if (movement != Vector3.zero)
            rb.MoveRotation(transform.rotation = newRotation);
    }
    void Jump()
    {
        if(isJumping && isGround)
        {
            rb.AddForce(new Vector3(0f, jumpPower, 0f));
            isGround = false;
            isJumping = false;
        }
        rb.AddForce(Vector3.down * forceGravity);
    }

    private void Stamina()
    {
        if(stamina <= 0) 
            stamina = 0;
        if(stamina>=100)
            stamina = 100;


        if(Input.GetKey(KeyCode.LeftShift) && stamina != 0) // ���� ����Ʈ Ŭ�� �� �޸���
        {
            recoveryStaminaTime = 0f; // ȸ�� ��Ÿ�� �ʱ�ȭ
            moveSpeed = 15f; //�ӵ� ����
            stamina -= 1.5f; // ���׹̳� ����           
        }        
        else
        {                        
            RecoveryStamina(); // �޸��� �� �ƴ� �� ���׹̳� ȸ�� �Լ� ����
            moveSpeed = 5f; // �̵��ӵ� �ʱ�ȭ
        }
    }
    private void RecoveryStamina()
    {        
        if(stamina >= 100) // ���¹̳ʰ� 100�϶� ȸ����Ÿ�� 0���� ����
        {
            recoveryStaminaTime = 0;
            return;
        }
        recoveryStaminaTime += Time.fixedDeltaTime; //�����Ӹ��� ��Ÿ���� �÷���
        if(recoveryStaminaTime > 3) // 3���̻��� �Ǹ� ���¹̳� ȸ��
        stamina += 2.5f;
    }
    private void FixedUpdate() 
    {
        Move();
        if(isGround)
        {
            Turn();
        }
        Jump();

        Stamina();
    }

    
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {                   
            isGround = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        // ��ó�� �ִ� ������Ʈ �Ǻ�
        nearObject = other.GetComponent<InteractionObject>();    
    }

    private void OnTriggerExit(Collider other) {
        // ��ó�� �ִ� ������Ʈ ����
        nearObject = null;
    }

    // �ڷ�ƾ���� ��ƾ ����
    // �ָ������� -> ������ ���� ��
    // ������Ʈ ĳ�� �̺�Ʈ ����
    IEnumerator PlayerInteraction(){
        // �÷��̾ chunk�Ŵ������� ����� �޾ƾ���.
        if (!isFarming){
            isFarming = true;              // ���߿� chunkManager�� ����� �޴� �ڵ�� �ٲٱ�

            // 1. ��ó������Ʈ�� �ٰ���
            yield return StartCoroutine(MoveToNearObject());
            // 2. ĳ�� �ִϸ��̼� ���� �� UI �ѱ�
            yield return StartCoroutine(WaitFarmingTime(nearObject.durationTime));
            // 3. ������Ʈ ���� ����
            nearObject.Send();
            // ���� ����ó���� ���߿�
            Destroy(nearObject);
            isFarming = false;
        }
    }

    IEnumerator MoveToNearObject(){
        float distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
        while(distance > 0.25f){        // �ӽ÷� ������ �Ÿ�(0.25f) ��ó���� �������� �� ����
            // �ӽ� �̵� �ڵ�
            distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
            Vector3 direction = nearObject.transform.position - myTransform.position;
            myTransform.position += direction.normalized * 3f * Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator WaitFarmingTime(float durationTime){
        Debug.Log("�������߾��");
        float time = 0;
        IState lifestate;
        CheckObjType(out lifestate);
        // ĳ���� �ִϸ��̼��� �����ϴ� �ڵ� �ۼ� �ʿ�
        lifestate.OperateEnter();

        while(durationTime > time)
        {
            lifestate.OperateUpdate();
            time += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        lifestate.OperateExit();
        
    
    }

    void InitLifeState()
    {
        Dictionary<Enum, IState> FarmingState = new Dictionary<Enum, IState>();
        Dictionary<Enum, IState> FishingState = new Dictionary<Enum, IState>();
        Dictionary<Enum, IState> LiveStockState = new Dictionary<Enum, IState>();
        Dictionary<Enum, IState> MiningState = new Dictionary<Enum, IState>();
        Dictionary<Enum, IState> WoodCuttingState = new Dictionary<Enum, IState>();
        
        FarmingState.Add(FarmingType.GroundPlant, new GroundState());
        FarmingState.Add(FarmingType.UnderGroundPlant, new UnGroundState());

        FishingState.Add(FishingType.Rod, new RodState());
        FishingState.Add(FishingType.Net, new NetState());

        LiveStockState.Add(LivestockType.Meat, new MeatState());
        LiveStockState.Add(LivestockType.Leather, new LeatherState());
        LiveStockState.Add(LivestockType.ByProduct, new ByProductState());

        MiningState.Add(MiningType.Pick, new PickState());

        WoodCuttingState.Add(WoodcuttingType.Tree, new TreeState());
        WoodCuttingState.Add(WoodcuttingType.FruitTree, new FruitTreeState());
        WoodCuttingState.Add(WoodcuttingType.FlowerTree, new FlowerTreeState());

        lifeStateDic.Add(LifeType.Farming, FarmingState);
        lifeStateDic.Add(LifeType.Fishing, FishingState);
        lifeStateDic.Add(LifeType.Livestock, LiveStockState);
        lifeStateDic.Add(LifeType.Mining, MiningState);
        lifeStateDic.Add(LifeType.Woodcutting, WoodCuttingState);
        // testDic[LifeType.Woodcutting][WoodcuttingType.Tree].OperateEnter();

    }

    void CheckObjType(out IState lifestate)
    {
        lifestate = null;
        if (nearObject is TreeObject)
        {
            lifestate = lifeStateDic[nearObject.lifeType][(nearObject as TreeObject).woodcuttingType];
            //lifeStateDic[nearObject.lifeType][(nearObject as TreeObject).woodcuttingType].OperateEnter();
            //LifeStateMachine.SetState(lifeStateDic[nearObject.lifeType][(nearObject as TreeObject).woodcuttingType]);
            // stateMachine.SetState(dicState[PlayerState.Dead]);
        }
        else if(nearObject is PlantObject)
        {
            //LifeStateMachine.SetState(lifeStateDic[nearObject.lifeType][(nearObject as PlantObject).farmingType]);
        }
        else if(nearObject is FishingAreaObject)
        {
            //LifeStateMachine.SetState(lifeStateDic[nearObject.lifeType][(nearObject as FishingAreaObject).fishingType]);
        }
        else if(nearObject is LivestockObject)
        {
            //LifeStateMachine.SetState(lifeStateDic[nearObject.lifeType][(nearObject as LivestockObject).livestockType]);
        }
        else if(nearObject is MineralObject)
        {
            //LifeStateMachine.SetState(lifeStateDic[nearObject.lifeType][(nearObject as MineralObject).miningType]);
        }
        else
        {
            Debug.Log("���ǵ��� ���� ������Ʈ Ÿ���Դϴ�.");
        }
    }
}