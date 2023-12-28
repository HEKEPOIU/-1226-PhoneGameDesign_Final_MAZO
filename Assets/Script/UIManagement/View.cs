using UnityEngine;

namespace UIManagement
{
    public abstract class View : MonoBehaviour
    {
        public abstract void Initialize();
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Hide() => gameObject.SetActive(false);
        protected virtual void Awake() => ViewManager.AddView(this);
    }
}
