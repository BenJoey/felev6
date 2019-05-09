using System;
using System.Windows.Controls;
using Cinema.WPF.Model;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;

namespace Cinema.WPF.ViewModel
{
    class MenuViewModel : ViewModelBase
    {
        private readonly ICinemaService _model;


        public DelegateCommand NewMovieCommand { get; set; }
        public DelegateCommand NewShowCommand { get; set; }
        public DelegateCommand ReserveCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }

        public event EventHandler NewMovie;
        public event EventHandler NewShow;
        public event EventHandler Reserve;
        public event EventHandler LogoutSuccess;

        public MenuViewModel(ICinemaService model)
        {
            this._model = model ?? throw new ArgumentNullException(nameof(model));

            NewMovieCommand = new DelegateCommand(param => OnNewMovie());
            NewShowCommand = new DelegateCommand(param => OnNewShow());
            ReserveCommand = new DelegateCommand(param => OnReserve());
            LogoutCommand = new DelegateCommand(param => OnLogout());
        }

        private void OnNewMovie()
        {
            NewMovie?.Invoke(this, EventArgs.Empty);
        }

        private void OnNewShow()
        {
            NewShow?.Invoke(this, EventArgs.Empty);
        }

        private void OnReserve()
        {
            Reserve?.Invoke(this, EventArgs.Empty);
        }

        private async void OnLogout()
        {
            try
            {
                await _model.LogoutAsync();
                LogoutSuccess?.Invoke(this, EventArgs.Empty);
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Unexpected error! ({ex.Message})");
            }
        }
    }
}
