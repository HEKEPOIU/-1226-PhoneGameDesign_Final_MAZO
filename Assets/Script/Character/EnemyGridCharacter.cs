using System;
using GirdSystem;
using UnityEngine;

namespace Character
{
    public class EnemyGridCharacter : BaseGridCharacter
    {
        public event Action<float> OnDestroyByPlayer;
        [SerializeField] private float _conquerRateDelta = 0.05f;
        public override void Interact(IGridObject interacter)
        {
            base.Interact(interacter);
            BaseGridCharacter playerCharacter = interacter as BaseGridCharacter;
            if (playerCharacter != null && playerCharacter.Owner is PlayerCharacter)
            {
                OnDestroyByPlayer?.Invoke(_conquerRateDelta);
            }

            DestroyGridCharacter();
        }
        
        public override void InteractOther(IGridObject target)
        {
            base.InteractOther(target);
            PlayerCharacter playerCharacter = target as PlayerCharacter;

            if (playerCharacter)
            {
                if (playerCharacter.IsUnstoppable)
                {
                    OnDestroyByPlayer?.Invoke(_conquerRateDelta);
                }
                DestroyGridCharacter();
            }
        }
        
    }
}