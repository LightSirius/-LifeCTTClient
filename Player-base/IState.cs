public interface IState
{
    void OperateEnter(); // ���°� �ٲ��ڸ��� �ϰ� ���� �ൿ
    void OperateUpdate(); // �� �����Ӹ��� �ϰ� ���� �ൿ
    void OperateExit(); // �ٸ� ���·� �ٲ�� ������ �ϰ� ���� �ൿ
}