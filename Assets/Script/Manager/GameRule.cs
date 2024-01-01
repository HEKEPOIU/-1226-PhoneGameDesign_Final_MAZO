using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class GameRule : MonoBehaviour
    {
        [Range(0f,1f)] 
        public float HigherHungryRate = 0.8f;
        [Range(0f,1f)] 
        public float LowerHungryRate = .3f;
        [Range(0f,1f)]
        public float StartHungryRate = 0.7f;
    }
}