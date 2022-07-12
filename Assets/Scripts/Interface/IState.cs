namespace HOGUS.Scripts.Interface
{
    public interface IState
    {
        /// <summary>
        /// 상태 진입 시 로직 수행
        /// ex) 행동 시작 시 애니메이션 수행
        /// </summary>
        void StateEnter();
        /// <summary>
        /// 상태 벗어날 시 로직 수행
        /// ex) 행동 종료 시 애니메이션 수행
        /// </summary>
        void StateUpdate();
        /// <summary>
        /// 상태 도중 매 프레임 호출될 로직 수행
        /// </summary>
        void StateExit();
    }
}