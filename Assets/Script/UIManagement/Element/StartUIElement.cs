using UIManagement.Controller;

namespace UIManagement.Element
{
    public class StartUIElement : UIElement
    {
        protected override void PreInitialize()
        {
            Controller = gameObject.AddComponent<StartController>();
            View = gameObject.AddComponent<Views.StartView>();
        }
    }
}