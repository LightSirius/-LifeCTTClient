using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeContent;
using Anim;
using Manager;

namespace Player{
    public enum PlayerState{
        Move,
        Skill,
        Gesture
    }

    // Player script
    // 역할 : player status, player animation, player life skill 을 관리
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimController))]
    public class Player : MonoBehaviour {

        private IInteraction nearInteractionObject = null; // 상호작용 오브젝트 (채집 가능한 오브젝트)
        private PlayerMovement playerMovement;      // 플레이어 움직이기, 점프
        private PlayerStatus playerStatus;          // 생활 스테이터스
        private PlayerState playerState;            // 플레이어 현재 상태
        private PlayerAnimController playerAnimController;
        // Life Skill 스크립트 추가 바람

        private void Start() {
            playerMovement = GetComponent<PlayerMovement>();
            playerAnimController = GetComponent<PlayerAnimController>();
        }

        private void OnEnable() {
            // PlayerMovement 추가 후 주석 해제 바람
            // InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Move);
            // InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Turn);
            InputManager.Instance.arrowKeyEvent.AddListener(playerAnimController.UpdateMove);

            InputManager.Instance.arrowKeyEvent.AddListener(CheckMove);
            InputManager.Instance.AddKeyDownListenr(KeyCode.G, DoSkill);
        }

        private void OnDisable() {
            // PlayerMovement 추가 후 주석 해제 바람
            // InputManager.Instance.arrowKeyEvent.RemoveListener(playerMovement.Move);
            // InputManager.Instance.arrowKeyEvent.RemoveListener(playerMovement.Turn);
            InputManager.Instance.arrowKeyEvent.RemoveListener(CheckMove);
            InputManager.Instance.RemoveKeyDownListenr(KeyCode.G, DoSkill);
        }

        private void OnTriggerEnter(Collider other) {
            if (nearInteractionObject == null){
                nearInteractionObject = other.GetComponent<IInteraction>();

                if (!nearInteractionObject.IsEnable){
                    nearInteractionObject = null;
                    return;
                }
                
                // Life UI 창 띄우기
            }
        }

        private void OnTriggerStay(Collider other) {
            if (nearInteractionObject == null){
                nearInteractionObject = other.GetComponent<IInteraction>();

                if (!nearInteractionObject.IsEnable){
                    nearInteractionObject = null;
                    return;
                }

                // Life UI창 띄우기
            }
        }

        private void OnTriggerExit(Collider other) {
            if (nearInteractionObject != null){
                nearInteractionObject = null;       // 플레이어가 인식하고있던 interaction Object 해제

                // Life UI창 해제
            }
        }

        // 플레이어가 움직일때 스킬 사용 및 애니메이션을 종료하는 함수
        // InputManager 이벤트에 OnEnable시 등록함
        // OnDisable시 이벤트 등록 해제
        private void CheckMove(Vector2 direction){
            if (direction.SqrMagnitude() > 0f && playerState == PlayerState.Skill){
                // 애니메이션 종료 스크립트 작성 필요

                // 현재 사용중인 스킬 종료 스크립트 작성 필요
                // UI 초기화 함수 실행
            }

            playerState = PlayerState.Move;
        }

        private void DoSkill(){
            if (nearInteractionObject != null && nearInteractionObject.IsEnable && playerState != PlayerState.Skill){
                playerState = PlayerState.Skill;
                // 생활 스킬
                // 1. nearObject안에 있는 type을 가져와서 씀

                // 2. 애니메이션 실행
            }
        }
    }
}