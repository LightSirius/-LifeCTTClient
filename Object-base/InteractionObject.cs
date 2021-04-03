using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class InteractionObject : MonoBehaviour, IInteraction
{
    public LifeType lifeType;
    public Climate suitableClimate;     // 적합 기후

    public int seed;                    // 시드 값
    public int index;                   // 오브젝트 식별값
    public int needProficiency;         // 필요 숙련도 (레벨)
    public float durationTime;          // 캐는데 걸리는 시간(소요시간)
    public float growthTime;            // 성장시간(오브젝트가 재생성하는데 걸리는 시간)

    protected Collider myCollider;

    protected virtual void Start() {
        // 시드 값 지정 코드 작성
        seed = Random.Range(-100000, 100000);
        
        myCollider = GetComponent<Collider>();
        myCollider.isTrigger = true;
    }

    public abstract void Send();
}