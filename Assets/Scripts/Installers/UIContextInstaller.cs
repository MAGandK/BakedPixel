using UI;
using UI.Window.FailWindow;
using UI.Window.GameWindow;
using UI.Window.InventoryWindow;
using UI.Window.StartWindow;
using UI.Window.WinWindow;
using Zenject;

namespace Installers
{
    public class UIContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
          //  Container.Bind<IJoystickController>().To<Joystick>().FromComponentInHierarchy().AsSingle();
            
            BindWindow<StartWindowController, StartWindowView>();
            BindWindow<GameWindowController, GameWindowView>();
            BindWindow<FailWindowController, FailWindowView>();
            BindWindow<WinWindowController, WinWindowView>();
            BindWindow<InventoryWindowController, InventoryWindowView>();

            Container.Bind<IUIController>().To<UIController>().AsSingle().NonLazy();
        }

        private void BindWindow<TController, TWindowView>()
            where TController : IWindowController
            where TWindowView : IWindowView
        {
            Container.Bind(typeof(IWindowController), typeof(IInitializable)).To<TController>().AsSingle();
            Container.Bind<TWindowView>().FromComponentInHierarchy().AsSingle();
        }
    }
}