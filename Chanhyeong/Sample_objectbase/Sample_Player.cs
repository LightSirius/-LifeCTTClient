using System.Collections.Generic;
using UnityEngine;

public class Sample_Player : MonoBehaviour {


    // ����Ʈ�� Dictionary 
    Dictionary<Sample_Interction.LifeKind, Sample_ILife> lifes = new Dictionary<Sample_Interction.LifeKind, Sample_ILife>();

    public Sample_Interction.LifeKind near;

    private void Start() {
        lifes.Add(Sample_Interction.LifeKind.Flower, new Sample_Farming());   // 0
        lifes.Add(Sample_Interction.LifeKind.Sea, new Sample_Fishing());   // 1
    }

    private void Update() {
        // Ű�Է�
        if (Input.GetKeyDown(KeyCode.A)){
            // ��ó�� ������Ʈ�� �Ǻ�
            if (near != Sample_Interction.LifeKind.Null){
                lifes[near].Motion();
                
                lifes[near].Result();
            }
        }
    }

    // ������Ʈ�� ���������� üũ�ϴ� ����� ����ؾ��ϰ�
    // �Ͼ ���� üũ�ϴ°� �߿��Ѱ� �ƴ϶� ����� ��ġ�� �����ϴ� ������Ʈ�� ��ü ��ü�� üũ�ؾ���
    // �׷��� �ش� ��ü�� ��ǿ� ������ �� �ְ���?
    private void OnTriggerEnter(Collider other) {
        near = other.GetComponent<Sample_Interction>().lifeKind;
    }

    private void OnTriggerExit(Collider other) {
        near = Sample_Interction.LifeKind.Null;
    }
}