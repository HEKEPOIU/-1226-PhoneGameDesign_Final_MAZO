
using System;
using GirdSystem;
using UnityEngine;

namespace Character
{
    public enum ItemType
    {
        Food,
        RandomItem,
        Rock
    }
    public class ItemGridCharacter : BaseGridCharacter
    {
        public event Action<ItemType,float> OnPickUp;
        [SerializeField] private ItemType _itemType;
        [Range(0f, 1f)] [SerializeField] private float _restoreHungryRate;
        public ItemType ItemType { get => _itemType; private set=> _itemType = value; }
        public override void Interact(IGridObject interacter) => BePickUp(interacter);

        public override void InteractOther(IGridObject target) => BePickUp(target);

        private void BePickUp(IGridObject owner)
        {
            PlayerCharacter playerCharacter = owner as PlayerCharacter;
            if (!playerCharacter) return;
            
            OnPickUp?.Invoke(ItemType, playerCharacter.IsUnstoppable ? _restoreHungryRate * 1.5f : _restoreHungryRate);
            DestroyGridCharacter();
        }
        
    }
}