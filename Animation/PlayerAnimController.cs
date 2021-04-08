using System;
using System.Collections.Generic;
using UnityEngine;
using LifeContent;

namespace Anim{
    public enum PlayerAnimState{
        Move,
        Exit,
        Gesture_Success,
        Gesture_Fail,
        Farming_Ground,
        Farming_Underground,
        Woodcutting_Fruit,
        Woodcutting_Flower,
        Mining_Pickaxe,
        Livestock_Leather,
        Livestock_Meat,
        Livestock_Milk,
        Fishing_Rod_Start,
        Fishing_Rod_Idle,
        Fishing_Rod_End
    }

    public class PlayerAnimController : AnimationController<PlayerAnimState> {
        
        AnimInfo<PlayerAnimState> moveInfo;
        AnimInfo<PlayerAnimState> exitInfo;

        protected override void Start() {
            base.Start();

            moveInfo = animationState[PlayerAnimState.Move];
            exitInfo = animationState[PlayerAnimState.Exit];
        }

        public static PlayerAnimState GetLifeTypeToPlayerAnimState(Enum life){
            PlayerAnimState state = PlayerAnimState.Exit;

            switch(life){
                case WoodcuttingType.Tree:
                    // ���� �ִϸ��̼���  �������� ����
                    break;
                case WoodcuttingType.FruitTree:
                    state = PlayerAnimState.Woodcutting_Fruit;
                    break;
                case WoodcuttingType.FlowerTree:
                    state = PlayerAnimState.Woodcutting_Flower;
                    break;
                case FishingType.Rod:
                    state = PlayerAnimState.Fishing_Rod_Start;
                    break;
                case FishingType.Net:
                    // ���� �ִϸ��̼���  �������� ����
                    break;
                case FarmingType.GroundPlant:
                    state = PlayerAnimState.Farming_Ground;
                    break;
                case FarmingType.UnderGroundPlant:
                    state = PlayerAnimState.Farming_Underground;
                    break;
                case LivestockType.Meat:
                    state = PlayerAnimState.Livestock_Meat;
                    break;
                case LivestockType.Leather:
                    state = PlayerAnimState.Livestock_Leather;
                    break;
                case LivestockType.ByProduct:
                    state = PlayerAnimState.Livestock_Milk;
                    break;
            }

            return state;
        }

        // �÷��̾��� ���¸� ��ȯ��Ű�� �Լ�
        public override void ChangeState(PlayerAnimState next_state, object value = null){
            if (current_state != next_state){       // ���� ���°� ���� ���¿� �ٸ��� ���� ��ȯ
                current_state = next_state;
                
                // exit ������ �� ����
                if (exitInfo == null){
                    Debug.LogError("Exit State ������ ���� �ʿ�");
                    return;
                }

                // �÷��̾� ���� �ִϸ��̼� ����
                parameterActions[exitInfo.parmeterType](exitInfo.stateName, value);

                // �÷��̾� ���� �ִϸ��̼� ���� ���� �� ����
                AnimInfo<PlayerAnimState> next_info = animationState[next_state];
                parameterActions[next_info.parmeterType](next_info.stateName, value);
            }
            else{
                // ���� ���°� ���� ���¿� ���� ��� �ƹ��͵� ���� ����
                return;
            }
        }

        // �÷��̾� �����϶����� �� �Լ� ���� �ʿ�
        public void UpdateMove(Vector3 value){
            if (current_state != PlayerAnimState.Move){
                current_state = PlayerAnimState.Move;
            }
            parameterActions[moveInfo.parmeterType](moveInfo.stateName, value.normalized.magnitude);
        }
    }
}