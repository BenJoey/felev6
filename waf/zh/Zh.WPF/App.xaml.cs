using System;
using System.Configuration;
using System.Windows;

using Zh.WPF.Model;
using Zh.WPF.View;
using Zh.WPF.ViewModel;

namespace Zh.WPF
{
    public partial class App : Application
    {
        private readonly Boolean _needLogIn = false;
        private ZhServices _service;
        private LoginViewModel _loginViewModel;
        private NewDataViewModel _dataViewModel;
        private LoginWindow _loginWindow;
        private NewDataWindow _movieWindow;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new ZhServices(ConfigurationManager.AppSettings["baseAddress"]);

            if (_needLogIn)
            {
                _loginViewModel = new LoginViewModel(_service);

                _loginViewModel.ExitApplication += ViewModel_ExitApplication;
                _loginViewModel.MessageApplication += ViewModel_MessageApplication;
                _loginViewModel.LoginSuccess += OpenNewData;
                _loginViewModel.LoginFailed += ViewModel_LoginFailed;

                _loginWindow = new LoginWindow
                {
                    DataContext = _loginViewModel
                };
                _loginWindow.Show();
            }
            else
            {
                _dataViewModel = new NewDataViewModel(_service);
                _dataViewModel.LogoutSuccess += ViewModel_Logout;
                _dataViewModel.MessageApplication += ViewModel_MessageApplication;
                _dataViewModel.Success += (o, args) =>
                {
                    ShowMsgBox("Successfully added");
                };

                _movieWindow = new NewDataWindow { DataContext = _dataViewModel };

                _movieWindow.Show();
            }
        }

        private void ViewModel_ExitApplication(object sender, EventArgs e)
        {
            Shutdown();
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
            if (_needLogIn)
            {
                _loginWindow = new LoginWindow
                {
                    DataContext = _loginViewModel
                };
                _loginViewModel = new LoginViewModel(_service);

                _loginViewModel.ExitApplication += ViewModel_ExitApplication;
                _loginViewModel.MessageApplication += ViewModel_MessageApplication;
                _loginViewModel.LoginSuccess += OpenNewData;
                _loginViewModel.LoginFailed += ViewModel_LoginFailed;
                _loginWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }

        private void OpenNewData(object sender, EventArgs e)
        {
            _dataViewModel = new NewDataViewModel(_service);
            _dataViewModel.LogoutSuccess += ViewModel_Logout;
            _dataViewModel.MessageApplication += ViewModel_MessageApplication;
            _dataViewModel.Success += (o, args) =>
            {
                ShowMsgBox("Successfully added");
            };

            _movieWindow = new NewDataWindow{DataContext = _dataViewModel};

            _movieWindow.Show();
            _loginWindow.Close();
        }

        private void ShowMsgBox(String msg, String source = "")
        {
            MessageBox.Show(msg, source, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
