using UnityEngine;
using LifeContent;
using System;

[RequireComponent(typeof(SphereCollider))]
public abstract class InteractionObject<T> : MonoBehaviour, IInteraction
{
    public Climate suitableClimate;     // 적합 기후
    public Enum Type => type as Enum;   // type 프로퍼티
    [SerializeField]
    protected T type;

    public int seed;                    // 시드 값
    public int index;                   // 오브젝트 식별값
    public int needProficiency;         // 필요 숙련도 (레벨)

    public float DurationTime => durationTime;      // duration Time 프로퍼티

    public bool IsEnable { get => isEnable; set => isEnable = value; }

    public Vector3 Position => this.transform.position;     // 오브젝트 위치

    protected bool isEnable;

    [SerializeField]
    protected float durationTime;          // 캐는데 걸리는 시간(소요시간)
    public float growthTime;            // 성장시간(오브젝트가 재생성하는데 걸리는 시간)

    // 오브젝트가 다 캐진지 검사가 필요해보임

    protected Collider myCollider;

    protected virtual void Start() {
        // 시드 값 지정 코드 작성
        seed = UnityEngine.Random.Range(-100000, 100000);
        
        myCollider = GetComponent<Collider>();
        myCollider.isTrigger = true;
    }

    public abstract void Send();
}