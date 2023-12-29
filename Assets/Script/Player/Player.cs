using Character;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private BaseGridCharacter _playerCharacter;
        private PlayerController _playerController;
        public MainInput MainPlayerInput;
        [SerializeField] private PlayerHUD _playerHUD;
        private void Awake()
        {
            //InitPlayerSelf;
            _playerCharacter = GetComponent<PlayerCharacter>();
            _playerController = GetComponent<PlayerController>();
            _playerController.PlayerCharacter = _playerCharacter;
            MainPlayerInput = new MainInput();
            _playerController.MainInput = MainPlayerInput;
        }
    }
}