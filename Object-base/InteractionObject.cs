using UnityEngine;
using LifeContent;
using System;

[RequireComponent(typeof(SphereCollider))]
public abstract class InteractionObject<T> : MonoBehaviour, IInteraction
{
    public Climate suitableClimate;     // ���� ����
    public Enum Type => type as Enum;   // type ������Ƽ
    [SerializeField]
    protected T type;

    public int seed;                    // �õ� ��
    public int index;                   // ������Ʈ �ĺ���
    public int needProficiency;         // �ʿ� ���õ� (����)

    public float DurationTime => durationTime;      // duration Time ������Ƽ

    public bool IsEnable { get => isEnable; set => isEnable = value; }

    public Vector3 Position => this.transform.position;     // ������Ʈ ��ġ

    protected bool isEnable;

    [SerializeField]
    protected float durationTime;          // ĳ�µ� �ɸ��� �ð�(�ҿ�ð�)
    public float growthTime;            // ����ð�(������Ʈ�� ������ϴµ� �ɸ��� �ð�)

    // ������Ʈ�� �� ĳ���� �˻簡 �ʿ��غ���

    protected Collider myCollider;

    protected virtual void Start() {
        // �õ� �� ���� �ڵ� �ۼ�
        seed = UnityEngine.Random.Range(-100000, 100000);
        
        myCollider = GetComponent<Collider>();
        myCollider.isTrigger = true;
    }

    public abstract void Send();
}