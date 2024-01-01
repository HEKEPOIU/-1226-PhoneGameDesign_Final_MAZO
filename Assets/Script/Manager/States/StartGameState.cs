using UnityEngine;

namespace Manager.States
{
    public class StartGameState : GameStateBase
    {
        public StartGameState(GameManager manager) : base(manager)
        {
            StateType = GameStateType.Start;
        }

        public override void OnStateEnter()
        {
            Debug.Log("StartGameState");
            OnStateStart?.Invoke(this);
        }

        public override void OnStateExit()
        {
        }

        public override void OnStateUpdate()
        {
        }
    }
}