using System;


namespace Character.Spawner
{
    public class GridEnemySpawnerSystem : GridSpawnerSystem<EnemyGridCharacter>
    {
        public event Action<float> OnPlayerKillEnemy;

        protected override void OnCharacterDeath(BaseGridCharacter sender)
        {
            base.OnCharacterDeath(sender);
            EnemyGridCharacter enemyGridCharacter = sender as EnemyGridCharacter;
            enemyGridCharacter.OnDestroyByPlayer -= OnPlayerKillEnemy;
        }
    
        protected override void PostCharacterSpawn(EnemyGridCharacter spawnedEnemy)
        {
            base.PostCharacterSpawn(spawnedEnemy);
            spawnedEnemy.OnDestroyByPlayer += OnPlayerKillEnemy;
        }
    }
}