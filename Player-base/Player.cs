using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    private enum PlayerState
    {
        Idle,
        Walk,
        Jump
    }


    private StateMachine stateMachine;

    private Dictionary<PlayerState, IState> dicState = new Dictionary<PlayerState, IState>();

    // TEST���Դϴ�.
    private Dictionary<LifeType.Kind, Dictionary<Enum, IState>> testDic = new Dictionary<LifeType.Kind, Dictionary<Enum, IState>>();

    public bool isFarming = false;      // ��Ȱ(ä��, ���� ��)�� �ϰ����� ��� true ���ϰ� ���� ��� false

    private Transform myTransform;
    private InteractionObject nearObject;

    private void Start() {
        myTransform = transform;

        IState idle = new IdleState();
        IState walk = new WalkState();
        IState jump = new JumpState();

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Walk, walk);
        dicState.Add(PlayerState.Jump, jump);

        // Dictionary<Enum, IState> woodCuttingState = new Dictionary<Enum, IState>();
        // woodCuttingState.Add(WoodcuttingType.Kind.FlowerTree, new FlowerTreeState());
        // woodCuttingState.Add(WoodcuttingType.Kind.Tree, new TreeState());

        // testDic.Add(LifeType.Kind.Woodcutting, woodCuttingState);

        // testDic[nearObject.lifeType][(nearObject as TreeObject).woodcuttingType].OperateEnter();
        // testDic[LifeType.Kind.Woodcutting][WoodcuttingType.Kind.Tree].OperateEnter();

        // �⺻���´� idle ���·� ����        
        stateMachine = new StateMachine(idle);    
    }
    void Update() {
        // Ű�Է�
        if (Input.GetKeyDown(KeyCode.G)){
            // ���࿡ �ָ������� �ٰ�����
            // ������ ������ �ٷ� ä��
            if (nearObject){
                StartCoroutine(PlayerInteraction());
            }
        }

        KeyboardInput();
        stateMachine.DoOperateUpdate();
    }
    void KeyboardInput()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            // idle�����̰ų� Walk�����϶��� ���� ����
            if(stateMachine.CurrentState == dicState[PlayerState.Idle] || stateMachine.CurrentState == dicState[PlayerState.Walk])
            {
                stateMachine.SetState(dicState[PlayerState.Jump]);
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        // ��ó�� �ִ� ������Ʈ �Ǻ�
        nearObject = other.GetComponent<InteractionObject>();    
    }

    private void OnTriggerExit(Collider other) {
        // ��ó�� �ִ� ������Ʈ ����
        nearObject = null;
    }

    // �ڷ�ƾ���� ��ƾ ����
    // �ָ������� -> ������ ���� ��
    // ������Ʈ ĳ�� �̺�Ʈ ����
    IEnumerator PlayerInteraction(){
        // �÷��̾ chunk�Ŵ������� ����� �޾ƾ���.
        isFarming = true;              // ���߿� chunkManager�� ����� �޴� �ڵ�� �ٲٱ�

        // 1. ��ó������Ʈ�� �ٰ���
        yield return StartCoroutine(MoveToNearObject());
        // 2. ĳ�� �ִϸ��̼� ���� �� UI �ѱ�
        yield return StartCoroutine(WaitFarmingTime(nearObject.durationTime));
        // 3. ������Ʈ ���� ����
        nearObject.Send();

        isFarming = false;
    }

    IEnumerator MoveToNearObject(){
        float distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
        while(distance > 0.25f){        // �ӽ÷� ������ �Ÿ�(0.25f) ��ó���� �������� �� ����
            // �ӽ� �̵� �ڵ�
            Vector3 direction = nearObject.transform.position - myTransform.position;
            myTransform.position += direction.normalized * 3f * Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator WaitFarmingTime(float durationTime){
        // ĳ���� �ִϸ��̼��� �����ϴ� �ڵ� �ۼ� �ʿ�
        Debug.Log("���𰡸� �ϴ� ���̴�...");

        // �÷��̾ ������Ʈ�� ��ȣ�ۿ��ϴ� �ڵ� �ʿ�
        

        yield return new WaitForSeconds(durationTime);
    }
}