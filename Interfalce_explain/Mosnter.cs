using UnityEngine;

public class Mosnter : MonsterOrigin {
    public override void Attack(){
        Debug.Log("고정형 몬스터가 공격합니다!!");
    }

    public void Defence(){
        Debug.Log("막는다!!");
    }
}