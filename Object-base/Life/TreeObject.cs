using UnityEngine;
using LifeContent;

public class TreeObject : InteractionObject<WoodcuttingType>
{
    // ���̳� ���Ű� ������ �ð�
    public float respawnTime;

    protected override void Start() {
        base.Start();
        _lifeType = LifeType.Woodcutting;
    }

    public override void Send(){
        
    }
}