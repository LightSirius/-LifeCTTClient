using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status 
{

    // 현재 레벨
    public int current_Level = 1;
    // 최대레벨
    public int maxLevel;
    //현재 경험치
    public int currentExp = 0;
    // 최대 경험치
    List<int> maxExp = new List<int>();
    //레벨업에 필요한 경험치
    public int levelUpExp = 100;    

    // 경험치 획득량
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

        // 레벨업 경험치 달성
        if(currentExp >= maxExp[current_Level]) 
        {            
            currentExp -= maxExp[current_Level]; // 초과한 경험치만 남겨주고 레벨 업
            LevelUp();            
        }
    }
}
