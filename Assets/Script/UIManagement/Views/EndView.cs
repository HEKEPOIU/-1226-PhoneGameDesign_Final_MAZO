using Manager;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIManagement.Views
{
    public class EndView : View
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Image _endImage;
        [SerializeField] private Image _successTextImage;
        [SerializeField] private Image _failTextImage;
        [SerializeField] private Sprite _failSprite;
        [SerializeField] private Sprite _successSprite;
        [SerializeField] private Sprite _trueSuccessSprite;
        
        private GameStateType _gameStateType;
        [SerializeField] private MMF_Player _failplayer;
        [SerializeField] private MMF_Player _successplayer;
        public override void Initialize()
        {
        }

        public override void Show()
        {
            base.Show();
        }

        public void BindRestartButton(UnityAction action)
        {
            _restartButton.onClick.AddListener(action);
        }

        public void UnBindRestartButton(UnityAction action)
        {
            _restartButton.onClick.RemoveListener(action);
        }

        public void UnBindAllRestartButton()
        {
            _restartButton.onClick.RemoveAllListeners();
        }
        
        public void SetEndImage(GameStateType obj)
        {
            _gameStateType = obj;
            _endImage.sprite = obj switch
            {
                GameStateType.Fail => _failSprite,
                GameStateType.Success => _successSprite,
                GameStateType.TrueSuccess => _trueSuccessSprite,
                _ => _endImage.sprite
            };
            if (obj == GameStateType.Fail)
            {
                _successTextImage.gameObject.SetActive(false);
                _failTextImage.gameObject.SetActive(true);
                _failplayer.PlayFeedbacks();

            }
            else
            {
                _successTextImage.gameObject.SetActive(true);
                _failTextImage.gameObject.SetActive(false);
                _successplayer.PlayFeedbacks();

            }
        }
        
    }
}