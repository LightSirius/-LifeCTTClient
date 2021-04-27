using UnityEngine;
using LifeContent;

public class TreeObject : InteractionObject<WoodcuttingType>
{
    // 꽃이나 열매가 맺히는 시간
    public float respawnTime;

    protected override void Start() {
        base.Start();
        _lifeType = LifeType.Woodcutting;
    }

    public override void Send(){
        
    }
}