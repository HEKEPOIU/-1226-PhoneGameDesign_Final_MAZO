using UIManagement.Controller;
using UIManagement.Views;

namespace UIManagement.Element
{
    public class EndUIElement : UIElement
    {
        protected override void PreInitialize()
        {
            Controller = gameObject.AddComponent<EndController>();
            View = gameObject.GetComponent<EndView>();
        }
    }
}