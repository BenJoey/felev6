using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.WPF.ViewModel
{
    public class ReservationButton : ViewModelBase
    {
        public Int32 Id { get; set; }
        public Int32 Row { get; set; }
        public Int32 Col { get; set; }
        private String _state;
        public String NameReserved { get; set; }
        public String PhoneNum { get; set; }
        public DelegateCommand ButtonClick { get; set; }

        public String State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }
    }
}
