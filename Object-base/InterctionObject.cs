using UnityEngine;

public abstract class InterctionObject : MonoBehaviour, IInteraction
{
    public LifeType lifeType;
    public Climate suitableClimate;     // ���� ����

    public int seed;
    public int index;                   // ������Ʈ �ĺ���
    public int needProficiency;         // �ʿ� ���õ� (����)
    public float durationTime;          // ĳ�µ� �ɸ��� �ð�(�ҿ�ð�)
    public float growthTime;            // ����ð�(������Ʈ�� ������ϴµ� �ɸ��� �ð�)

    private void Start() {
        // �õ� �� ���� �ڵ� �ۼ�
        seed = Random.seed;
    }

    public abstract void Send();
}