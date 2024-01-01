
using Manager;
using UIManagement.Views;

namespace UIManagement.Controller
{
    public class EndController : UIController
    {
        private EndView _endView;
        
        public override void Initialize(UIElement uIElement)
        {
            base.Initialize(uIElement);
            _endView = GetComponent<EndView>();
        }
        
        public override void Show()
        {
            _endView.BindRestartButton(RestartGame);
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnStateChange += _endView.SetEndImage;
        }

        public override void Hide()
        {
            _endView.UnBindAllRestartButton();
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnStateChange -= _endView.SetEndImage;
        }
        
        private void RestartGame()
        {
            GameManager.Instance.SwitchState(GameStateType.MainGame);
        }
        
    }
}