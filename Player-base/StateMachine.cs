using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    // 현재 상태를 담는 프로퍼티.
    public IState CurrentState { get; private set; }

    // 기본 상태를 생성시에 설정하게 생성자 만들기.
    public StateMachine(IState defaultState)
    {
        CurrentState = defaultState;
    }

    public void SetState(IState state) 
    {
        if(CurrentState == state)
        {
            Debug.Log("현재 이미 그 상태입니다. ^_^ ");
            return;
        }
        
        //상태가 바뀌기 전에, 이전 상태의 Exit를 호출한다.
        CurrentState.OperateExit();

        // 상태교체
        CurrentState = state;

        // 새 상태의 Enter를 호출한다.
        CurrentState.OperateEnter();
    }
    

    // 매 프레임마다 호출되는 함수.
    public void DoOperateUpdate()
    {
        CurrentState.OperateUpdate();
    }
}
