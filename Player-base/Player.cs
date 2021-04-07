using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeContent;
using Anim;

public class Player : MonoBehaviour {

    //플레이어 스크립트
    private enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        LifeSkill,
    }

    private PlayerAnimController playerAnimController;
    private StateMachine stateMachine;
    //private StateMachine LifeStateMachine;
    public PlayerStatus playerStatus;

    private Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

    // 생활종류 - 세부종류가 뭔지 
    private Dictionary<LifeType, Dictionary<Enum, IState>> lifeStateDic = new Dictionary<LifeType, Dictionary<Enum, IState>>();

    public bool isFarming = false;      // 생활(채집, 낚시 등)을 하고있을 경우 true 안하고 있을 경우 false

    private Transform myTransform;
    private InteractionObject nearObject;



    #region  변수 목록

    private float h;
    private float v;
    public float moveSpeed = 5.0f;
    private float rotateSpeed = 15.0f;
  
    private Vector3 movement;
    private Transform camVec; // 카메라 벡터
    private Vector3 camDir; // 카메라가 보는 방향
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
        playerAnimController = GetComponent<PlayerAnimController>();
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
        
        // 기본상태는 idle 상태로 설정        
        stateMachine = new StateMachine(idle);   

    
        movement = Vector3.zero;
        camVec = Camera.main.transform;
        camDir = camVec.localRotation * Vector3.forward; 

        animator = GetComponent<Animator>();
    }
    void Update() {
        // 키입력
        if (Input.GetKeyDown(KeyCode.G)){
            // 만약에 멀리있으면 다가가고
            // 가까이 있으면 바로 채집
            if (nearObject){
                // 
                StartCoroutine(PlayerInteraction());
            }
        }

        KeyboardInput();
        stateMachine.DoOperateUpdate();


        // Animation Test Start
        // -----------------------------------
        playerAnimController.UpdateMove(new Vector3(h, v));

        if (isFarming && playerAnimController.Current_State == PlayerAnimState.Move){
            Debug.Log("??");
            playerAnimController.ChangeState(PlayerAnimState.Exit);
            StopAllCoroutines();
        }
        // -----------------------------------
        // Animation Test End

        
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJumping = true;
        }

        // animator.SetFloat("TimmyMove", new Vector3(h,v).magnitude);
       // LifeStateMachine.DoOperateUpdate();  
    }

    private void FixedUpdate() 
    {
        Move();
        Turn();        
        Jump();
        Stamina();
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
        movement.Set(h ,0, v);
        Vector3 moveHorizontal = camVec.right * h;
        Vector3 moveVertical = camVec.forward * v;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;

        rb.MovePosition(transform.position + velocity * Time.deltaTime);   
    }

    
    void Turn()
    {
        if(h == 0 && v == 0)
           return;

        Quaternion newRotation = Quaternion.LookRotation(camVec.TransformDirection(movement));
        rb.rotation = Quaternion.Slerp(rb.rotation, newRotation, rotateSpeed * Time.deltaTime);
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


        if(Input.GetKey(KeyCode.LeftShift) && stamina != 0) // 왼쪽 시프트 클릭 시 달리기
        {
            recoveryStaminaTime = 0f; // 회복 쿨타임 초기화
            moveSpeed = 15f; //속도 증가
            stamina -= 1.5f; // 스테미너 감소           
        }        
        else
        {                        
            RecoveryStamina(); // 달리는 중 아닐 때 스테미너 회복 함수 실행
            moveSpeed = 5f; // 이동속도 초기화
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
        stamina += 2.5f;
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
        // 근처에 있는 오브젝트 판별
        nearObject = other.GetComponent<InteractionObject>();    
    }

    private void OnTriggerExit(Collider other) {
        // 근처에 있는 오브젝트 해제
        // nearObject = null;
    }

    // 코루틴으로 루틴 생성
    // 멀리있으면 -> 가까이 가는 것
    // 오브젝트 캐는 이벤트 실행
    IEnumerator PlayerInteraction(){
        // 플레이어가 chunk매니저에게 허락을 받아야함.
        if (!isFarming){
            

            // 1. 근처오브젝트로 다가감
            yield return StartCoroutine(MoveToNearObject());
            // 2. 캐는 애니메이션 실행 및 UI 켜기
            yield return StartCoroutine(WaitFarmingTime(nearObject.durationTime));
            // 3. 오브젝트 정보 전송
            nearObject.Send();
            // 따로 스폰처리는 나중에
            // Destroy(nearObject);
            
        }
    }

    IEnumerator MoveToNearObject(){
        float distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
        while(distance > 0.25f){        // 임시로 지정한 거리(0.25f) 근처까지 도달했을 때 실행
            // 임시 이동 코드
            distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
            Vector3 direction = nearObject.transform.position - myTransform.position;

            // Animation Test Start
            // -----------------------------------
            playerAnimController.UpdateMove(direction, false);
            // -----------------------------------
            // Animation Test End

            myTransform.position += direction.normalized * 3f * Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator WaitFarmingTime(float durationTime){
        isFarming = true;              // 나중에 chunkManager에 허락을 받는 코드로 바꾸기
        // Debug.Log("나실행했어요");
        float time = 0;
        IState lifestate;
        CheckObjType(out lifestate);
        // 캐릭터 애니메이션을 실행하는 코드 작성 필요
        // lifestate.OperateEnter();

        // Animation Test Start
        // -----------------------------------
        playerAnimController.ChangeState(PlayerAnimController.GetLifeTypeToPlayerAnimState(FishingType.Rod));

        while(durationTime > time)
        {
            lifestate.OperateUpdate();
            time += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        playerAnimController.ChangeState(PlayerAnimState.Fishing_Rod_End);

        yield return new WaitForSeconds(5f);

        playerAnimController.ChangeState(PlayerAnimState.Exit);
        // lifestate.OperateExit();

        isFarming = false;
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
            Debug.Log("정의되지 않은 오브젝트 타입입니다.");
        }
    }
}