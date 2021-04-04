using UnityEngine;
using System.Collections;

public class TreeState : IState
{
    public void OperateEnter()
    {
        UIMgr.Instance.LifeProgressBar.gameObject.SetActive(true);
        Debug.Log("������ ĳ�� �����մϴ�.");
    }

    public void OperateExit()
    {
        UIMgr.Instance.LifeProgressBar.gameObject.SetActive(false);
        UIMgr.Instance.LifeInfoText.text = " ";
        Debug.Log("���� ĳ�⸦ �����մϴ�.");
    }

    public void OperateUpdate()
    {
        UIMgr.Instance.LifeInfoText.text = "������ ĳ�� ���Դϴ�..";
        UIMgr.Instance.LifeProgressBar.fillAmount =  UIMgr.Instance.LifeProgressBar.fillAmount - 0.002f; 
    }
}