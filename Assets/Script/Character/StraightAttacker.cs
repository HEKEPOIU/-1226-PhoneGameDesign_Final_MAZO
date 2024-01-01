using System;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class StraightAttacker : BaseGridCharacter
    {
        [SerializeField] private float _moveSpeedPerGrid = .5f;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(MoveFront(new Vector2Int(GridPosition.x, Grid.Height-1)));
            OnInteractOther += StopSelf;
        }
        
        IEnumerator MoveFront(Vector2Int newPosition)
        { 
            int direction = newPosition.y - GridPosition.y;
            while (direction != 0)
            {
                Move(new Vector2Int( GridPosition.x, GridPosition.y + 1));
                direction--;
                yield return new WaitForSeconds(_moveSpeedPerGrid);
            }
            Destroy(gameObject);
            
        }
        
   
        private void StopSelf()
        {
            Destroy(gameObject);
        }     
    }
    
}