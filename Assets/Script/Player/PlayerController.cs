using System;
using Character;
using GirdSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public BaseGridCharacter PlayerCharacter;
        [SerializeField] private BaseGridCharacter _attackPrefab;
        public event Action OnMoveEnd;
        public event Action OnAttackEnd;
        public MainInput MainInput;

        private void Awake()
        {
            MainInput = new MainInput();
        }

        private void Start()
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
            PlayerCharacter.Move(new Vector2Int(PlayerCharacter.GridPosition.x + (int)dir,
                                                            PlayerCharacter.GridPosition.y));
            EndMove();
        }

        public void StartMove()
        {
            MainInput.MoveStates.Move.Enable();
        }

        private void EndMove()
        {
            OnMoveEnd?.Invoke();
            MainInput.MoveStates.Move.Disable();
        }

        public void Attack()
        {
            Vector2Int attackPosition = PlayerCharacter.GridPosition + new Vector2Int(0, 1);
            BaseGridCharacter attackPrefab = 
                GridGenerator.Instance.SpawnGridCharacter(_attackPrefab, attackPosition);
            attackPrefab.OnDeath += OnAttackEnd;

        }
    }
}