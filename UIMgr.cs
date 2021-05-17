using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : Singleton<UIMgr>
{
    public Image LifeProgressBar;
    public Text LifeInfoText;

    private bool enableBar;

    private Coroutine coroutine;

    private void Awake() {
        enableBar = false;
        InitUI(); 
    }

    public void SetSkillUI(float time, string text)
    {
        coroutine = StartCoroutine(SetSkillProgressBar(time, text));
    }

    // 생활스킬을 쓸 때 ProgressBar 진행 (time : 소요시간, text : 채집중인 텍스트)
    public IEnumerator SetSkillProgressBar(float time, string text){
        enableBar = true;
        LifeProgressBar.gameObject.SetActive(true);
        LifeProgressBar.fillAmount = 1f;
        LifeInfoText.text = text;
        while(LifeProgressBar.fillAmount != 0)
        {
            float amount = 1f / time * Time.deltaTime;
            LifeProgressBar.fillAmount -= amount;
            yield return new WaitForSeconds(amount);
        }
        enableBar = false;
        InitUI();
    }

    // UI 초기화 함수
    public void InitUI(){
        Debug.Log("UI 초기화");
        if(enableBar == true)           // 코루틴이 실행중일 때 초기화한다면
        {
            StopCoroutine(coroutine);       // 코루틴 중단
            enableBar = false;
        }
        LifeInfoText.text = " ";
        LifeProgressBar.gameObject.SetActive(false);
    }

}
