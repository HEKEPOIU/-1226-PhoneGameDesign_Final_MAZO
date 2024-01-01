using Manager;
using Manager.States;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        private MainGameState _mainGameState;
        [SerializeField] private GameObject _mapObject;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        private void Awake()
        {
            GameStateBase.OnStateStart += SwitchGameState;
        }
        

        private void OnDestroy()
        {
            GameStateBase.OnStateStart -= SwitchGameState;
        }

        private void SwitchGameState(GameStateBase obj)
        {
            if (obj.StateType == GameStateType.MainGame)
            {
                _mapObject.SetActive(true);
                _mainGameState = obj as MainGameState;
                ResetMap();
            }
            else _mapObject.SetActive(false);
        } 
        
        public void ResetMap()
        {
            _mapObject.transform.localPosition = _startPoint.localPosition;
        }
        
        
        public void UpdateMapPosition(float pathPercent)
        {
            Vector3 targetPos = Vector3.Lerp(_startPoint.localPosition, _endPoint.localPosition, pathPercent);
            _mapObject.transform.localPosition = targetPos;
        }
        
    }
}