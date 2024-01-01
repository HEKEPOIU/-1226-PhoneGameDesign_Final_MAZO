using System;
using GirdSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Character.Spawner
{
    public class GridEnemySpawnerSystem : MonoBehaviour
    {
        private GridGenerator _gridGenerator;
        [SerializeField] private EnemyGridCharacter[] _enemyGridCharacter;
        [SerializeField] private int _maxSpawnCountPerRound = 3;
        [SerializeField] private int _startSpawnCount = 20;
        private void Start()
        {
            _gridGenerator = GridGenerator.Instance;
            OnGameStart();
        }

        private void OnGameStart()
        {
            for (int i = 0; i < _startSpawnCount; i++)
            {
                SpawnEnemy(GetRandomGridPosition());
            }
        }
        
        private void OnRoundStart()
        {
            for (int i = 0; i < _maxSpawnCountPerRound; i++)
            {
                SpawnEnemy(GetRandomGridTopPosition());
            }
        }
        
        private void SpawnEnemy(Vector2Int spawnPosition)
        {
            if (_gridGenerator.Grid.GetValue(spawnPosition.x, spawnPosition.y) == null)
            {
                BaseGridCharacter enemyGridCharacter = 
                    _gridGenerator.SpawnGridCharacter(
                        _enemyGridCharacter[Random.Range(0,_enemyGridCharacter.Length)], spawnPosition);
                enemyGridCharacter.OnDeath += OnEnemyDeath;
            }
            
        }
        
        private void OnEnemyDeath()
        {
        }
        
        private Vector2Int GetRandomGridTopPosition()
        {
            return new Vector2Int(Random.Range(0, _gridGenerator.Grid.Width), _gridGenerator.Grid.Height - 1);
        }
        
        private Vector2Int GetRandomGridPosition()
        {
            return new Vector2Int(Random.Range(0, _gridGenerator.Grid.Width), Random.Range(0, _gridGenerator.Grid.Height));
        }
        
        
    }
}