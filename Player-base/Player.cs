using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public bool isFarming = false;      // 생활(채집, 낚시 등)을 하고있을 경우 true 안하고 있을 경우 false

    private Transform myTransform;
    private InteractionObject nearObject;

    private void Start() {
        myTransform = transform;
    }

    private void OnDestroy() {
        myTransform = null;
    }

    private void Update() {
        // 키입력
        if (Input.GetKeyDown(KeyCode.G)){
            // 만약에 멀리있으면 다가가고
            // 가까이 있으면 바로 채집
            if (nearObject){
                StartCoroutine(PlayerInteraction());
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        // 근처에 있는 오브젝트 판별
        nearObject = other.GetComponent<InteractionObject>();    
    }

    private void OnTriggerExit(Collider other) {
        // 근처에 있는 오브젝트 해제
        nearObject = null;
    }

    // 코루틴으로 루틴 생성
    // 멀리있으면 -> 가까이 가는 것
    // 오브젝트 캐는 이벤트 실행
    IEnumerator PlayerInteraction(){
        // 플레이어가 chunk매니저에게 허락을 받아야함.
        isFarming = true;              // 나중에 chunkManager에 허락을 받는 코드로 바꾸기

        // 1. 근처오브젝트로 다가감
        yield return StartCoroutine(MoveToNearObject());
        // 2. 캐는 애니메이션 실행 및 UI 켜기
        yield return StartCoroutine(WaitFarmingTime(nearObject.durationTime));
        // 3. 오브젝트 정보 전송
        nearObject.Send();

        isFarming = false;
    }

    IEnumerator MoveToNearObject(){
        float distance = Vector3.Distance(myTransform.position, nearObject.transform.position);
        while(distance > 0.25f){        // 임시로 지정한 거리(0.25f) 근처까지 도달했을 때 실행
            // 임시 이동 코드
            Vector3 direction = nearObject.transform.position - myTransform.position;
            myTransform.position += direction.normalized * 3f * Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator WaitFarmingTime(float durationTime){
        // 캐릭터 애니메이션을 실행하는 코드 작성 필요
        Debug.Log("무언가를 하는 중이다...");

        yield return new WaitForSeconds(durationTime);
    }
}