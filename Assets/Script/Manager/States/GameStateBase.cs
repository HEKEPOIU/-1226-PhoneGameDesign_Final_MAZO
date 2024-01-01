using System;

namespace Manager.States
{
    public abstract class GameStateBase
    {
        protected GameManager Manager;
        public GameStateType StateType { get; protected set; }
        public static Action<GameStateBase> OnStateStart; 

        protected GameStateBase(GameManager manager)
        {
            Manager = manager;
        }
        public abstract void OnStateEnter();
        public abstract void OnStateExit();
        public abstract void OnStateUpdate();
    }
}