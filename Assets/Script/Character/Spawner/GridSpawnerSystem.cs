using System.Collections.Generic;
using GirdSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Character.Spawner
{
    public abstract class GridSpawnerSystem<T> : MonoBehaviour where T : BaseGridCharacter
    {
        protected GridGenerator _gridGenerator;
        [FormerlySerializedAs("_enemyGridCharacter")] [SerializeField] protected T[] _characterGridCharacter;
        [SerializeField] protected int _maxSpawnCountPerRound = 3;
        [SerializeField] protected int _startSpawnCount = 20;
        [SerializeField] protected Vector2Int[] _spawnMasks;
        [Range(0f,1f)] [SerializeField] protected float _spawnRate = .7f;
        [HideInInspector] public List<T> EnemyList = new List<T>();
        


        protected virtual void Start()
        {
            _gridGenerator = GridGenerator.Instance;
        }

        public void RandomSpawnOnGrid() => RandomSpawnOnGrid(_startSpawnCount);
        public void RandomSpawnOnGrid(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnCharacter(GetRandomGridPosition());
            }
            EnemyList.RemoveAll(item => item == null);
        }

        
        public virtual void RandomSpawnOnGridTop()
        {
            if (_spawnRate <= Random.Range(0f, 1f)) return;
        
            for (int i = 0; i < _maxSpawnCountPerRound; i++)
            {
                SpawnCharacter(GetRandomGridTopPosition());
            }
            EnemyList.RemoveAll(item => item == null);
        }

        public void CharacterMoveDown()
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                if(EnemyList[i] == null) continue;
                if (EnemyList[i].GridPosition.y == 0)
                {
                    EnemyList[i].DestroyGridCharacter();
                    continue;
                }
                Vector2Int targetPos = new Vector2Int(EnemyList[i].GridPosition.x
                    , EnemyList[i].GridPosition.y - 1);
                EnemyList[i].Move(targetPos);
            }
        }
        
        public void ResetCharacter()
        {
            foreach (T enemy in EnemyList)
            {
                if (enemy == null) continue;
                
                OnCharacterDeath(enemy);
                enemy.OnDeath -= OnCharacterDeath;
                enemy.DestroyGridCharacter();
            }

            EnemyList.Clear();
        }

        protected void SpawnCharacter(Vector2Int spawnPosition)
        {
            foreach (Vector2Int mask in _spawnMasks)
            {
                if (spawnPosition == mask)
                {
                    return;
                }
            }

            if (_gridGenerator.Grid.GetValue(spawnPosition.x, spawnPosition.y) == null)
            {
                T enemyGridCharacter = 
                    (T)_gridGenerator.SpawnGridCharacter(
                        _characterGridCharacter[Random.Range(0,_characterGridCharacter.Length)], spawnPosition);
                EnemyList.Add(enemyGridCharacter);
                enemyGridCharacter.OnDeath += OnCharacterDeath;
                PostCharacterSpawn(enemyGridCharacter);
            }
            EnemyList.Sort((a, b) => a.GridPosition.y.CompareTo(b.GridPosition.y));
        }
        
        protected virtual void OnCharacterDeath(BaseGridCharacter sender){}

        protected virtual void PostCharacterSpawn(T spawnedEnemy){ }
        
        protected Vector2Int GetRandomGridTopPosition()
        {
            return new Vector2Int(Random.Range(0, _gridGenerator.Grid.Width), _gridGenerator.Grid.Height - 1);
        }
        
        protected Vector2Int GetRandomGridPosition()
        {
            return new Vector2Int(Random.Range(0, _gridGenerator.Grid.Width), Random.Range(0, _gridGenerator.Grid.Height));
        }
        
        
    }
}