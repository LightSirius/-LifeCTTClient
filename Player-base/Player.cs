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
    // ���� : player status, player animation, player life skill �� ����
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimController))]
    public class Player : MonoBehaviour {

        private IInteraction nearInteractionObject = null; // ��ȣ�ۿ� ������Ʈ (ä�� ������ ������Ʈ)
        private PlayerMovement playerMovement;      // �÷��̾� �����̱�, ����
        private PlayerStatus playerStatus;          // ��Ȱ �������ͽ�
        private PlayerState playerState;            // �÷��̾� ���� ����
        private PlayerAnimController playerAnimController;
        // Life Skill ��ũ��Ʈ �߰� �ٶ�
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

            // Ű �̺�Ʈ �߰�
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
                nearInteractionObject = null;       // �÷��̾ �ν��ϰ��ִ� interaction Object ����
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
            // 1. nearObject�ȿ� �ִ� type�� �����ͼ� ��

            // 2. �ִϸ��̼� ����
            playerAnimController.ChangeState(nearInteractionObject.lifeType, nearInteractionObject.Type);

            // 3. UI���� ( �ҿ�� �� �ؽ�Ʈ )
            string text = nearInteractionObject.ToString() + "�� ä�����Դϴ�...";
            UIMgr.Instance.SetSkillUI(nearInteractionObject.DurationTime, text);
            
            // 4. 0.1�� �ֱ⸶�� �����ؾ� �� �͵� ?
            while(time <= nearInteractionObject.DurationTime)
            {
                time += 0.1f;
                if (playerMovement.velocity.sqrMagnitude > 0f){
                    // ���º�ȯ
                    playerState = PlayerState.Move;

                    // �ִϸ��̼� ���� ��ũ��Ʈ �ۼ� �ʿ�
                    playerAnimController.ChangeState(PlayerState.Move);

                    // UI �ʱ�ȭ �Լ� ����
                    UIMgr.Instance.InitUI();

                    if (skillCoroutine != null){
                        StopCoroutine(skillCoroutine);
                        skillCoroutine = null;
                    }
                } // ������ ������ ���� ?
               

                yield return new WaitForSeconds(0.1f);
            }

            // 5. ���� �ִϸ��̼� ����
            playerState = PlayerState.Move;
            playerAnimController.ChangeState(PlayerState.Move);

            // 6. ������ ����

            // 7. UI ����

            // 8. 3�� �� UI �ʱ�ȭ

            skillCoroutine = null;
        }
    }
}