using GalaSoft.MvvmLight;

using NotABookLibraryStandart.DB;

namespace NotABookViewModels
{
    public class ViewModelCustomBase : ViewModelBase
    {
        public readonly IService Service;
        public ViewModelCustomBase(IService service)
        {
            Service = service;
        }
    }
}
