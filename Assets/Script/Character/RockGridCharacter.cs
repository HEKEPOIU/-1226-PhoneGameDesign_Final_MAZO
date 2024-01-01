using UnityEngine;

namespace Character
{
    public enum RockDirection
    {
        Left,
        Right
    }
    public class RockGridCharacter : ItemGridCharacter
    {
        public RockDirection RockDirection;
    }
}