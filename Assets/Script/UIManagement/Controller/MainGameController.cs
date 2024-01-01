
using GirdSystem;
using Manager;
using Manager.States;
using Map;
using Player;
using UIManagement.Element;
using UIManagement.Views;
using Unity.VisualScripting;
using UnityEngine;

namespace UIManagement.Controller
{
    
    public class MainGameController : UIController
    {
        private MainGameView _view;
        private PlayerController _playerController;
        private MainGameRoundState _cacehRoundState;
        private MapManager _mapManager;
        private int _currentPlayerSkillIndex = -1;
        private int _clickDrawCardCount = 0;
        private int _cacheDashCount = 0;
        public override void Initialize(UIElement uIElement)
        {
            base.Initialize(uIElement);
            _view = GetComponent<MainGameView>();
            _playerController = FindObjectOfType<PlayerController>(true);
            _mapManager = FindObjectOfType<MapManager>(true);
        }
        
        public override void Show()
        {
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnHungryChange += InitializeHungryBar;
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnConquerRateChange += _view.UpdateConquerBar;
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnRoundChange += OnRoundChange;
            _view.BindCardButtonEvent(OnPlayerAttack, PlayerActionType.Attack);
            _view.BindCardButtonEvent(OnPlayerMove, PlayerActionType.Move);
            _view.BindDrawCardButtonEvent(OnClickDrawCardButton);
            _view.BindCloseDrawCardPanelTimerEvent(OnCloseDrawCardPanel);
            _view.BindDragonHeadButtonEvent(OnUseSkill);
            _playerController.OnActionEnd += OnPlayerActionEnd;
            _playerController.OnPlayerDash += OnPlayerDash;
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.GridSkillSystem.OnSkillUsed += OnPlayerActionEnd;
            MainGameRule.OnRoundStateChange += OnRoundStateChange;
            _mapManager.ResetMap();
            _playerController.PlayerCharacter.IsUnstoppable = false;
        }
        public override void Hide()
        {
            
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnHungryChange -= InitializeHungryBar;
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnConquerRateChange -= _view.UpdateConquerBar;
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.OnRoundChange -= OnRoundChange;

            _view.UnBindAllCardButtonEvent(PlayerActionType.Attack);
            _view.UnBindAllCardButtonEvent(PlayerActionType.Move);
            _view.UnBindAllDrawCardButtonEvent();
            _view.UnBindCloseDrawCardPanelTimerEvent(OnCloseDrawCardPanel);
            _view.UnBindAllDragonHeadButtonEvent();
            _playerController.OnActionEnd -= OnPlayerActionEnd;
            _playerController.OnPlayerDash -= OnPlayerDash;
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.GridSkillSystem.OnSkillUsed -= OnPlayerActionEnd;
            MainGameRule.OnRoundStateChange -= OnRoundStateChange;
        }
        private void InitializeHungryBar(float currentHungry, float maxHungry)
        {
            _view.UpdateHungryBar(currentHungry, maxHungry);
            if (GameManager.Instance.CurrentState is MainGameState mainGameState)
            {
                _view.SetRange(mainGameState.MainGameRule.HigherHungryRate, mainGameState.MainGameRule.LowerHungryRate);
            }

        }
        
        private void OnPlayerAttack()
        {
            //很他媽神奇，下面一定要這樣放，因為unity的uievent系統不知道為啥
            //，如果你在中途把interacterable關掉，他就會暫停你綁定的function，直到下次解綁在開始執行。
            _view.SetAllCardButtonInteractable(false);
            _playerController.Attack();
        }
        
        
        private void OnPlayerActionEnd()
        {
            print("OnPlayerActionEnd");
            _view.SetDirArrowActive(false);
            MainGameState mainGameState = GameManager.Instance.CurrentState as MainGameState;
            
            mainGameState?.MainGameRule.SwitchToNextRound(mainGameState.MainGameRule.CurrentRoundState);
        }

        private void OnPlayerMove()
        {
            _playerController.StartMove();
            _view.SetDirArrowActive(true);
            _view.SetAllCardButtonInteractable(false);
        }
        
        private void OnPlayerRoundStart()
        {
            OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.ModifyHungry();
            _playerController.PlayerCharacter.Move(_playerController.PlayerCharacter.GridPosition);
            if (_playerController.PlayerCharacter.IsUnstoppable)
            {
                _view.SetAllCardButtonInteractable(false);
                _cacheDashCount--;
                if (_cacheDashCount <= 0)
                {
                    _playerController.PlayerCharacter.EndDashSkill();
                }
                MainGameState mainGameState = GameManager.Instance.CurrentState as MainGameState;
                mainGameState?.MainGameRule.SwitchToNextRound(mainGameState.MainGameRule.CurrentRoundState);             
                return;
            }
            _view.SetAllCardButtonInteractable(true);
            
        }
        
        private void OnRoundChange(int round)
        {
            MainGameState mainGameState = GameManager.Instance.CurrentState as MainGameState;
            if (mainGameState == null) return;

            _mapManager.UpdateMapPosition((float)round / mainGameState.MainGameRule.EndRound);
        }
        
        
        
        private void OnClickDrawCardButton()
        {
            _clickDrawCardCount++;
            if (_clickDrawCardCount >= Random.Range(3,8))
            {
                _currentPlayerSkillIndex = 
                    Random.Range(0, _playerController.PlayerStates.GridSkillSystem.PlayerSkills.Length);
                _view.SpawnDrawCardItem(_playerController.PlayerStates
                    .GridSkillSystem.PlayerSkills[_currentPlayerSkillIndex]);
                _clickDrawCardCount = 0;
            }
        }
        
        private void OnPlayerDrawCard()
        {
            _view.SetDrawCardPanelActive(true);
        }
        
        private void OnRoundStateChange(MainGameRoundState newStates, MainGameRoundState lastStates)
        {
            switch (newStates)
            {
                case MainGameRoundState.EndTurn:
                    OnPlayerRoundStart();
                    break;
                case MainGameRoundState.DrawCard:
                    OnPlayerDrawCard();
                    _cacehRoundState = lastStates;
                    break;
                case MainGameRoundState.EnemyTurn:
                    OwnerUIElement.UIManager.OwnerPlayer.PlayerStates.ModifyRound();
                    break;
            }
        }
        
        
        private void OnCloseDrawCardPanel()
        {
            print(_cacehRoundState);
            MainGameState mainGameState = (MainGameState)GameManager.Instance.CurrentState;
            mainGameState.MainGameRule.SwitchToNextRound(_cacehRoundState);
            _view.SpawnDragonHeadItem(_playerController.PlayerStates
                .GridSkillSystem.PlayerSkills[_currentPlayerSkillIndex]);
            _view.SetGragonHeadButtonActive(true);
        }
        
        
        private void OnUseSkill()
        {
            _playerController.PlayerStates.GridSkillSystem.UseSkill(_currentPlayerSkillIndex);
            _view.SetGragonHeadButtonActive(false);
            _view.SetAllCardButtonInteractable(false);
            _view.DestroyGragonBollObjTemp();
        }

        private void OnPlayerDash(int distance)
        {
            _cacheDashCount = distance - 1;
            MainGameState mainGameState = (MainGameState)GameManager.Instance.CurrentState;
            mainGameState.MainGameRule.SwitchToNextRound(mainGameState.MainGameRule.CurrentRoundState);
        }


    }
}