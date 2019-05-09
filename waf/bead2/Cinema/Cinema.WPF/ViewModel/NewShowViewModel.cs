using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Cinema.WPF.Model;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;

namespace Cinema.WPF.ViewModel
{
    public class NewShowViewModel : ViewModelBase
    {
        private readonly ICinemaService _model;
        private ObservableCollection<Movie> movies;
        private ObservableCollection<Room> rooms;
        private ShowDto newShow;

        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public event EventHandler Success;
        public event EventHandler Canceled;

        public NewShowViewModel(ICinemaService model)
        {
            this._model = model ?? throw new ArgumentNullException(nameof(model));

            newShow = new ShowDto();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                movies = new ObservableCollection<Movie>(await _model.LoadMovies());
                rooms = new ObservableCollection<Room>(await _model.LoadRooms());
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }
    }
}
