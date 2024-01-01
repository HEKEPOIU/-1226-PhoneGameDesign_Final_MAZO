using UIManagement.Controller;
using UIManagement.Views;

namespace UIManagement.Element
{
    public enum PlayerActionType
    {
        Attack,
        Move
    }
    public class MainGameUIElement : UIElement
    {
        protected override void PreInitialize()
        {
            Controller = gameObject.AddComponent<MainGameController>();
            View = gameObject.GetComponent<MainGameView>();
        }
    }
}