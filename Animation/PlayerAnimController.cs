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
                Debug.Log("물고기 잡힘");
                state = PlayerAnimState.Fishing_Rod_Start;
            }
            if (life is FarmingType){
                Debug.Log("풀이 캐짐");

                state = PlayerAnimState.Farming_Ground;
            }

            return state;
        }

        // 플레이어의 상태를 변환시키는 함수
        public override void ChangeState(PlayerAnimState next_state, object value = null){
            if (current_state != next_state){       // 현재 상태가 다음 상태와 다르면 상태 변환
                current_state = next_state;
                
                // exit 미지정 시 종료
                if (exitInfo == null){
                    Debug.LogError("Exit State 미지정 정의 필요");
                    return;
                }

                // 플레이어 현재 애니메이션 종료
                parameterActions[exitInfo.parmeterType](exitInfo.stateName, value);

                // 플레이어 다음 애니메이션 상태 지정 후 실행
                AnimInfo<PlayerAnimState> next_info = animationState[next_state];
                parameterActions[next_info.parmeterType](next_info.stateName, value);
            }
            else{
                // 현재 상태가 다음 상태와 같을 경우 아무것도 하지 않음
                return;
            }
        }

        // 플레이어 움직일때마다 이 함수 실행 필요
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