using GalaSoft.MvvmLight;

using NotABookLibraryStandart.Models.Roles;

namespace NotABookViewModels
{
    public class ViewModelCustomBase : ViewModelBase
    {
        protected readonly IAuthenticationService _authenticationService;
        public ViewModelCustomBase(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
    }
}
