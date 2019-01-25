using GalaSoft.MvvmLight;

namespace NotABookWPF
{
    interface IWindow
    {
        ViewModelBase ViewModel { get; set; }
        void ProcessMessage(string message);
    }
}
