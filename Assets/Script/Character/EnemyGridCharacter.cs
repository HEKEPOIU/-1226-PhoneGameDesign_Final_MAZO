using System;
using GirdSystem;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Character
{
    public class EnemyGridCharacter : BaseGridCharacter
    {
        public event Action<float> OnDestroyByPlayer;
        [SerializeField] private float _conquerRateDelta = 0.05f;
        [SerializeField] private GameObject _destroyEffect;
        

        public override void Interact(IGridObject interacter)
        {
            base.Interact(interacter);
            BaseGridCharacter playerCharacter = interacter as BaseGridCharacter;
            if (playerCharacter != null && playerCharacter.Owner is PlayerCharacter)
            {
                GameObject temp = Instantiate(_destroyEffect, transform.position, Quaternion.identity);
                Destroy(temp, 1f);
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
                    GameObject temp = Instantiate(_destroyEffect, transform.position, Quaternion.identity);
                    Destroy(temp, 1f);
                    OnDestroyByPlayer?.Invoke(_conquerRateDelta);
                }
                DestroyGridCharacter();
            }
        }
        
    }
}