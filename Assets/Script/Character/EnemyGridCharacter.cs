using UnityEngine;

namespace Character
{
    public class EnemyGridCharacter : BaseGridCharacter
    {
        public override void Interact()
        {
            base.Interact();
            Destroy(gameObject);
        }
    }
}