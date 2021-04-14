using System;
using System.Collections.Generic;
using UnityEngine;
using LifeContent;
using Player;

namespace Anim{
    public enum Gesture{
        Hi = 0,
        Success,
        Fail
    }

    public class PlayerAnimController : AnimationController {

        protected override void Start() {
            base.Start();

            current_state = PlayerState.Move;
        }

        protected override void InitAnimState(){
            animationState.Add(PlayerState.Move, new AnimInfo(PlayerState.Move, ParmeterType.Boolean, "Move"));

            animationState.Add(PlayerState.Gesture, new AnimInfo(PlayerState.Gesture, ParmeterType.Boolean, "Gesture"));
            
            animationState.Add(LifeType.Farming, new AnimInfo(LifeType.Farming, ParmeterType.Boolean, "Farming"));
            animationState.Add(LifeType.Fishing, new AnimInfo(LifeType.Fishing, ParmeterType.Boolean, "Fishing"));
            animationState.Add(LifeType.Mining, new AnimInfo(LifeType.Mining, ParmeterType.Boolean, "Mining"));
            animationState.Add(LifeType.Livestock, new AnimInfo(LifeType.Livestock, ParmeterType.Boolean, "Livestock"));
            animationState.Add(LifeType.Woodcutting, new AnimInfo(LifeType.Woodcutting, ParmeterType.Boolean, "Woodcutting"));
        }

        // 플레이어의 상태를 변환시키는 함수
        public override void ChangeState(Enum next_state, Enum type = null, object value = null){
            if (next_state != current_state){
                AnimInfo currentInfo = animationState[current_state];
                parameterActions[currentInfo.parmeterType](currentInfo.stateName, false);
                
                if (type != null){
                    parameterActions[ParmeterType.Integer]("Type", type);
                }

                AnimInfo next_info = animationState[next_state];
                parameterActions[next_info.parmeterType](next_info.stateName, true);
            }
        }

        // 플레이어 움직일때마다 이 함수 실행 필요
        public void UpdateMove(Vector2 value){
            if (current_state.Equals(PlayerState.Move)){
                parameterActions[ParmeterType.Float]("MoveValue", value.normalized.magnitude);
            }
        }
    }
}