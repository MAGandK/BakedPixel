using UI.Window.GameWindow;
using UI.Window.StartWindow;

namespace UI.Window.FailWindow
{
    public class FailWindowController : AbstractWindowController<FailWindowView>
    {
        private FailWindowView _failWindowView;
        
        public FailWindowController(FailWindowView view) : base(view)
        {
            _failWindowView = view;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _failWindowView.SubscribeButton(OnRetryButtonClick, OnNoTryButtonClick);
        }

        private void OnNoTryButtonClick()
        {
            _uiController.ShowWindow<StartWindowController>();
        }

        private void OnRetryButtonClick()
        {
            _uiController.ShowWindow<GameWindowController>();
        }
    }
}