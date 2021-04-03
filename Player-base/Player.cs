using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public bool isFarming = false;      // ��Ȱ(ä��, ���� ��)�� �ϰ����� ��� true ���ϰ� ���� ��� false

    private Transform myTransform;
    private InteractionObject nearObject;

    private void Start() {
        myTransform = transform;
    }

    private void OnDestroy() {
        myTransform = null;
    }

    private void Update() {
        // Ű�Է�
        if (Input.GetKeyDown(KeyCode.G)){
            // ���࿡ �ָ������� �ٰ�����
            // ������ ������ �ٷ� ä��
            if (nearObject){
                StartCoroutine(PlayerInteraction());
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

        yield return new WaitForSeconds(durationTime);
    }
}