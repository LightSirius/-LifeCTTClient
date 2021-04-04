using UnityEngine;

public class FlowerTreeState : IState
{
    public void OperateEnter()
    {
        Debug.Log("나무에 올라가서 꽃을 캐기 시작한다!!!!!");
    }

    public void OperateExit()
    {
        Debug.Log("나무에 올라가서 꽃을 다 캤다!!!!!!!!!!");
    }

    public void OperateUpdate()
    {
        Debug.Log("나무에 올라가서 꽃을 캐는 중이다!!");
    }
}