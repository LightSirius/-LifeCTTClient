using UnityEngine;
using LifeContent;

public class MineralObject : InteractionObject<MiningType> {
    protected override void Start() {
        base.Start();
        _lifeType = LifeType.Mining;
    }

    public override void Send(){
        
    }
}