using UnityEngine;

namespace UIManagement.Controller
{
    /// <summary>
    /// This is Ui Controller class. It is used to create the logic part.
    /// </summary>
    public abstract class UIController : MonoBehaviour
    {
        protected UIElement OwnerUIElement;
        
        public virtual void Initialize(UIElement uIElement)
        {
            OwnerUIElement = uIElement;
        }

        public virtual void Show() { }

        public virtual void Hide() { }
    }
}