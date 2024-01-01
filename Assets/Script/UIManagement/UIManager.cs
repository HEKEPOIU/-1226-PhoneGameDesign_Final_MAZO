using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIManagement
{
    
    public class UIManager : MonoBehaviour
    {
        [HideInInspector] public Player.Player OwnerPlayer;
        [SerializeField] private UIElement _startingElement;
        private List<UIElement> _elements = new List<UIElement>();
        private UIElement _currentElement;
        private readonly Stack<UIElement> _history = new Stack<UIElement>();

        private void Awake()
        {
            _elements = FindObjectsOfType<UIElement>(true).ToList();
            foreach (var v in _elements)
            {
                v.Initialize(this);
                
                v.gameObject.SetActive(false);
            }
            if (_startingElement != null)
            {
                Show(_startingElement);
            }
        }

        #region Static Methods
        public T GetElement<T>() where T: UIElement
        {
            foreach (var view in _elements)
            {
                if (view is T tView)
                {
                    return tView;
                }
            }
            return null;
        }
        public void AddElement(UIElement view)
        {
            if (!_elements.Contains(view))
            {
                _elements.Add(view);
            }
        }
        public void RemoveElement(UIElement view)
        {
            if (_elements.Contains(view))
            {
                _elements.Remove(view);
            }
        }
        public void Show<T>(bool remember = true) where T : UIElement
        {
            
            foreach (var t in _elements)
            {
                if (t is not T) continue;
                if (_currentElement != null)
                {
                    if (remember)
                    {
                        _history.Push(_currentElement);
                    }
                    _currentElement.Hide();
                }
                _currentElement = t;
                _currentElement.Show();
            }
        }
        public void Show(UIElement view, bool remember = true)
        {
            if (_currentElement != null)
            {
                if (remember)
                {
                    _history.Push(_currentElement);
                }
                _currentElement.Hide();
            }
            _currentElement = view;
            _currentElement.Show();
        }
        public void Show<T>(Action<T> action, bool remember = true) where T : UIElement
        {
            foreach (var t in _elements)
            {
                if (t is not T) continue;
                if (_currentElement != null)
                {
                    if (remember)
                    {
                        _history.Push(_currentElement);
                    }
                    _currentElement.Hide();
                }
                _currentElement = t;
                _currentElement.Show();
            }
        }
        public void ShowLast()
        {
            if (_history.Count != 0)
            {
                Show(_history.Pop(), false);
            }

        }
        #endregion

    }
}
