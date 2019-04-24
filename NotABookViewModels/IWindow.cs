using NotABookViewModels;

namespace NotABookViewModels
{
    interface IWindow
    {
        ViewModelCustomBase ViewModel { get; set; }
        void ProcessMessage(string message);
    }
}
