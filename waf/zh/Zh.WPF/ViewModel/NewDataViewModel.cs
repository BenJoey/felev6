using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zh.WPF.Model;
using Zh.Persistence;
using Zh.Persistence.DTOs;
using Microsoft.Win32;

namespace Zh.WPF.ViewModel
{
    public class NewDataViewModel : ViewModelBase
    {
        private readonly ZhServices _model;
        private DataDto _newData;

        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }

        public event EventHandler Success;
        public event EventHandler LogoutSuccess;

        public NewDataViewModel(ZhServices model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            _newData = new DataDto();

            SendCommand = new DelegateCommand(param => SendNewData());
            LogoutCommand = new DelegateCommand(param => OnLogout());
        }

        public DataDto NewData
        {
            get => _newData;
            set
            {
                _newData = value;
                OnPropertyChanged();
            }
        }

        private async void SendNewData()
        {
            if (await _model.SendNewData(NewData))
            {
                OnSuccessfulAdd();
            }
            else
            {
                OnMessageApplication("Error happened during the process.");
            }
        }

        private void OnSuccessfulAdd()
        {
            Success?.Invoke(this, EventArgs.Empty);
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
