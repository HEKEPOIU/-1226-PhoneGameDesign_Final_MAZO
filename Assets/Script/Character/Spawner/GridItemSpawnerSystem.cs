using System;
using UnityEngine;

namespace Character.Spawner
{
    public class GridItemSpawnerSystem : GridSpawnerSystem<ItemGridCharacter>
    {
        public event Action<ItemType, float> OnItemPickedUp;
        [SerializeField] private RockGridCharacter[] _rockPrefab;
        [SerializeField] private float _rockSpawnRate = 0.5f;

        protected override void PostCharacterSpawn(ItemGridCharacter spawnedEnemy)
        {
            spawnedEnemy.OnPickUp += OnItemPickedUp;
        }

        public override void RandomSpawnOnGridTop()
        {
            if (_spawnRate <= UnityEngine.Random.Range(0f, 1f)) return;
        

            for (int i = 0; i < 2; i++)
            {
                if (_rockSpawnRate <= UnityEngine.Random.Range(0f, 1f) ) continue;
                int targetRockIndex = UnityEngine.Random.Range(0, _rockPrefab.Length);
                Vector2Int spawnPosition;
                if (_rockPrefab[targetRockIndex].RockDirection == RockDirection.Left)
                {
                    spawnPosition = new Vector2Int(0, _gridGenerator.Grid.Height - 1);
                }
                else
                {
                    spawnPosition = new Vector2Int(_gridGenerator.Grid.Width - 1, _gridGenerator.Grid.Height - 1);
                }
                
                ItemGridCharacter enemyGridCharacter = 
                    (ItemGridCharacter)_gridGenerator.SpawnGridCharacter(_rockPrefab[targetRockIndex], spawnPosition);
                EnemyList.Add(enemyGridCharacter);
                enemyGridCharacter.OnDeath += OnCharacterDeath;
                PostCharacterSpawn(enemyGridCharacter);
            }
            
            for (int i = 0; i < _maxSpawnCountPerRound; i++)
            {
                SpawnCharacter(GetRandomGridTopPosition());
            }
                
            EnemyList.RemoveAll(item => item == null);
        }
        
        private Vector2Int GetRandomGridRTopCorner()
        {
            if (UnityEngine.Random.Range(0f, 1f) > .5)
            {
               return new Vector2Int(0, _gridGenerator.Grid.Height - 1);
            }
            else
            {
                return new Vector2Int(_gridGenerator.Grid.Width - 1, _gridGenerator.Grid.Height - 1);
            }
        }

        protected override void OnCharacterDeath(BaseGridCharacter sender)
        {
            base.OnCharacterDeath(sender);
            ItemGridCharacter item = (ItemGridCharacter)sender;
            item.OnPickUp -= OnItemPickedUp;
        }
    }
}