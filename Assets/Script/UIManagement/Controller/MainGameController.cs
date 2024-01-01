
using Manager;
using Manager.States;
using Player;
using UIManagement.Element;
using UIManagement.Views;

namespace UIManagement.Controller
{
    
    public class MainGameController : UIController
    {
        private MainGameView _view;
        private PlayerController _playerController;
        public override void Initialize(UIElement uIElement)
        {
            base.Initialize(uIElement);
            _view = GetComponent<MainGameView>();
            _playerController = FindObjectOfType<PlayerController>(true);
        }
        
        public override void Show()
        {
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnHungryChange += InitializeHungryBar;
            _view.BindCardButtonEvent(OnPlayerAttack, PlayerActionType.Attack);
            _view.BindCardButtonEvent(OnPlayerMove, PlayerActionType.Move);
            _playerController.OnMoveEnd += OnPlayerActionEnd;
            _playerController.OnAttackEnd += OnPlayerActionEnd;
        }
        public override void Hide()
        {
            
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnHungryChange -= InitializeHungryBar;
            _view.UnBindAllCardButtonEvent(PlayerActionType.Attack);
            _view.UnBindAllCardButtonEvent(PlayerActionType.Move);
            _playerController.OnMoveEnd -= OnPlayerActionEnd;
            _playerController.OnAttackEnd -= OnPlayerActionEnd;
        }
        
        private void InitializeHungryBar(float currentHungry, float maxHungry)
        {
            _view.UpdateHungryBar(currentHungry, maxHungry);
            if (GameManager.Instance.CurrentState is MainGameState mainGameState)
            {
                _view.SetRange(mainGameState.GameRule.HigherHungryRate, mainGameState.GameRule.LowerHungryRate);
            }

        }
        
        private void OnPlayerAttack()
        {
            _playerController.Attack();
            _view.SetAllCardButtonInteractable(false);
        }
        
        
        private void OnPlayerActionEnd()
        {
            _view.SetDirArrowActive(false);
            _view.SetAllCardButtonInteractable(true);
        }

        private void OnPlayerMove()
        {
            _playerController.StartMove();
            _view.SetDirArrowActive(true);
            _view.SetAllCardButtonInteractable(false);
        }
        
    }
}