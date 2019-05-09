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

            NewShow = new ShowDto();
            LoadData();
        }

        public ShowDto NewShow
        {
            get => newShow;
            set
            {
                newShow = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Movie> Movies
        {
            get => movies;
            set
            {
                movies = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get => rooms;
            set
            {
                rooms = value;
                OnPropertyChanged();
            }
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

        private async void AddNewShow()
        {
            try
            {
                if (await _model.AddNewShow(NewShow))
                {
                    OnSuccessfulAdd();
                }

            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }

        private void OnSuccessfulAdd()
        {
            Success?.Invoke(this, EventArgs.Empty);
        }
    }
}
