
using System;
using UIManagement.Element;
using UnityEngine;
using UnityEngine.UI;

namespace UIManagement.Views
{
    
    public class MainGameView : View
    {
        private Slider _hungrySlider;
        private Image _hungrySliderfillImage;
        private Button _attactButton;
        private Button _moveButton;
        private RectTransform _maxRangeTransform;
        private RectTransform _minRangeTransform;
        [SerializeField] private int _rangeIconThickness = 5;
        //TODO: 這裡不應該這麼做，因為他應該要是被動態產生的，不能這樣獲取非子物件的東西，要想辦法解決。
        [SerializeField] private GameObject _dirArror;

        public override void Initialize()
        {
            _hungrySlider = transform.Find("MainGame/HungryBar/Progress").GetComponent<Slider>();
            _maxRangeTransform = _hungrySlider.transform.Find("Fill Area/MaxRange").GetComponent<RectTransform>();
            _minRangeTransform = _hungrySlider.transform.Find("Fill Area/MinRange").GetComponent<RectTransform>();
            _hungrySliderfillImage = _hungrySlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
            _attactButton = transform.Find("MainGame/FooterBorder/BackGround/Cards/AttackButtonCard").GetComponent<Button>();
            _moveButton = transform.Find("MainGame/FooterBorder/BackGround/Cards/MoveButtonCard").GetComponent<Button>();
            _hungrySliderfillImage = _hungrySlider.fillRect.GetComponent<Image>();
        }

        public override void Show()
        {
            base.Show();
            SetDirArrowActive(false);
        }


        public void UpdateHungryBar(float value, float maxValue)
        {
            _hungrySlider.maxValue = maxValue;
            _hungrySlider.value = value;
            _hungrySliderfillImage.material.SetFloat("_CurrentPercentage", value / maxValue);
        }

        public void SetRange(float gameRuleHigherHungryRate, float gameRuleLowerHungryRate)
        {
            _maxRangeTransform.anchorMax = new Vector2(gameRuleHigherHungryRate + _rangeIconThickness * 0.001f, 1);
            _maxRangeTransform.anchorMin = new Vector2(gameRuleHigherHungryRate - _rangeIconThickness * 0.001f, 0);
            _minRangeTransform.anchorMax = new Vector2(gameRuleLowerHungryRate + _rangeIconThickness * 0.001f, 1);
            _minRangeTransform.anchorMin = new Vector2(gameRuleLowerHungryRate - _rangeIconThickness * 0.001f, 0);
            _minRangeTransform.offsetMax = new Vector2(0, 0);
            _minRangeTransform.offsetMin = new Vector2(0, 0);
            _maxRangeTransform.offsetMax = new Vector2(0, 0);
            _maxRangeTransform.offsetMin = new Vector2(0, 0);
            
        }
        
        public void BindCardButtonEvent(UnityEngine.Events.UnityAction action, PlayerActionType playerActionType)
        {
            switch (playerActionType)
            {
                case PlayerActionType.Attack:
                    _attactButton.onClick.AddListener(action);
                    break;
                case PlayerActionType.Move:
                    _moveButton.onClick.AddListener(action);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerActionType), playerActionType, "PlayerActionType not found");
            }
        }
        
        public void UnBindAllCardButtonEvent(PlayerActionType playerActionType)
        {
            switch (playerActionType)
            {
                case PlayerActionType.Attack:
                    _attactButton.onClick.RemoveAllListeners();
                    break;
                case PlayerActionType.Move:
                    _moveButton.onClick.RemoveAllListeners();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerActionType), playerActionType, "PlayerActionType not found");
            }
        }
        
        public void SetAllCardButtonInteractable(bool interactable)
        {
            _attactButton.interactable = interactable;
            _moveButton.interactable = interactable;
        }

        public void SetDirArrowActive(bool active)
        {
            _dirArror.SetActive(active);
        }
        
    }
}