// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using LifeContent;
// using Player;

// namespace Anim{

//     public class PlayerAnimController : AnimationController<PlayerState> {
        
//         AnimInfo<PlayerState> moveInfo;
//         AnimInfo<PlayerState> exitInfo;

//         protected override void Start() {
//             base.Start();

//             moveInfo = animationState[PlayerState.Move];
//             exitInfo = animationState[PlayerState.Exit];
//         }

//         // public static PlayerState GetLifeTypeToPlayerState(Enum life){
//         //     PlayerState state = PlayerState.Exit;

//         //     switch(life){
//         //         case WoodcuttingType.Tree:
//         //             // ���� �ִϸ��̼���  �������� ����
//         //             break;
//         //         case WoodcuttingType.FruitTree:
//         //             state = PlayerState.Woodcutting_Fruit;
//         //             break;
//         //         case WoodcuttingType.FlowerTree:
//         //             state = PlayerState.Woodcutting_Flower;
//         //             break;
//         //         case FishingType.Rod:
//         //             state = PlayerState.Fishing_Rod_Start;
//         //             break;
//         //         case FishingType.Net:
//         //             // ���� �ִϸ��̼���  �������� ����
//         //             break;
//         //         case FarmingType.GroundPlant:
//         //             state = PlayerState.Farming_Ground;
//         //             break;
//         //         case FarmingType.UnderGroundPlant:
//         //             state = PlayerState.Farming_Underground;
//         //             break;
//         //         case LivestockType.Meat:
//         //             state = PlayerState.Livestock_Meat;
//         //             break;
//         //         case LivestockType.Leather:
//         //             state = PlayerState.Livestock_Leather;
//         //             break;
//         //         case LivestockType.ByProduct:
//         //             state = PlayerState.Livestock_Milk;
//         //             break;
//         //     }

//         //     return state;
//         // }

//         // �÷��̾��� ���¸� ��ȯ��Ű�� �Լ�
//         public override void ChangeState(PlayerState next_state, object value = null){
//             if (current_state != next_state){       // ���� ���°� ���� ���¿� �ٸ��� ���� ��ȯ
//                 current_state = next_state;
                
//                 // exit ������ �� ����
//                 if (exitInfo == null){
//                     Debug.LogError("Exit State ������ ���� �ʿ�");
//                     return;
//                 }

//                 // �÷��̾� ���� �ִϸ��̼� ����
//                 parameterActions[exitInfo.parmeterType](exitInfo.stateName, value);

//                 // �÷��̾� ���� �ִϸ��̼� ���� ���� �� ����
//                 AnimInfo<PlayerState> next_info = animationState[next_state];
//                 parameterActions[next_info.parmeterType](next_info.stateName, value);
//             }
//             else{
//                 // ���� ���°� ���� ���¿� ���� ��� �ƹ��͵� ���� ����
//                 return;
//             }
//         }

//         // �÷��̾� �����϶����� �� �Լ� ���� �ʿ�
//         public void UpdateMove(Vector3 value){
//             if (current_state != PlayerState.Move){
//                 current_state = PlayerState.Move;
//             }
//             parameterActions[moveInfo.parmeterType](moveInfo.stateName, value.normalized.magnitude);
//         }
//     }
// }