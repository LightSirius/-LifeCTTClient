using UnityEngine;
using LifeContent;

public class LivestockObject : InteractionObject<LivestockType> {
    protected override void Start() {
        base.Start();
        _lifeType = LifeType.Livestock;
    }
    public override void Send(){
        
    }
}