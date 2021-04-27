using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeContent;

public class PlayerStatus 
{
    public Dictionary<LifeType,Status> status = new Dictionary<LifeType, Status>();

    PlayerStatus(){
        status.Add(LifeType.Farming, new Status());
        status.Add(LifeType.Fishing, new Status());
        status.Add(LifeType.Mining, new Status());
        status.Add(LifeType.Woodcutting, new Status());
        status.Add(LifeType.Livestock, new Status());
    }
}
