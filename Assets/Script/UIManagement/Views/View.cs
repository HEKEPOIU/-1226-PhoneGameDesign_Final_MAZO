using UnityEngine;

namespace UIManagement.Views
{
    /// <summary>
    /// This is Ui View class. It is used to create the visual part.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        public abstract void Initialize();
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
    }
}
