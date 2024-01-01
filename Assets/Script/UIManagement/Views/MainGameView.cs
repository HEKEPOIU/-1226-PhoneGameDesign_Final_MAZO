
using System;
using SkillSystem.Skill;
using UIManagement.Element;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UIManagement.Views
{
    
    public class MainGameView : View
    {
        private Slider _hungrySlider;
        [SerializeField] private Slider _conquerSlider;
        [SerializeField] private GameObject _drawCardPanel;
        [SerializeField] private Button _drawCardButton;
        [SerializeField] private Button _dragonHeadButton;
        [SerializeField] private GameObject _drawCardSpawnPoint;
        [SerializeField] private GameObject _dragonHeadSpawnPoint;
        private Timer _closeDarwCardPanelTimer;
        private Image _hungrySliderfillImage;
        private Button _attactButton;
        private Button _moveButton;
        private GameObject _spawnObjTemp;
        private GameObject _spawnGragonBollObjTemp;

        
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
            _closeDarwCardPanelTimer = gameObject.AddComponent<Timer>();
        }

        public override void Show()
        {
            base.Show();
            SetDirArrowActive(false);
            SetGragonHeadButtonActive(false);
            DestroyGragonBollObjTemp();
            _closeDarwCardPanelTimer.OnTimerEnd += CloseDarwCardPanelTimerOnOnTimerEnd;
        }
        
        public override void Hide()
        {
            base.Hide();
            SetDirArrowActive(false);
            SetGragonHeadButtonActive(false);
            DestroyGragonBollObjTemp();
            _closeDarwCardPanelTimer.OnTimerEnd -= CloseDarwCardPanelTimerOnOnTimerEnd;
        }


        public void UpdateHungryBar(float value, float maxValue)
        {
            _hungrySlider.maxValue = maxValue;
            _hungrySlider.value = value;
            _hungrySliderfillImage.material.SetFloat("_CurrentPercentage", value / maxValue);
        }

        private void CloseDarwCardPanelTimerOnOnTimerEnd()
        {
            print("CloseDarwCardPanelTimerOnOnTimerEnd");
            SetDrawCardPanelActive(false);
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
        
        public void BindDrawCardButtonEvent(UnityEngine.Events.UnityAction action)
        {
            _drawCardButton.onClick.AddListener(action);
        }
        
        public void UnBindAllDrawCardButtonEvent()
        {
            _drawCardButton.onClick.RemoveAllListeners();
        }
        
        public void BindDragonHeadButtonEvent(UnityEngine.Events.UnityAction action)
        {
            _dragonHeadButton.onClick.AddListener(action);
        }
        public void UnBindAllDragonHeadButtonEvent()
        {
            _dragonHeadButton.onClick.RemoveAllListeners();
        }
        
        public void SetGragonHeadButtonActive(bool active)
        {
            _dragonHeadButton.interactable = active;
        }
        
        public void BindCloseDrawCardPanelTimerEvent(Action action)
        {
            _closeDarwCardPanelTimer.OnTimerEnd += action;
        }

        public void UnBindCloseDrawCardPanelTimerEvent(Action action)
        {
            _closeDarwCardPanelTimer.OnTimerEnd -= action;
        }
        
        public void SetAllCardButtonInteractable(bool interactable)
        {
            _attactButton.interactable = interactable;
            _moveButton.interactable = interactable;
        }

        public void SetDirArrowActive(bool active)
        {
            if(_dirArror == null) return;
            _dirArror.SetActive(active);
        }
        
        public void UpdateConquerBar(float value, float maxValue)
        {
            _conquerSlider.maxValue = maxValue;
            _conquerSlider.value = value;
        }
        
        public void SpawnDrawCardItem(GridPlayerSkillBase item)
        {
            _spawnObjTemp = Instantiate(item.SkillCardPrefab, _drawCardSpawnPoint.transform);
            _spawnObjTemp.transform.localPosition = Vector3.zero;
            //這裡可以對她做一點動畫
            _closeDarwCardPanelTimer.StartTimer();
        }
        
        public void SpawnDragonHeadItem(GridPlayerSkillBase item)
        {
            _spawnGragonBollObjTemp = Instantiate(item.SkillBallPrefab, _dragonHeadSpawnPoint.transform);
            _spawnGragonBollObjTemp.transform.localPosition = Vector3.zero;
        }
        
        public void DestroyGragonBollObjTemp()
        {
            if (_spawnGragonBollObjTemp == null) return;
            Destroy(_spawnGragonBollObjTemp);
        }
        
        public void SetDrawCardPanelActive(bool active)
        {
            _drawCardPanel.SetActive(active);
            Destroy(_spawnObjTemp);
        }
        
    }
}