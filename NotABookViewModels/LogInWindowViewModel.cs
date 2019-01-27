using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NotABookLibraryStandart.DB;
using NotABookLibraryStandart.Models.Roles;

using System;
using System.Threading;
using System.Windows.Input;

namespace NotABookViewModels
{
    public class LogInWindowViewModel : ViewModelCustomBase
    {
        private RelayCommand loginCommand;
        private RelayCommand logoutCommand;
        private RelayCommand signUpCommand;

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
        public ICommand SignUpCommand
        {
            get
            {
                if (signUpCommand == null)
                    signUpCommand = new RelayCommand(ShowSignUpWindow);
                return signUpCommand;
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticated
        {
            get => Thread.CurrentPrincipal.Identity.IsAuthenticated;
        }

        public LogInWindowViewModel(IService service) : base(service)
        {
            
        }

        private bool CanLogin()
        {
            return !IsAuthenticated;
        }
        private void Login()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Password))
                    throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");
                //Validate credentials through the authentication service
                User user = Service.GetUser(Username, Password.Trim());

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
                Messenger.Default.Send("Error! Login failed! Please provide some valid credentials.");
            }
            catch (Exception ex)
            {
                Messenger.Default.Send($"Error: {ex.Message}");
            }
            finally
            {
                Messenger.Default.Send(IsAuthenticated ? "logged" : "unknown");
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
        private void ShowSignUpWindow()
        {
            Messenger.Default.Send("SignUp");
        }
    }
}