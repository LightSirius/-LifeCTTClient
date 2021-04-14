using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��Ȱ�� �Ƿε� ���׹̳� ��ũ��Ʈ 
public class PlayerLifeForce : MonoBehaviour
{
    [SerializeField]
    private float fatigue = 100f;
    public float decrease_fatigue = 25f;
    private float recoveryFatigueTime = 0;

    
    // �Ƿε� ���� �Լ�
    public void DecreaseFatigue()
    {
        if(fatigue > 0)
            fatigue -= decrease_fatigue;
    }

    // ���������� Ȯ������ �Ƿε� �Լ�
    public void Fatigue()
    {
        if(fatigue <= 0)
            fatigue = 0;
        if(fatigue >= 100)
            fatigue = 100;

        if(fatigue < 100)
            RecoveryFatigue();
    }


    // �Ƿε� ȸ�� �Լ�
    public void RecoveryFatigue()
    {
        if(fatigue >= 100)
        {
            recoveryFatigueTime = 0;
            return;
        }

        recoveryFatigueTime += Time.fixedDeltaTime;
        if(recoveryFatigueTime > 5) // 5��(�ӽ�) ���� �Ƿε� ȸ��
        {
            fatigue += 25f;
            recoveryFatigueTime = 0;
        }
    }
}
