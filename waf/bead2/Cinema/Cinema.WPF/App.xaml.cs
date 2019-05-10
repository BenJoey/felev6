using System;
using System.Configuration;
using System.Windows;

using Cinema.WPF.Model;
using Cinema.WPF.View;
using Cinema.WPF.ViewModel;

namespace Cinema.WPF
{
    public partial class App : Application
    {
        private ICinemaService _service;
        private LoginViewModel _loginViewModel;
        private MenuViewModel _menuViewModel;
        private NewShowViewModel _showViewModel;
        private ReservationViewModel _reservationViewModel;
        private NewMovieViewModel _movieViewModel;
        private LoginWindow _loginWindow;
        private MenuWindow _menuWindow;
        private NewShowWindow _showWindow;
        private ReservationWindow _reservationWindow;
        private NewMovieWindow _movieWindow;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new CinemaServices(ConfigurationManager.AppSettings["baseAddress"]);

            _loginViewModel = new LoginViewModel(_service);

            _loginViewModel.ExitApplication += ViewModel_ExitApplication;
            _loginViewModel.MessageApplication += ViewModel_MessageApplication;
            _loginViewModel.LoginSuccess += ViewModel_LoginSuccess;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;

            _loginWindow = new LoginWindow
            {
                DataContext = _loginViewModel
            };
            _loginWindow.Show();
        }

        private void ViewModel_ExitApplication(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void ViewModel_LoginSuccess(object sender, EventArgs e)
        {
            OpenMenu(_loginWindow);
        }

        private void ViewModel_LoginFailed(object sender, EventArgs e)
        {
            ShowMsgBox("Invalid username or password!", "Login");
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            ShowMsgBox(e.Message);
        }

        private void ViewModel_Logout(object sender, EventArgs e)
        {
            _loginWindow = new LoginWindow
            {
                DataContext = _loginViewModel
            };
            _loginViewModel = new LoginViewModel(_service);

            _loginViewModel.ExitApplication += ViewModel_ExitApplication;
            _loginViewModel.MessageApplication += ViewModel_MessageApplication;
            _loginViewModel.LoginSuccess += ViewModel_LoginSuccess;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;
            _loginWindow.Show();
            _menuWindow.Close();
        }

        private void OpenNewMovie(object sender, EventArgs e)
        {
            _movieViewModel = new NewMovieViewModel(_service);
            _movieViewModel.Canceled += (o, args) => { OpenMenu(_movieWindow); };
            _movieViewModel.MessageApplication += ViewModel_MessageApplication;
            _movieViewModel.Success += (o, args) =>
            {
                OpenMenu(_movieWindow);
                ShowMsgBox("Successfully added");
            };

            _movieWindow = new NewMovieWindow{DataContext = _movieViewModel};

            _movieWindow.Show();
            _menuWindow.Close();
        }
        private void OpenNewShow(object sender, EventArgs e)
        {
            _showViewModel = new NewShowViewModel(_service);
            _showViewModel.Canceled += (o, args) => { OpenMenu(_showWindow); };
            _showViewModel.MessageApplication += ViewModel_MessageApplication;
            _showViewModel.Success += (o, args) =>
            {
                OpenMenu(_showWindow);
                ShowMsgBox("Successfully added");
            };

            _showWindow = new NewShowWindow {DataContext = _showViewModel};

            _showWindow.Show();
            _menuWindow.Close();
        }

        private void OpenNewReserve(object sender, EventArgs e)
        {
            _reservationViewModel = new ReservationViewModel(_service);
            _reservationViewModel.Canceled += (o, args) => { OpenMenu(_reservationWindow); };
            _reservationViewModel.MessageApplication += ViewModel_MessageApplication;
            _reservationViewModel.Success += (o, args) =>
            {
                OpenMenu(_reservationWindow);
                ShowMsgBox("Successfully sold");
            };

            _reservationWindow = new ReservationWindow {DataContext = _reservationViewModel};

            _reservationWindow.Show();
            _menuWindow.Close();
        }

        private void OpenMenu(Window previous = null)
        {
            _menuViewModel = new MenuViewModel(_service);
            _menuViewModel.NewMovie += OpenNewMovie;
            _menuViewModel.NewShow += OpenNewShow;
            _menuViewModel.Reserve += OpenNewReserve;
            _menuViewModel.LogoutSuccess += ViewModel_Logout;
            _menuViewModel.MessageApplication += ViewModel_MessageApplication;

            _menuWindow = new MenuWindow
            {
                DataContext = _menuViewModel
            };
            previous?.Close();
            _menuWindow.Show();
        }

        private void ShowMsgBox(String msg, String source = "")
        {
            MessageBox.Show(msg, source, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
