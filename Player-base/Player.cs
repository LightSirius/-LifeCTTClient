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
        private Vector3 moveValue = Vector3.zero;
        private Coroutine skillCoroutine = null;

        private void Awake() {
            playerMovement = GetComponent<PlayerMovement>();
            playerAnimController = GetComponent<PlayerAnimController>();
        }

        private void OnEnable() {
            InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Move);
            InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Turn);

            InputManager.Instance.arrowKeyEvent.AddListener(playerAnimController.UpdateMove);

            // 키 이벤트 추가
            InputManager.Instance.AddKeyDownListenr(KeyCode.G, DoSkill);
            InputManager.Instance.AddKeyDownListenr(KeyCode.Space, playerMovement.Jump);
        }

        // private void OnDisable() {
        //     if (InputManager.Instance != null){
        //         InputManager.Instance.arrowKeyEvent.RemoveListener(playerMovement.Move);
        //         InputManager.Instance.arrowKeyEvent.RemoveListener(playerMovement.Turn);
            
        //         InputManager.Instance.arrowKeyEvent.RemoveListener(playerAnimController.UpdateMove);

        //         InputManager.Instance.RemoveKeyDownListenr(KeyCode.G, DoSkill);
        //         InputManager.Instance.RemoveKeyDownListenr(KeyCode.Space, playerMovement.Jump);
        //     }
        // }

        private void FixedUpdate() {
            playerMovement.ForceGravity();
        }

        private void OnTriggerEnter(Collider other) {
            nearInteractionObject = other.GetComponent<IInteraction>();

            if (nearInteractionObject != null){
                if (!nearInteractionObject.IsEnable){
                    nearInteractionObject = null;
                    return;
                }
            }
        }

        private void OnTriggerStay(Collider other) {
            if (nearInteractionObject == null){
                nearInteractionObject = other.GetComponent<IInteraction>();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (nearInteractionObject != null){
                nearInteractionObject = null;       // 플레이어가 인식하고있던 interaction Object 해제
            }
        }

        private void DoSkill(){
            if (nearInteractionObject != null && playerState != PlayerState.Skill){
                if (skillCoroutine == null){
                    skillCoroutine = StartCoroutine(DoLifeSkill());
                }
            }
        }

        private IEnumerator DoLifeSkill()
        {
            playerState = PlayerState.Skill;
            float time = 0;
            // 1. nearObject안에 있는 type을 가져와서 씀

            // 2. 애니메이션 실행
            playerAnimController.ChangeState(nearInteractionObject.lifeType, nearInteractionObject.Type);

            // 3. UI실행 ( 소요바 및 텍스트 )
            string text = nearInteractionObject.ToString() + "을 채집중입니다...";
            UIMgr.Instance.SetSkillUI(nearInteractionObject.DurationTime, text);
            
            // 4. 0.1초 주기마다 실행해야 할 것들 ?
            while(time <= nearInteractionObject.DurationTime)
            {
                time += 0.1f;
                if (playerMovement.velocity.sqrMagnitude > 0f){
                    // 상태변환
                    playerState = PlayerState.Move;

                    // 애니메이션 종료 스크립트 작성 필요
                    playerAnimController.ChangeState(PlayerState.Move);

                    // UI 초기화 함수 실행
                    UIMgr.Instance.InitUI();

                    if (skillCoroutine != null){
                        StopCoroutine(skillCoroutine);
                        skillCoroutine = null;
                    }
                } // 서버에 데이터 전송 ?
               

                yield return new WaitForSeconds(0.1f);
            }

            // 5. 성공 애니메이션 실행
            playerState = PlayerState.Move;
            playerAnimController.ChangeState(PlayerState.Move);

            // 6. 아이템 얻음

            // 7. UI 실행

            // 8. 3초 뒤 UI 초기화

            skillCoroutine = null;
        }
    }
}