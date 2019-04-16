using NotABookViewModels;

namespace NotABookWPF
{
    interface IWindow
    {
        ViewModelCustomBase ViewModel { get; set; }
        void ProcessMessage(string message);
    }
}
