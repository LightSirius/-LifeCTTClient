// using System.Collections;
// using System.Collections.Generic;
// using System;
// using UnityEngine;
// using LifeContent;


//     // ��Ȱ�������� �Ҷ�
//     // ����_����, ����_��, ��������
//     // ���� Ȯ�� �ε��� <- �� ������ �ʱ�ȭ �������
//     // DB���� ���� �޾ƿͼ� �ʱ�ȭ �ؾ���

//     // ��Ǿ �ȿ����� ��밡 �����ϱ� Start�� ��� awake�� ��� ������� init�� ������ �������� ����
//     // �׷��ٰ� �����Լ��� ���ڴ� �̰� �� �����޸����� �Ź� 4�� �Ŵ´���
//     // ��Ǿ ���ο� �����ڸ� ���ؼ� �ʱⰪ�� ��������
//     // private Dictionary<LifeType, Dictionary<Enum, ILifeSkill>> lifeStateDic = new Dictionary<LifeType, Dictionary<Enum, ILifeSkill>>();
//     // �ش� ������� ������ �������� ��
//     // �ʱⰪ�� �����ڸ� ���� init��

//     // ��Ǿ �����ڸ� ������
//     // ��Ǿ �ȿ� ��û���� ���� ��ũ��Ʈ�� �����ϱ�
//     // �� ��ũ��Ʈ ���� �����ڿ��� DBȣ���� �Ѵ�? ���� �f -> �ش� ��ũ��Ʈ���� ������ �ּ� 3�� ����
//     // �׷� DB���� �� ��� ������?

//     // DB Init Manager ��ũ��Ʈ ����
//     // DB�� ��Ȱ�� ���õ� ������ ���� ������Ʈ�� �� �ܾ�´��� (ȣ�� 1ȸ)

//     // DB Init Manager �� ���� ������ ������ ����.
//     // �׸��� ��Ǿ ������ ���� DB Init Manager�� �ִ� ���� ������ �����ؼ� ���� init��
    
//     // �׸��� DB Init Manager�� ���������� �ɵ�? (�ϴ��� ���ܵδ� ������� ����)
//     // ��� �ش� DB Init Manager�� ������ ��� �Լ��� ��� �� ȣ�� �ϴ� ���� ����� ��

// public class PlayerLife : MonoBehaviour
// {
//     ILifeSkill lifestate;

//     public InteractionObject nearObject = null;

//     public bool isFarming = false;

//     private Transform myTransform;

//     // ��Ȱ ������ ���� ��ų�� �־���
//      // private Dictionary<LifeType, Dictionary<Enum, ILifeSkill>> lifeStateDic = new Dictionary<LifeType, Dictionary<Enum, ILifeSkill>>();
//     private Dictionary<Enum, ILifeSkill> lifeStateDic = new Dictionary<Enum, ILifeSkill>();
//     private void OnTriggerEnter(Collider other) {
//         Debug.Log(other);
//         // ��ó�� �ִ� ������Ʈ �Ǻ�
//         nearObject = other.GetComponent<InteractionObject>();    
//     }

//     private void Awake() {
//         myTransform = transform;
//         InitLifeState();
//     }

//     private void OnTriggerExit(Collider other) {
//         // ��ó�� �ִ� ������Ʈ ����
//         nearObject = null;
//     }

//        public IEnumerator PlayerInteraction(){
//         // �÷��̾ chunk�Ŵ������� ����� �޾ƾ���.
//         if (!isFarming){
//             isFarming = true;              // ���߿� chunkManager�� ����� �޴� �ڵ�� �ٲٱ�

//             // 1. ��ó������Ʈ�� �ٰ���
//             yield return StartCoroutine(MoveToNearObject());
//             // 2. ĳ�� �ִϸ��̼� ���� �� UI �ѱ�
//             //yield return StartCoroutine(WaitFarmingTime(nearObject.durationTime));
//             // 3. ������Ʈ ���� ����
//             nearObject.Send();
//             // ���� ����ó���� ���߿�
//             isFarming = false;
//         }
//     }

//     IEnumerator MoveToNearObject(){
//         float distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
//         while(distance > 0.25f){        // �ӽ÷� ������ �Ÿ�(0.25f) ��ó���� �������� �� ����
//             // �ӽ� �̵� �ڵ�
//             distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
//             Vector3 direction = nearObject.transform.position - myTransform.position;
//             myTransform.position += direction.normalized * 3f * Time.deltaTime;

//             yield return null;
//         }
//     }

//     // IEnumerator WaitFarmingTime(float durationTime){
//     //     Debug.Log("�������߾��");
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
//         // �Լ������� �����ϸ� 
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
//             Debug.Log("���ǵ��� ���� ������Ʈ Ÿ���Դϴ�.");
//         }
//     }

// }
