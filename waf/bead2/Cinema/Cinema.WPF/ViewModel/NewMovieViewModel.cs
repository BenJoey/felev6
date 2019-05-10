using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.WPF.Model;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;

namespace Cinema.WPF.ViewModel
{
    public class NewMovieViewModel : ViewModelBase
    {
        private readonly ICinemaService _model;
        private MovieDto newMovie;

        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public event EventHandler Success;
        public event EventHandler Canceled;

        public NewMovieViewModel(ICinemaService model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            newMovie = new MovieDto();

            // SendCommand = new DelegateCommand(param => AddNewShow());
            CancelCommand = new DelegateCommand(param => OnCancel());
        }

        public MovieDto NewMovie
        {
            get => newMovie;
            set
            {
                newMovie = value;
                OnPropertyChanged();
            }
        }

        private void OnCancel()
        {
            Canceled?.Invoke(this, EventArgs.Empty);
        }

        private void OnSuccessfulAdd()
        {
            Success?.Invoke(this, EventArgs.Empty);
        }
    }
}
