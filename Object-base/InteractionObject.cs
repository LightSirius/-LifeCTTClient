using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class InteractionObject : MonoBehaviour, IInteraction
{
    public LifeType lifeType;
    public Climate suitableClimate;     // ���� ����

    public int seed;                    // �õ� ��
    public int index;                   // ������Ʈ �ĺ���
    public int needProficiency;         // �ʿ� ���õ� (����)
    public float durationTime;          // ĳ�µ� �ɸ��� �ð�(�ҿ�ð�)
    public float growthTime;            // ����ð�(������Ʈ�� ������ϴµ� �ɸ��� �ð�)

    protected Collider myCollider;

    protected virtual void Start() {
        // �õ� �� ���� �ڵ� �ۼ�
        seed = Random.Range(-100000, 100000);
        
        myCollider = GetComponent<Collider>();
        myCollider.isTrigger = true;
    }

    public abstract void Send();
}