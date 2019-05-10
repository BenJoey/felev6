using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cinema.WPF.Model;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;

namespace Cinema.WPF.ViewModel
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly ICinemaService _model;
        private ObservableCollection<ShowDto> shows;
        private ObservableCollection<ReservationButton> seats;
        private IEnumerable<SeatDto> savedSeatDtos;
        private String sname;
        private String sphone;
        private ReservationDto _newReservation;
        private ShowDto _selectedShow;

        private Int32 RowNum;
        private Int32 ColNum;

        public DelegateCommand OpenShowSeats { get; set; }
        public DelegateCommand SellTickets { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public event EventHandler Success;
        public event EventHandler Canceled;

        public ReservationViewModel(ICinemaService model)
        {
            this._model = model ?? throw new ArgumentNullException(nameof(model));
            LoadData();

            _newReservation = new ReservationDto();
            CancelCommand = new DelegateCommand(param => OnCancel());
            OpenShowSeats = new DelegateCommand(param => LoadSeats(param as ShowDto));
            SellTickets = new DelegateCommand(param => SendReservation());
        }

        public ObservableCollection<ShowDto> Shows => shows;
        public ObservableCollection<ReservationButton> Seats => seats;
        public Int32 Rows => RowNum;
        public Int32 Columns => ColNum;
        public String DisplayedName => sname;
        public String DisplayedPhone => sphone;

        public ReservationDto NewReserve
        {
            get => _newReservation;
            set
            {
                _newReservation = value;
                OnPropertyChanged();
            }
        }

        public ShowDto SelectedShow
        {
            get => _selectedShow;
            set
            {
                if (_selectedShow != value)
                {
                    _selectedShow = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private async void LoadSeats(ShowDto selected)
        {
            try
            {
                savedSeatDtos = await _model.LoadSeats(selected.showId);
                seats = new ObservableCollection<ReservationButton>();
                foreach (var Seat in savedSeatDtos)
                {
                    seats.Add(new ReservationButton
                    {
                        Id = Seat.Id,
                        Col = Seat.Col,
                        Row = Seat.Row,
                        NameReserved = Seat.NameReserved,
                        PhoneNum = Seat.PhoneNum,
                        State = Seat.State,
                        ButtonClick = new DelegateCommand(param => Click((int)param))
                });
                }
                RowNum = seats.Max(o => o.Row);
                ColNum = seats.Max(o => o.Col);
                OnPropertyChanged(nameof(Rows));
                OnPropertyChanged(nameof(Columns));
                OnPropertyChanged(nameof(Seats));
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }

        private void Click(int id)
        {
            var selected = seats.FirstOrDefault(o => o.Id == id);
            if(selected == null)
                return;
            if (selected.State == "Sold" || selected.State == "Reserved")
            {
                sname = selected.NameReserved;
                sphone = selected.PhoneNum;
            }
            else
            {
                sname = "";
                sphone = "";
            }

            if (selected.State != "Sold" && selected.State != "Selected")
            {
                selected.State = "Selected";
            }

            else if (selected.State == "Selected")
            {
                selected.State = savedSeatDtos.FirstOrDefault(o => o.Id == selected.Id)?.State;
            }

            OnPropertyChanged(nameof(DisplayedName));
            OnPropertyChanged(nameof(DisplayedPhone));
        }

        private async void SendReservation()
        {
            _newReservation.SelectedSeats = seats.Where(o => o.State == "Selected").Select(o => o.Id);
            if (!_newReservation.SelectedSeats.Any())
            {
                OnMessageApplication("No Seats Selected");
                return;
            }

            if (await _model.SendReserve(_newReservation))
            {
                OnSucces();
            }
            else
            {
                OnMessageApplication("Error happened during the process.");
            }
        }

        private void OnSucces()
        {
            Success?.Invoke(this, EventArgs.Empty);
        }

        private void OnCancel()
        {
            Canceled?.Invoke(this, EventArgs.Empty);
        }
    }
}
