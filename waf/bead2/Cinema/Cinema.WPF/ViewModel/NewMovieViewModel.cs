using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.WPF.Model;
using Cinema.Persistence;
using Cinema.Persistence.DTOs;
using Microsoft.Win32;

namespace Cinema.WPF.ViewModel
{
    public class NewMovieViewModel : ViewModelBase
    {
        private readonly ICinemaService _model;
        private MovieDto newMovie;
        public String PosterPath { get; private set; }

        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand OpenPicture { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public event EventHandler Success;
        public event EventHandler Canceled;

        public NewMovieViewModel(ICinemaService model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            newMovie = new MovieDto();

            SendCommand = new DelegateCommand(param => AddNewMovie());
            CancelCommand = new DelegateCommand(param => OnCancel());
            OpenPicture = new DelegateCommand(param => CreateImage());
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

        private void CreateImage() {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.CheckFileExists = true;
                dialog.Filter = "Képfájlok|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                Boolean? result = dialog.ShowDialog();

                if (result == true)
                {
                    PosterPath = dialog.FileName;
                    OnPropertyChanged(nameof(PosterPath));
                    newMovie.Poster = ImageHandler.OpenAndResize(dialog.FileName, 600);
                }
            }
            catch { }
        }

        private async void AddNewMovie()
        {
            if (!CheckModel()) { return;}
            if (await _model.SendNewMovie(NewMovie))
            {
                OnSuccessfulAdd();
            }
            else
            {
                OnMessageApplication("Error happened during the process.");
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

        private Boolean CheckModel()
        {
            if (NewMovie.Poster == null)
            {
                OnMessageApplication("No Poster Selected");
                return false;
            }
            if (NewMovie.Description == "")
            {
                OnMessageApplication("Empty Description");
                return false;
            }
            if (NewMovie.Title == "")
            {
                OnMessageApplication("Empty Title");
                return false;
            }
            if (NewMovie.Length == "")
            {
                OnMessageApplication("Empty Title");
                return false;
            }
            if (NewMovie.Director == "")
            {
                OnMessageApplication("No director given");
                return false;
            }
            return true;
        }
    }
}
