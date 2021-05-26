// using System.Collections;
// using System.Collections.Generic;
// using System;
// using UnityEngine;
// using LifeContent;


//     // 생활컨텐츠를 할때
//     // 나무_열매, 나무_꽃, 나무나무
//     // 보상 확률 인덱스 <- 이 값들을 초기화 해줘야함
//     // DB에서 값을 받아와서 초기화 해야함

//     // 딕션어리 안에서는 모노가 없으니까 Start가 됬든 awake가 됬던 얘네한테 init할 기점을 잡을수가 없음
//     // 그렇다고 인잇함수를 빼자니 이건 뭐 빨리달릴려고 신발 4개 신는느낌
//     // 딕션어리 내부에 생성자를 통해서 초기값을 인잇하자
//     // private Dictionary<LifeType, Dictionary<Enum, ILifeSkill>> lifeStateDic = new Dictionary<LifeType, Dictionary<Enum, ILifeSkill>>();
//     // 해당 방식으로 변수로 선언됬을 때
//     // 초기값이 생성자를 통해 init됨

//     // 딕션어리 생성자를 돌릴때
//     // 딕션어리 안에 엄청나게 많은 스크립트가 있으니까
//     // 이 스크립트 마다 생성자에서 DB호출을 한다? 오우 쓋 -> 해당 스크립트에서 딜레이 최소 3초 예상
//     // 그럼 DB에서 값 어떻게 가져옴?

//     // DB Init Manager 스크립트 에서
//     // DB에 생활에 관련된 데이터 값을 오브젝트로 쭉 긁어온다음 (호출 1회)

//     // DB Init Manager 에 내부 변수로 가지고 있음.
//     // 그리고 딕션어리 생성자 에서 DB Init Manager에 있는 내부 변수를 참조해서 값을 init함
    
//     // 그리고 DB Init Manager는 날려버려도 될듯? (일단은 남겨두는 방면으로 쓰고)
//     // 대신 해당 DB Init Manager에 변수가 됬든 함수가 됬든 재 호출 하는 경우는 없어야 함

// public class PlayerLife : MonoBehaviour
// {
//     ILifeSkill lifestate;

//     public InteractionObject nearObject = null;

//     public bool isFarming = false;

//     private Transform myTransform;

//     // 생활 종류에 따라 스킬을 넣어줌
//      // private Dictionary<LifeType, Dictionary<Enum, ILifeSkill>> lifeStateDic = new Dictionary<LifeType, Dictionary<Enum, ILifeSkill>>();
//     private Dictionary<Enum, ILifeSkill> lifeStateDic = new Dictionary<Enum, ILifeSkill>();
//     private void OnTriggerEnter(Collider other) {
//         Debug.Log(other);
//         // 근처에 있는 오브젝트 판별
//         nearObject = other.GetComponent<InteractionObject>();    
//     }

//     private void Awake() {
//         myTransform = transform;
//         InitLifeState();
//     }

//     private void OnTriggerExit(Collider other) {
//         // 근처에 있는 오브젝트 해제
//         nearObject = null;
//     }

//        public IEnumerator PlayerInteraction(){
//         // 플레이어가 chunk매니저에게 허락을 받아야함.
//         if (!isFarming){
//             isFarming = true;              // 나중에 chunkManager에 허락을 받는 코드로 바꾸기

//             // 1. 근처오브젝트로 다가감
//             yield return StartCoroutine(MoveToNearObject());
//             // 2. 캐는 애니메이션 실행 및 UI 켜기
//             //yield return StartCoroutine(WaitFarmingTime(nearObject.durationTime));
//             // 3. 오브젝트 정보 전송
//             nearObject.Send();
//             // 따로 스폰처리는 나중에
//             isFarming = false;
//         }
//     }

//     IEnumerator MoveToNearObject(){
//         float distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
//         while(distance > 0.25f){        // 임시로 지정한 거리(0.25f) 근처까지 도달했을 때 실행
//             // 임시 이동 코드
//             distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
//             Vector3 direction = nearObject.transform.position - myTransform.position;
//             myTransform.position += direction.normalized * 3f * Time.deltaTime;

//             yield return null;
//         }
//     }

//     // IEnumerator WaitFarmingTime(float durationTime){
//     //     Debug.Log("나실행했어요");
//     //     float time = 0;
       


//     //     // 
//     //     lifestate.OperateEnter();

//     //     while(durationTime > time)
//     //     {
//     //         lifestate.OperateUpdate();
//     //         time += 0.01f;
//     //         yield return new WaitForSeconds(0.01f);
//     //     }
//     //     lifestate.OperateExit();
        
    
//     // }

//     void DoFarming()
//     {
//         // 함수내에서 선언하면 
//         // ILifeSkill lifestate;

//         CheckObjType(out lifestate);
//         lifestate.InteractionObject = nearObject;
//         lifestate.ChooseLifeType();

//     }

//     void InitLifeState()
//     {
//         lifeStateDic.Add(FishingType.Rod, new GroundState());
//         lifeStateDic.Add(FarmingType.GroundPlant, new GroundState());

//         lifeStateDic[FishingType.Rod].ExitLifeSkill();

//         Dictionary<Enum, ILifeSkill> FarmingState = new Dictionary<Enum, ILifeSkill>();
//         Dictionary<Enum, ILifeSkill> FishingState = new Dictionary<Enum, ILifeSkill>();
//         Dictionary<Enum, ILifeSkill> LiveStockState = new Dictionary<Enum, ILifeSkill>();
//         Dictionary<Enum, ILifeSkill> MiningState = new Dictionary<Enum, ILifeSkill>();
//         Dictionary<Enum, ILifeSkill> WoodCuttingState = new Dictionary<Enum, ILifeSkill>();
        
//         FarmingState.Add(FarmingType.GroundPlant, new GroundState());
//         FarmingState.Add(FarmingType.UnderGroundPlant, new UnGroundState());

//         FishingState.Add(FishingType.Rod, new RodState());
//         FishingState.Add(FishingType.Net, new NetState());

//         LiveStockState.Add(LivestockType.Meat, new MeatState());
//         LiveStockState.Add(LivestockType.Leather, new LeatherState());
//         LiveStockState.Add(LivestockType.ByProduct, new ByProductState());

//         MiningState.Add(MiningType.Pick, new PickState());

//         WoodCuttingState.Add(WoodcuttingType.Tree, new TreeState());
//         WoodCuttingState.Add(WoodcuttingType.FruitTree, new FruitTreeState());
//         WoodCuttingState.Add(WoodcuttingType.FlowerTree, new FlowerTreeState());

//         lifeStateDic.Add(LifeType.Farming, FarmingState);
//         lifeStateDic.Add(LifeType.Fishing, FishingState);
//         lifeStateDic.Add(LifeType.Livestock, LiveStockState);
//         lifeStateDic.Add(LifeType.Mining, MiningState);
//         lifeStateDic.Add(LifeType.Woodcutting, WoodCuttingState);
//         // testDic[LifeType.Woodcutting][WoodcuttingType.Tree].OperateEnter();

//     }

//     void CheckObjType(out ILifeSkill lifestate)
//     {
//         lifestate = null;
//         if (nearObject is TreeObject)
//         {
//             lifestate = lifeStateDic[nearObject.lifeType][(nearObject as TreeObject).woodcuttingType];
//             //lifeStateDic[nearObject.lifeType][(nearObject as TreeObject).woodcuttingType].OperateEnter();
//             //LifeStateMachine.SetState(lifeStateDic[nearObject.lifeType][(nearObject as TreeObject).woodcuttingType]);
//             // stateMachine.SetState(dicState[PlayerState.Dead]);
//         }
//         else if(nearObject is PlantObject)
//         {
//             lifestate = lifeStateDic[nearObject.lifeType][(nearObject as PlantObject).farmingType];
//         }
//         else if(nearObject is FishingAreaObject)
//         {
//             lifestate = lifeStateDic[nearObject.lifeType][(nearObject as FishingAreaObject).fishingType];
//         }
//         else if(nearObject is LivestockObject)
//         {
//             lifestate = lifeStateDic[nearObject.lifeType][(nearObject as LivestockObject).livestockType];
//         }
//         else if(nearObject is MineralObject)
//         {
//             lifestate = lifeStateDic[nearObject.lifeType][(nearObject as MineralObject).miningType];
//         }
//         else
//         {
//             Debug.Log("정의되지 않은 오브젝트 타입입니다.");
//         }
//     }

// }
