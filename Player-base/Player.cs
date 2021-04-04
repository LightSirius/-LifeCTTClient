using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    private enum PlayerState
    {
        Idle,
        Walk,
        Jump,
        LifeSkill,
    }


    private StateMachine stateMachine;
    //private StateMachine LifeStateMachine;

    private Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

    // 생활종류 - 세부종류가 뭔지 
    private Dictionary<LifeType.Kind, Dictionary<Enum, IState>> lifeStateDic = new Dictionary<LifeType.Kind, Dictionary<Enum, IState>>();

    public bool isFarming = false;      // 생활(채집, 낚시 등)을 하고있을 경우 true 안하고 있을 경우 false

    private Transform myTransform;
    private InteractionObject nearObject;

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
       // LifeStateMachine.DoOperateUpdate();
    }
    void KeyboardInput()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            // idle상태이거나 Walk상태일때만 점프 가능
            if(stateMachine.CurrentState == dicState[PlayerState.Idle] || stateMachine.CurrentState == dicState[PlayerState.Walk])
            {
                stateMachine.SetState(dicState[PlayerState.Jump]);
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        // 근처에 있는 오브젝트 판별
        nearObject = other.GetComponent<InteractionObject>();    
    }

    private void OnTriggerExit(Collider other) {
        // 근처에 있는 오브젝트 해제
        nearObject = null;
    }

    // 코루틴으로 루틴 생성
    // 멀리있으면 -> 가까이 가는 것
    // 오브젝트 캐는 이벤트 실행
    IEnumerator PlayerInteraction(){
        // 플레이어가 chunk매니저에게 허락을 받아야함.
        isFarming = true;              // 나중에 chunkManager에 허락을 받는 코드로 바꾸기

        // 1. 근처오브젝트로 다가감
        yield return StartCoroutine(MoveToNearObject());
        // 2. 캐는 애니메이션 실행 및 UI 켜기
        yield return StartCoroutine(WaitFarmingTime(nearObject.durationTime));
        // 3. 오브젝트 정보 전송
        nearObject.Send();

        isFarming = false;
    }

    IEnumerator MoveToNearObject(){
        float distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
        while(distance > 0.25f){        // 임시로 지정한 거리(0.25f) 근처까지 도달했을 때 실행
            // 임시 이동 코드
            distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
            Vector3 direction = nearObject.transform.position - myTransform.position;
            myTransform.position += direction.normalized * 3f * Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator WaitFarmingTime(float durationTime){
        Debug.Log("나실행했어요");
        float time = 0;
        IState lifestate;
        CheckObjType(out lifestate);
        // 캐릭터 애니메이션을 실행하는 코드 작성 필요
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
        
        FarmingState.Add(FarmingType.Kind.GroundPlant, new GroundState());
        FarmingState.Add(FarmingType.Kind.UnderGroundPlant, new UnGroundState());

        FishingState.Add(FishingType.Kind.Rod, new RodState());
        FishingState.Add(FishingType.Kind.Net, new NetState());

        LiveStockState.Add(LivestockType.Kind.Meat, new MeatState());
        LiveStockState.Add(LivestockType.Kind.Leather, new LeatherState());
        LiveStockState.Add(LivestockType.Kind.ByProduct, new ByProductState());

        MiningState.Add(MiningType.Kind.Pick, new PickState());

        WoodCuttingState.Add(WoodcuttingType.Kind.Tree, new TreeState());
        WoodCuttingState.Add(WoodcuttingType.Kind.FruitTree, new FruitTreeState());
        WoodCuttingState.Add(WoodcuttingType.Kind.FlowerTree, new FlowerTreeState());

        lifeStateDic.Add(LifeType.Kind.Farming, FarmingState);
        lifeStateDic.Add(LifeType.Kind.Fishing, FishingState);
        lifeStateDic.Add(LifeType.Kind.Livestock, LiveStockState);
        lifeStateDic.Add(LifeType.Kind.Mining, MiningState);
        lifeStateDic.Add(LifeType.Kind.Woodcutting, WoodCuttingState);
        // testDic[LifeType.Kind.Woodcutting][WoodcuttingType.Kind.Tree].OperateEnter();

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