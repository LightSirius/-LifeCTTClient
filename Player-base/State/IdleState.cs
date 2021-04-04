using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OperateEnter()
    {
        
    }

    public void OperateExit()
    {

    }

    public void OperateUpdate()
    {
        Debug.Log("아이들아이들");
    }
}
