using GalaSoft.MvvmLight;
using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.Roles;

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
