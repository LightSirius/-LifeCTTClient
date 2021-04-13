using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 생활력 피로도 스테미너 스크립트 
public class PlayerLifeForce : MonoBehaviour
{
    [SerializeField]
    private float fatigue = 100f;
    public float decrease_fatigue = 25f;
    private float recoveryFatigueTime = 0;

    
    // 피로도 감소 함수
    public void DecreaseFatigue()
    {
        if(fatigue > 0)
            fatigue -= decrease_fatigue;
    }

    // 지속적으로 확인해줄 피로도 함수
    public void Fatigue()
    {
        if(fatigue <= 0)
            fatigue = 0;
        if(fatigue >= 100)
            fatigue = 100;

        if(fatigue < 100)
            RecoveryFatigue();
    }


    // 피로도 회복 함수
    public void RecoveryFatigue()
    {
        if(fatigue >= 100)
        {
            recoveryFatigueTime = 0;
            return;
        }

        recoveryFatigueTime += Time.fixedDeltaTime;
        if(recoveryFatigueTime > 5) // 5초(임시) 마다 피로도 회복
        {
            fatigue += 25f;
            recoveryFatigueTime = 0;
        }
    }
}
