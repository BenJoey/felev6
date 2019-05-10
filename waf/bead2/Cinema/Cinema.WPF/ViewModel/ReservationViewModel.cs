using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.WPF.Model;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;

namespace Cinema.WPF.ViewModel
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly ICinemaService _model;
        private ObservableCollection<ShowDto> shows;

        public DelegateCommand OpenShowSeats { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public event EventHandler Success;
        public event EventHandler Canceled;

        public ReservationViewModel(ICinemaService model)
        {
            this._model = model ?? throw new ArgumentNullException(nameof(model));
            LoadData();

            // SendCommand = new DelegateCommand(param => AddNewShow());
            CancelCommand = new DelegateCommand(param => OnCancel());
        }

        public ObservableCollection<ShowDto> Shows => shows;

        private async void LoadData() {
            try
            {
                shows = new ObservableCollection<ShowDto>(await _model.LoadShows());
                OnPropertyChanged(nameof(Shows));
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }

        private void OnCancel()
        {
            Canceled?.Invoke(this, EventArgs.Empty);
        }
    }
}
