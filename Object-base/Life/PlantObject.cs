using UnityEngine;
using LifeContent;

public class PlantObject : InteractionObject<FarmingType> {
    protected override void Start() {
        base.Start();
        _lifeType = LifeType.Farming;
    }
    
    public override void Send(){
        
    }
}