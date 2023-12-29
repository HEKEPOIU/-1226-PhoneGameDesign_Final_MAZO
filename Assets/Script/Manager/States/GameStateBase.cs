namespace Manager.States
{
    public abstract class GameStateBase
    {
        protected GameManager Manager;

        protected GameStateBase(GameManager manager)
        {
            Manager = manager;
        }
        public abstract void OnStateEnter();
        public abstract void OnStateExit();
        public abstract void OnStateUpdate();
    }
}