public interface IState
{
    void OperateEnter(); // 상태가 바뀌자마자 하고 싶은 행동
    void OperateUpdate(); // 매 프레임마다 하고 싶은 행동
    void OperateExit(); // 다른 상태로 바뀌기 직전에 하고 싶은 행동
}