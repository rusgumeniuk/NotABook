using NotABookViewModels;

namespace NotABookViewModels
{
    public interface IWindow
    {
        ViewModelCustomBase ViewModel { get; set; }
        void ProcessMessage(string message);
    }
}
