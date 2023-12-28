using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIManagement
{
    public class ViewManager : MonoBehaviour
    {
        private static ViewManager _instance;
        [SerializeField] private View _startingView;
        private readonly List<View> _views = new List<View>();
        private View _currentView;
        private readonly Stack<View> _history = new Stack<View>();
        private void Awake() => _instance = this;
        private void Start()
        {
            foreach (var v in _views)
            {
                v.Initialize();
                v.Hide();
            }

            if (_startingView != null)
            {
                Show(_startingView);
            }

        }

        #region Static Methods
        public static T GetView<T>() where T: View
        {
            foreach (var view in _instance._views)
            {
                if (view is T tView)
                {
                    return tView;
                }
            }
            return null;
        }
        public static void AddView(View view)
        {
            if (!_instance._views.Contains(view))
            {
                _instance._views.Add(view);
            }
        }
        public static void Show<T>(bool remember = true) where T : View
        {
            foreach (var t in _instance._views)
            {
                if (t is not T) continue;
                if (_instance._currentView != null)
                {
                    if (remember)
                    {
                        _instance._history.Push(_instance._currentView);
                    }
                    _instance._currentView.Hide();
                }
                _instance._currentView = t;
                _instance._currentView.Show();
            }
        }
        public static void Show(View view, bool remember = true)
        {
            if (_instance._currentView != null)
            {
                if (remember)
                {
                    _instance._history.Push(_instance._currentView);
                }
                _instance._currentView.Hide();
            }
            _instance._currentView = view;
            _instance._currentView.Show();
        }
        public static void Show<T>(Action<T> action, bool remember = true) where T : View
        {
            foreach (var t in _instance._views)
            {
                if (t is not T) continue;
                if (_instance._currentView != null)
                {
                    if (remember)
                    {
                        _instance._history.Push(_instance._currentView);
                    }
                    _instance._currentView.Hide();
                }
                _instance._currentView = t;
                _instance._currentView.Show();
            }
        }
        public static void ShowLast()
        {
            if (_instance._history.Count != 0)
            {
                Show(_instance._history.Pop(), false);
            }

        }
        #endregion

    }
}
