namespace Manager.States
{
    public class FailGameState : EndGameStates
    {
        public FailGameState(GameManager manager) : base(manager)
        {
            StateType = GameStateType.Fail;
        }
    }
}