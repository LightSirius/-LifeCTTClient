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

        private void Awake() {
            playerMovement = GetComponent<PlayerMovement>();
            playerAnimController = GetComponent<PlayerAnimController>();
        }

        private void OnEnable() {
            // InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Move);
            InputManager.Instance.arrowKeyEvent.AddListener(playerMovement.Turn);

            InputManager.Instance.arrowKeyEvent.AddListener(playerAnimController.UpdateMove);

            InputManager.Instance.arrowKeyEvent.AddListener(CheckMove);

            // Ű �̺�Ʈ �߰�
            // InputManager.Instance.AddKeyDownListenr(KeyCode.G, DoSkill);
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

        // �⺻������ ����� if������ nearObject�� null�� �ƴ��� Ȯ�� �� ����
        public IEnumerator PlayerLifeInteraction()
        {
            // ��ó ������Ʈ�� �ٰ����� �ڷ�ƾ ����

            // ������Ʈ�� �Ĺְ����� ���°� �÷��̾���°� Skill�� �ƴϰ� (�߰�����) ��Ȱ���� ����ϴٸ�
            if(nearInteractionObject.IsEnable && playerState != PlayerState.Skill)
            {
                // ��������ų ����
                StartCoroutine(DoLifeSkill());
            }
            else if(!nearInteractionObject.IsEnable)
            {
                Debug.Log("ä�� ������ ���°� �ƴմϴ�.");
            }
            // else if(��Ȱ���� ������� �ʴٸ�) 

            yield break;
        }

        private IEnumerator DoLifeSkill()
        {
            playerState = PlayerState.Skill;
            float time = 0;
            // 1. nearObject�ȿ� �ִ� type�� �����ͼ� ��

            // 2. �ִϸ��̼� ����

            // 3. UI���� ( �ҿ�� �� �ؽ�Ʈ )

            // 4. 0.1�� �ֱ⸶�� �����ؾ� �� �͵� ?
            while(time <= nearInteractionObject.DurationTime)
            {
                time += 0.1f;
                // ������ ������ ���� ?

                yield return new WaitForSeconds(0.1f);
            }
            playerState = PlayerState.Gesture;
            // 5. ���� �ִϸ��̼� ����

            // 6. ������ ����

            // 7. UI ����

            // 8. 3�� �� UI �ʱ�ȭ
            yield return new WaitForSeconds(3f);

        }
    }
}