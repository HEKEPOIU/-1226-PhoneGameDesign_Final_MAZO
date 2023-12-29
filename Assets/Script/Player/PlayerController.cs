using System;
using Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public BaseGridCharacter PlayerCharacter;
        public event Action OnMoveEnd;
        public MainInput MainInput;

        private void Awake()
        {
            MainInput.MoveStates.Move.performed += Move;
        }

        private void OnDestroy()
        {
            MainInput.MoveStates.Move.performed -= Move;
        }

        private void Move(InputAction.CallbackContext ctx)
        {
            float dir = ctx.ReadValue<float>();
            PlayerCharacter.Move(new Vector2Int((int)dir, 0));
            OnMoveEnd?.Invoke();
        }
    }
}