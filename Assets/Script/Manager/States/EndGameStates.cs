namespace Manager.States
{
    public abstract class EndGameStates : GameStateBase
    {
        protected EndGameStates(GameManager manager) : base(manager)
        {
        }

        public override void OnStateEnter()
        {
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