using System;
using UnityEngine;
using LifeContent;

public interface IInteraction{
    LifeType lifeType {get;}
    Enum Type{get;}
    Vector3 Position{ get; }
    float DurationTime{get;}
    bool IsEnable{ get; set; }
    void Send();        // ChunkManager에게 데이터 값을 전송하는 함수
}