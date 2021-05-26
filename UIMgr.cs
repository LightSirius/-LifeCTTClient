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

    // ��Ȱ��ų�� �� �� ProgressBar ���� (time : �ҿ�ð�, text : ä������ �ؽ�Ʈ)
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

    // UI �ʱ�ȭ �Լ�
    public void InitUI(){
        Debug.Log("UI �ʱ�ȭ");
        if(enableBar == true)           // �ڷ�ƾ�� �������� �� �ʱ�ȭ�Ѵٸ�
        {
            StopCoroutine(coroutine);       // �ڷ�ƾ �ߴ�
            enableBar = false;
        }
        LifeInfoText.text = " ";
        LifeProgressBar.gameObject.SetActive(false);
    }

}
