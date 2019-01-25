using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using NotABookLibraryStandart.Models.Roles;

using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace NotABookViewModels
{
    public class LogInWindowViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private RelayCommand loginCommand;
        private RelayCommand logoutCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new RelayCommand(Login, CanLogin);
                return loginCommand;
            }
        }
        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                    logoutCommand = new RelayCommand(Logout, CanLogout);
                return logoutCommand;
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticated
        {
            get => Thread.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public LogInWindowViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;                        
        }

        private bool CanLogin()
        {
            return !IsAuthenticated;
        }
        private void Login()
        {
            try
            {
                //Validate credentials through the authentication service
                User user = _authenticationService.AuthenticateUser(Username, Password?.Trim());

                //Get the current principal object
                if (!(Thread.CurrentPrincipal is Principal Principal))
                    throw new ArgumentException("The application's default thread principal must be set to a Principal object on startup.");

                //Authenticate the user
                Principal.Identity = new Identity(user.Username, user.Email, user.Roles);

                //Update UI
                loginCommand?.RaiseCanExecuteChanged();
                logoutCommand?.RaiseCanExecuteChanged();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Login failed! Please provide some valid credentials.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}");
            }
            finally
            {
                Messenger.Default.Send(IsAuthenticated ? "user" : "unknown");
            }
        }
        private bool CanLogout()
        {
            return IsAuthenticated;
        }
        private void Logout()
        {
            if (Thread.CurrentPrincipal is Principal Principal)
            {
                Principal.Identity = new AnonymusIdentity();
                loginCommand.RaiseCanExecuteChanged();
                logoutCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
