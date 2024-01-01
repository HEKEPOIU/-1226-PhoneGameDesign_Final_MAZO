using System;
using Character;
using GirdSystem;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public PlayerStates PlayerStates;
        [HideInInspector] public PlayerCharacter PlayerCharacter;
        [SerializeField] private StraightAttacker _attackPrefab;
        public event Action OnActionEnd;
        public event Action<int> OnPlayerDash;
        public MainInput MainInput;
        private bool _isRangeAttack = false;
        private int _cacheRangeAttackCount = 0;

        private void Awake()
        {
            MainInput = new MainInput();
        }

        private void Start()
        {
            MainInput.MoveStates.Move.performed += Move;
            PlayerCharacter.OnPlayerBeHit += PlayerStates.ModifyHungry;
            PlayerCharacter.OnDash += Dash;
            
        }

        private void OnDestroy()
        {
            MainInput.MoveStates.Move.performed -= Move;
            PlayerCharacter.OnPlayerBeHit -= PlayerStates.ModifyHungry;
            PlayerCharacter.OnDash += Dash;
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
            MainInput.MoveStates.Move.Disable();
            OnActionEnd?.Invoke();
        }
        public void Attack() => Attack(PlayerCharacter.GridPosition);
        
        public void Attack(Vector2Int characterPosition)
        {
            Vector2Int attackPosition = characterPosition + new Vector2Int(0, 1);
            StraightAttacker attackPrefab = 
                Instantiate(_attackPrefab, GridGenerator.Instance.Grid.GetCellCenterPosition
                    (attackPosition.x, attackPosition.y), Quaternion.identity);
            attackPrefab.OnHitTarget += AttackEnd;
            attackPrefab.Owner = PlayerCharacter;
            attackPrefab.InitGridObject(attackPosition);

        }
        
        public void RangeAttack()
        {
            _isRangeAttack = true;
            _cacheRangeAttackCount = GridGenerator.Instance.Grid.Width;
            for (int i = 0; i < GridGenerator.Instance.Grid.Width; i++)
            {
                Attack(new Vector2Int(i, PlayerCharacter.GridPosition.y));
            }
        }

        private void AttackEnd(StraightAttacker obj)
        {
            if (_isRangeAttack)
            {
                _cacheRangeAttackCount--;
                obj.OnHitTarget -= AttackEnd;
                if (_cacheRangeAttackCount <= 0)
                {
                    _isRangeAttack = false;
                    OnActionEnd?.Invoke();
                }
            }
            else
            {
                obj.OnHitTarget -= AttackEnd;
                OnActionEnd?.Invoke();   
            }
        }
        
        public void Dash(int distance)
        {
            print("dash");
            OnPlayerDash?.Invoke(distance);
        }
    }
}