using System;
using UnityEngine;

public interface IInteraction{
    Vector3 Position{ get; }
    Enum Type{get;}
    float DurationTime{get;}
    bool IsEnable{ get; set; }
    void Send();        // ChunkManager에게 데이터 값을 전송하는 함수
}