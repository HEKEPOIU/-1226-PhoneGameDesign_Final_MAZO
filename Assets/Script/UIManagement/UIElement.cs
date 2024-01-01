using UIManagement.Controller;
using UIManagement.Views;
using UnityEngine;
using UnityEngine.Serialization;

namespace UIManagement
{
    
    /// <summary>
    /// This is Ui Element class. It is UI Base part, which contains the controller and the view.
    /// and the controller is not required.
    /// </summary>
    public abstract class UIElement : MonoBehaviour
    {
        [HideInInspector] public UIController Controller;
        
        [HideInInspector] public View View;
        [HideInInspector] public UIManager UIManager;

        public void Initialize(UIManager uIManager)
        {
            UIManager = uIManager;
            PreInitialize();
            if (Controller != null) Controller.Initialize(this);
            View.Initialize();
            PostInitialize();
        }

        /// <summary>
        ///   This method is called before the initialization of the controller and the view.
        ///   Use this method to set the controller and the view.
        /// </summary>
        protected abstract void PreInitialize();
        protected virtual void PostInitialize() { }

        public virtual void Show()
        {
            if (Controller != null) Controller.Show();
            View.Show();
        }

        public virtual void Hide()
        {
            if (Controller != null) Controller.Hide();
            View.Hide();
        }
        protected virtual void OnDestroy() => UIManager.RemoveElement(this);
    }
}