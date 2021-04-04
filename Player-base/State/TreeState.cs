using UnityEngine;

public class TreeState : IState
{
    public void OperateEnter()
    {
        Debug.Log("나무를 벤다!!!!");
    }

    public void OperateExit()
    {
        throw new System.NotImplementedException();
    }

    public void OperateUpdate()
    {
        throw new System.NotImplementedException();
    }
}