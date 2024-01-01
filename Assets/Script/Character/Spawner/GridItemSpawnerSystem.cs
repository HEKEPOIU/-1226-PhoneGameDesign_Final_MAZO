
using System;

namespace Character.Spawner
{
    public class GridItemSpawnerSystem : GridSpawnerSystem<ItemGridCharacter>
    {
        public event Action<ItemType, float> OnItemPickedUp;

        protected override void PostCharacterSpawn(ItemGridCharacter spawnedEnemy)
        {
            spawnedEnemy.OnPickUp += OnItemPickedUp;
        }

        protected override void OnCharacterDeath(BaseGridCharacter sender)
        {
            base.OnCharacterDeath(sender);
            ItemGridCharacter item = (ItemGridCharacter)sender;
            item.OnPickUp -= OnItemPickedUp;
        }
    }
}