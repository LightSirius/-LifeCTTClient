using UnityEngine;
using LifeContent;

public class FishingAreaObject : InteractionObject<FishingType> {

    protected override void Start() {
        base.Start();
        _lifeType = LifeType.Fishing;
    }

    public override void Send(){
        
    }
}