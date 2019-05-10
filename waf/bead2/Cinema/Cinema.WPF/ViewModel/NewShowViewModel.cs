using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Cinema.WPF.Model;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;

namespace Cinema.WPF.ViewModel
{
    public class NewShowViewModel : ViewModelBase
    {
        private readonly ICinemaService _model;
        private ObservableCollection<MovieDto> movies;
        private ObservableCollection<RoomDto> rooms;
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

            SendCommand = new DelegateCommand(param => AddNewShow());
            CancelCommand = new DelegateCommand(param => OnCancel());
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

        public ObservableCollection<MovieDto> Movies => movies;

        public ObservableCollection<RoomDto> Rooms => rooms;

        private async void LoadData()
        {
            try
            {
                movies = new ObservableCollection<MovieDto>(await _model.LoadMovies());
                rooms = new ObservableCollection<RoomDto>(await _model.LoadRooms());
                OnPropertyChanged(nameof(Movies));
                OnPropertyChanged(nameof(Rooms));
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

        private void OnCancel()
        {
            var t = 0;
            Canceled?.Invoke(this, EventArgs.Empty);
        }

        private void OnSuccessfulAdd()
        {
            Success?.Invoke(this, EventArgs.Empty);
        }
    }
}
