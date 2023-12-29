using UnityEngine;

namespace Character
{
    public class PlayerCharacter : BaseGridCharacter
    {
        [SerializeField] private Vector2Int _startPosition;
        public override void Interact()
        {
        }
        
        protected override void InitGridObject()
        {
            base.InitGridObject();
            
            UpdatePosition(_startPosition);
        }
    }
}