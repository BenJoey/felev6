﻿using System;
using System.Configuration;
using System.Windows;

using Cinema.WPF.Model;
using Cinema.WPF.View;
using Cinema.WPF.ViewModel;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;

namespace Cinema.WPF
{
    public partial class App : Application
    {
        private ICinemaService _service;
        private LoginViewModel _loginViewModel;
        private MainWindow _mainWindow;
        private LoginWindow _loginWindow;

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
            // TODO
        }

        private void ViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Invalid username or password!", "Login", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "Login", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
