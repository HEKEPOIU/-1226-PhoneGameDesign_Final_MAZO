using UnityEngine.Events;
using UnityEngine.UI;

namespace UIManagement.Views
{
    public class StartView : View
    {
        private Button _startButton;
        
        public override void Initialize()
        {
            _startButton = GetComponent<Button>();
        }
        
        public void BindStartButton(UnityAction action) => _startButton.onClick.AddListener(action);
        public void UnbindStartButton(UnityAction action) => _startButton.onClick.RemoveListener(action);
        public void UnbindAllStartButton() => _startButton.onClick.RemoveAllListeners();
    }
}
