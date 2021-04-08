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

            if (life is FishingType){
                Debug.Log("����� ����");
                state = PlayerAnimState.Fishing_Rod_Start;
            }
            if (life is FarmingType){
                Debug.Log("Ǯ�� ĳ��");

                state = PlayerAnimState.Farming_Ground;
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
            current_state = PlayerAnimState.Move;
            parameterActions[moveInfo.parmeterType](moveInfo.stateName, value.normalized.magnitude);
        }

        public void UpdateMove(Vector3 value, bool changeState){
            if (changeState){
                current_state = PlayerAnimState.Move;
            }
            parameterActions[moveInfo.parmeterType](moveInfo.stateName, value.normalized.magnitude);
        }
    }
}