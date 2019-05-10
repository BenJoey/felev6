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

            // SendCommand = new DelegateCommand(param => AddNewShow());
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
                    newMovie.Poster = new ImageDto
                    {
                        ImageSmall = ImageHandler.OpenAndResize(dialog.FileName, 100),
                        ImageLarge = ImageHandler.OpenAndResize(dialog.FileName, 600)
                    };
                }
            }
            catch { }
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
