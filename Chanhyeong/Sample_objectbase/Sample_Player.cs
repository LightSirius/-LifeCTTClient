using System.Collections.Generic;
using UnityEngine;

public class Sample_Player : MonoBehaviour {


    // 리스트를 Dictionary 
    Dictionary<Sample_Interction.LifeKind, Sample_ILife> lifes = new Dictionary<Sample_Interction.LifeKind, Sample_ILife>();

    public Sample_Interction.LifeKind near;

    private void Start() {
        lifes.Add(Sample_Interction.LifeKind.Flower, new Sample_Farming());   // 0
        lifes.Add(Sample_Interction.LifeKind.Sea, new Sample_Fishing());   // 1
    }

    private void Update() {
        // 키입력
        if (Input.GetKeyDown(KeyCode.A)){
            // 근처에 오브젝트를 판별
            if (near != Sample_Interction.LifeKind.Null){
                lifes[near].Motion();
                
                lifes[near].Result();
            }
        }
    }

    // 오브젝트가 겹쳐있을때 체크하는 방식을 고민해야하고
    // 니어가 뭔지 체크하는게 중요한게 아니라 가까운 위치에 존재하는 오브젝트의 개체 자체를 체크해야함
    // 그래야 해당 개체의 펑션에 접근할 수 있겠지?
    private void OnTriggerEnter(Collider other) {
        near = other.GetComponent<Sample_Interction>().lifeKind;
    }

    private void OnTriggerExit(Collider other) {
        near = Sample_Interction.LifeKind.Null;
    }
}