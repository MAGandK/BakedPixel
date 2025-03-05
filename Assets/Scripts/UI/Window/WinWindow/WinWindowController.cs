using UI.Window.GameWindow;
using UI.Window.StartWindow;

namespace UI.Window.WinWindow
{
    public class WinWindowController : AbstractWindowController<WinWindowView>
    {
        private readonly WinWindowView _winView;

        public WinWindowController(WinWindowView view) : base(view)
        {
            _winView = view;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _winView.SubscribeButton(OnContinueButtonClick, OnRewardButtonClick);
            
        }

        private void OnContinueButtonClick()
        {
            _uiController.ShowWindow<GameWindowController>();
        }
        
        private void OnRewardButtonClick()
        {
                
        }
    }
}