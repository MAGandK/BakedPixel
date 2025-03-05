namespace UI
{
    public interface IUIController
    {
        public void ShowWindow<T>() where T : IWindowController;

        public T GetWindow<T>() where T : class,IWindowController;
    }
}