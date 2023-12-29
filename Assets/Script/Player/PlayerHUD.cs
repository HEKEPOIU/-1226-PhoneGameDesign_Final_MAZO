using System;
using UIManagement;
using UnityEngine;

namespace Player
{
    public class PlayerHUD : MonoBehaviour
    {
        private ViewManager _viewManager;

        private void Awake()
        {
            _viewManager = GetComponent<ViewManager>();
        }
    }
}