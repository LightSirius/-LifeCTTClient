using UnityEngine;

public class FlowerTreeState : IState
{
    public void OperateEnter()
    {
        Debug.Log("������ �ö󰡼� ���� ĳ�� �����Ѵ�!!!!!");
    }

    public void OperateExit()
    {
        Debug.Log("������ �ö󰡼� ���� �� ĺ��!!!!!!!!!!");
    }

    public void OperateUpdate()
    {
        Debug.Log("������ �ö󰡼� ���� ĳ�� ���̴�!!");
    }
}