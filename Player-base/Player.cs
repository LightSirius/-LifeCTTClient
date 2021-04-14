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

        private void Start() {
            playerMovement = GetComponent<PlayerMovement>();
            playerAnimController = GetComponent<PlayerAnimController>();
        }

        private void OnEnable() {
            // PlayerMovement �߰� �� �ּ� ���� �ٶ�
            // InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Move);
            // InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Turn);
            InputManager.Instance.arrowKeyEvent.AddListener(playerAnimController.UpdateMove);

            InputManager.Instance.arrowKeyEvent.AddListener(CheckMove);
            InputManager.Instance.AddKeyDownListenr(KeyCode.G, DoSkill);
        }

        private void OnDisable() {
            // PlayerMovement �߰� �� �ּ� ���� �ٶ�
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
                
                // Life UI â ����
            }
        }

        private void OnTriggerStay(Collider other) {
            if (nearInteractionObject == null){
                nearInteractionObject = other.GetComponent<IInteraction>();

                if (!nearInteractionObject.IsEnable){
                    nearInteractionObject = null;
                    return;
                }

                // Life UIâ ����
            }
        }

        private void OnTriggerExit(Collider other) {
            if (nearInteractionObject != null){
                nearInteractionObject = null;       // �÷��̾ �ν��ϰ��ִ� interaction Object ����

                // Life UIâ ����
            }
        }

        // �÷��̾ �����϶� ��ų ��� �� �ִϸ��̼��� �����ϴ� �Լ�
        // InputManager �̺�Ʈ�� OnEnable�� �����
        // OnDisable�� �̺�Ʈ ��� ����
        private void CheckMove(Vector2 direction){
            if (direction.SqrMagnitude() > 0f && playerState == PlayerState.Skill){
                // �ִϸ��̼� ���� ��ũ��Ʈ �ۼ� �ʿ�

                // ���� ������� ��ų ���� ��ũ��Ʈ �ۼ� �ʿ�
                // UI �ʱ�ȭ �Լ� ����
            }

            playerState = PlayerState.Move;
        }

        private void DoSkill(){
            if (nearInteractionObject != null && nearInteractionObject.IsEnable && playerState != PlayerState.Skill){
                playerState = PlayerState.Skill;
                // ��Ȱ ��ų
                // 1. nearObject�ȿ� �ִ� type�� �����ͼ� ��

                // 2. �ִϸ��̼� ����
            }
        }
    }
}