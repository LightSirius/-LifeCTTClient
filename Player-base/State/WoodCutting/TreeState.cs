using UnityEngine;
using System.Collections;

public class TreeState : IState
{
    public void OperateEnter()
    {
        UIMgr.Instance.LifeProgressBar.gameObject.SetActive(true);
        Debug.Log("나무를 캐기 시작합니다.");
    }

    public void OperateExit()
    {
        UIMgr.Instance.LifeProgressBar.gameObject.SetActive(false);
        UIMgr.Instance.LifeInfoText.text = " ";
        Debug.Log("나무 캐기를 종료합니다.");
    }

    public void OperateUpdate()
    {
        UIMgr.Instance.LifeInfoText.text = "나무를 캐는 중입니다..";
        UIMgr.Instance.LifeProgressBar.fillAmount =  UIMgr.Instance.LifeProgressBar.fillAmount - 0.002f; 
    }
}