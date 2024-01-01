using Manager;
using UIManagement.Element;

namespace UIManagement.Controller
{
    public class StartController : UIController
    {
        private Views.StartView _startView;

        public override void Initialize(UIElement uIElement)
        {
            base.Initialize(uIElement);
            _startView = GetComponent<Views.StartView>();
        }

        public override void Show()
        {
            _startView.BindStartButton(OnStartButtonClicked);
        }
        public override void Hide()
        {
            if (_startView == null) return;
            _startView.UnbindAllStartButton();
        }

        private void OnStartButtonClicked()
        {
            GameManager.Instance.SwitchState(GameStateType.MainGame);
        }
    }
}