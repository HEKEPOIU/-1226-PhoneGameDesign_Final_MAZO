using System;
using System.Collections;
using GirdSystem;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Character
{
    public class StraightAttacker : BaseGridCharacter
    {
        [SerializeField] private float _moveSpeedPerGrid = .5f;
        public event Action<StraightAttacker> OnHitTarget;


        protected void Start()
        {
            StartCoroutine(MoveFront(new Vector2Int(GridPosition.x, Grid.Height-1)));
        }

        IEnumerator MoveFront(Vector2Int newPosition)
        { 
            yield return new WaitForSeconds(_moveSpeedPerGrid);
            int direction = newPosition.y - GridPosition.y;
            while (direction != 0)
            {
                Move(new Vector2Int( GridPosition.x, GridPosition.y + 1));
                direction--;
                yield return new WaitForSeconds(_moveSpeedPerGrid);
            }
            OnHitTarget?.Invoke(this);
            DestroyGridCharacter();
            
        }

        public override void InteractOther(IGridObject target)
        {
            EnemyGridCharacter enemyGridCharacter = target as EnemyGridCharacter;
            if (enemyGridCharacter)
            {
                OnHitTarget?.Invoke(this);
                DestroyGridCharacter();
            }
            
        }


    }
    
}