using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    // ���� ���¸� ��� ������Ƽ.
    public IState CurrentState { get; private set; }

    // �⺻ ���¸� �����ÿ� �����ϰ� ������ �����.
    public StateMachine(IState defaultState)
    {
        CurrentState = defaultState;
    }

    public void SetState(IState state) 
    {
        if(CurrentState == state)
        {
            Debug.Log("���� �̹� �� �����Դϴ�. ^_^ ");
            return;
        }
        
        //���°� �ٲ�� ����, ���� ������ Exit�� ȣ���Ѵ�.
        CurrentState.OperateExit();

        // ���±�ü
        CurrentState = state;

        // �� ������ Enter�� ȣ���Ѵ�.
        CurrentState.OperateEnter();
    }
    

    // �� �����Ӹ��� ȣ��Ǵ� �Լ�.
    public void DoOperateUpdate()
    {
        CurrentState.OperateUpdate();
    }
}
