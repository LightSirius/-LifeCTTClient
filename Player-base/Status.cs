using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status 
{

    // ���� ����
    public int current_Level = 1;
    // �ִ뷹��
    public int maxLevel;
    //���� ����ġ
    public int currentExp = 0;
    // �ִ� ����ġ
    List<int> maxExp = new List<int>();
    //�������� �ʿ��� ����ġ
    public int levelUpExp = 100;    

    // ����ġ ȹ�淮
    public int lifeExp = 40;

    private void Start()
    {        
        maxExp.Add(levelUpExp);
    }
    public void LevelUp()
    {
        current_Level++;
        maxExp.Add(levelUpExp+50);
    }
    public void GetExp()
    {
        currentExp += lifeExp;

        // ������ ����ġ �޼�
        if(currentExp >= maxExp[current_Level]) 
        {            
            currentExp -= maxExp[current_Level]; // �ʰ��� ����ġ�� �����ְ� ���� ��
            LevelUp();            
        }
    }
}
