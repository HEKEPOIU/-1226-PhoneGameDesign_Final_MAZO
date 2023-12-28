using UnityEngine;

namespace Character
{
    public class EnemyGridCharacterTester : BaseGridCharacter
    {
        public override void Interact()
        {
            Destroy(gameObject);
        }
    }
}