using System;
using System.Collections.Generic;
using Manager;
using Manager.States;
using UnityEngine;

namespace Character.Spawner
{
    public class GridSpawnerManager : MonoBehaviour
    {
        [SerializeField] private GridEnemySpawnerSystem _enemySpawnerSystem;
        [SerializeField] private GridItemSpawnerSystem _itemSpawnerSystem;
        private List<BaseGridCharacter> _gridCharacters = new List<BaseGridCharacter>();
        private MainGameRule _mainGameRule;

        private void Awake()
        {
            GameStateBase.OnStateStart += SwitchGameState;
            MainGameRule.OnRoundStateChange += OnRoundChange;
        }

        private void Start()
        {
            _enemySpawnerSystem.OnPlayerKillEnemy += GameManager.Instance.Player.PlayerStates.ModifyConquerRate;
            _itemSpawnerSystem.OnItemPickedUp += ItemBePickUp;

        }

        private void OnDestroy()
        {
            GameStateBase.OnStateStart -= SwitchGameState;
            MainGameRule.OnRoundStateChange -= OnRoundChange;
            _enemySpawnerSystem.OnPlayerKillEnemy -= GameManager.Instance.Player.PlayerStates.ModifyConquerRate;
            _itemSpawnerSystem.OnItemPickedUp -= ItemBePickUp;

        }

        private void OnRoundChange(MainGameRoundState newRoundState, MainGameRoundState lastRoundState)
        {
            switch (newRoundState)
            {

                case MainGameRoundState.EnemyTurn:
                    OnEnemyRound();
                    break;
                case MainGameRoundState.End:
                    OnEndRound();
                    break;
                case MainGameRoundState.EndTurn:
                case MainGameRoundState.DrawCard:
                default:
                    break;
            }
        }


        private void SwitchGameState(GameStateBase gameState)
        {
            if (gameState.StateType == GameStateType.MainGame)
            {
                MainGameState mainGameState = gameState as MainGameState;
                ResetSystem();
                StartRandomSpawnOnGrid();
                _mainGameRule = mainGameState.MainGameRule;
            }
        }


        private void ResetSystem()
        {
            _enemySpawnerSystem.ResetCharacter();
            _itemSpawnerSystem.ResetCharacter();
        }

        private void StartRandomSpawnOnGrid()
        {
            _enemySpawnerSystem.RandomSpawnOnGrid();
            _itemSpawnerSystem.RandomSpawnOnGrid();
        }
        
        public void RandomSpawnEnemyOnGrid(int count)
        {
            _enemySpawnerSystem.RandomSpawnOnGrid(count);
        }
        
        
        
        private void OnEndRound()
        {
            _enemySpawnerSystem.RandomSpawnOnGridTop();
            _itemSpawnerSystem.RandomSpawnOnGridTop();
            _mainGameRule.SwitchToNextRound(_mainGameRule.CurrentRoundState);
        }
        
        [ContextMenu("MoveDown")]
        private void OnEnemyRound()
        {
            _gridCharacters = new List<BaseGridCharacter>(_enemySpawnerSystem.EnemyList);
            _gridCharacters.AddRange(_itemSpawnerSystem.EnemyList);
            _gridCharacters.Sort((x, y) => 
                x.GridPosition.y.CompareTo(y.GridPosition.y));
            for (int i = 0; i < _gridCharacters.Count; i++)
            {
                if(_gridCharacters[i] == null) continue;
                if (_gridCharacters[i].GridPosition.y == 0)
                {
                    _gridCharacters[i].DestroyGridCharacter();
                    continue;
                }
                Vector2Int targetPos = new Vector2Int(_gridCharacters[i].GridPosition.x
                    , _gridCharacters[i].GridPosition.y - 1);
                _gridCharacters[i].Move(targetPos);
            }

            _itemSpawnerSystem.EnemyList.RemoveAll(item => item == null);
            _enemySpawnerSystem.EnemyList.RemoveAll(item => item == null);
            _mainGameRule.SwitchToNextRound(_mainGameRule.CurrentRoundState);
        }
        
        private void ItemBePickUp(ItemType arg1, float arg2)
        {
            switch (arg1)
            {
                case ItemType.Food:
                    GameManager.Instance.Player.PlayerStates.UpdateHungry(
                        GameManager.Instance.Player.PlayerStates.MaxHungry * arg2);
                    break;
                case ItemType.RandomItem:
                    print("To DrawCard");
                    _mainGameRule.SwitchRoundState(MainGameRoundState.DrawCard);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(arg1), arg1, null);
            }
        }
    }
}