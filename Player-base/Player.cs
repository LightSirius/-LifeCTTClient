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

        private void Awake() {
            playerMovement = GetComponent<PlayerMovement>();
            playerAnimController = GetComponent<PlayerAnimController>();
        }

        private void OnEnable() {
            InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Move);
            InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Turn);

            InputManager.Instance.arrowKeyEvent.AddListener(playerAnimController.UpdateMove);

            InputManager.Instance.arrowKeyEvent.AddListener(CheckMove);

            // 키 이벤트 추가
            InputManager.Instance.AddKeyDownListenr(KeyCode.G, DoSkill);
            InputManager.Instance.AddKeyDownListenr(KeyCode.Space, playerMovement.Jump);
        }

        private void OnDisable() {
            if (InputManager.Instance != null){
                InputManager.Instance.arrowKeyEvent.RemoveListener(playerMovement.Move);
                InputManager.Instance.arrowKeyEvent.RemoveListener(playerMovement.Turn);
            
                InputManager.Instance.arrowKeyEvent.RemoveListener(playerAnimController.UpdateMove);

                InputManager.Instance.arrowKeyEvent.RemoveListener(CheckMove);

                InputManager.Instance.RemoveKeyDownListenr(KeyCode.G, DoSkill);
                InputManager.Instance.RemoveKeyDownListenr(KeyCode.Space, playerMovement.Jump);
            }
        }

        private void FixedUpdate() {
            playerMovement.ForceGravity();
        }

        private void OnTriggerEnter(Collider other) {
            if (nearInteractionObject == null){
                nearInteractionObject = other.GetComponent<IInteraction>();

                if (!nearInteractionObject.IsEnable){
                    nearInteractionObject = null;
                    return;
                }
            }
        }

        private void OnTriggerStay(Collider other) {
            if (nearInteractionObject == null){
                nearInteractionObject = other.GetComponent<IInteraction>();

                // if (!nearInteractionObject.IsEnable){
                //     nearInteractionObject = null;
                //     return;
                // }
            }
        }

        private void OnTriggerExit(Collider other) {
            if (nearInteractionObject != null){
                nearInteractionObject = null;       // 플레이어가 인식하고있던 interaction Object 해제
            }
        }

        // 플레이어가 움직일때 스킬 사용 및 애니메이션을 종료하는 함수
        // InputManager 이벤트에 OnEnable시 등록함
        // OnDisable시 이벤트 등록 해제
        private void CheckMove(Vector2 direction){
            if (direction.SqrMagnitude() > 0f){
                // 애니메이션 종료 스크립트 작성 필요
                playerAnimController.ChangeState(PlayerState.Move);

                // 현재 사용중인 스킬 종료 스크립트 작성 필요
                StopCoroutine(DoLifeSkill());
                StopCoroutine(PlayerLifeInteraction());
                // UI 초기화 함수 실행
            }

            playerState = PlayerState.Move;
        }

        private void DoSkill(){
            Debug.Log("실행 ");
            if (nearInteractionObject != null){
                // playerAnimController.ChangeState(nearInteractionObject.lifeType, nearInteractionObject.Type);
                StartCoroutine(PlayerLifeInteraction());
            }
        }

        // 기본적으로 사용전 if문으로 nearObject가 null이 아닌지 확인 후 실행
        public IEnumerator PlayerLifeInteraction()
        {
            // 근처 오브젝트로 다가가는 코루틴 실행

            // 오브젝트가 파밍가능한 상태고 플레이어상태가 Skill이 아니고 (추가사항) 생활력이 충분하다면
            Debug.Log("움직여!!");
            // 라이프스킬 실행
            yield return StartCoroutine(DoLifeSkill());
        }

        private IEnumerator DoLifeSkill()
        {
            playerState = PlayerState.Skill;
            float time = 0;
            // 1. nearObject안에 있는 type을 가져와서 씀

            // 2. 애니메이션 실행
            playerAnimController.ChangeState(nearInteractionObject.lifeType, nearInteractionObject.Type);
            yield return null;

            // 3. UI실행 ( 소요바 및 텍스트 )

            // 4. 0.1초 주기마다 실행해야 할 것들 ?
            while(time <= nearInteractionObject.DurationTime)
            {
                time += 0.1f;
                // 서버에 데이터 전송 ?

                yield return new WaitForSeconds(0.1f);
            }
            //playerState = PlayerState.Gesture;
            playerAnimController.ChangeState(PlayerState.Move);
            // 5. 성공 애니메이션 실행

            // 6. 아이템 얻음

            // 7. UI 실행

            // 8. 3초 뒤 UI 초기화
            yield return new WaitForSeconds(3f);

        }
    }
}