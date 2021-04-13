using System;
using UnityEngine;

public interface IInteraction{
    Vector3 Position{ get; }
    Enum Type{get;}
    float DurationTime{get;}
    bool IsEnable{ get; set; }
    void Send();        // ChunkManager���� ������ ���� �����ϴ� �Լ�
}