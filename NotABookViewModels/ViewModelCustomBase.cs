using GalaSoft.MvvmLight;

using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.Roles;

namespace NotABookViewModels
{
    public class ViewModelCustomBase : ViewModelBase
    {
        public readonly IService Service;
        public User CurrentUser
        {
            get => Service.GetUser((System.Threading.Thread.CurrentPrincipal as Principal).Identity.Name) ?? throw new System.UnauthorizedAccessException("Wrong CurrentUser");
        }
        public ViewModelCustomBase(IService service)
        {
            Service = service;
        }
    }
}
