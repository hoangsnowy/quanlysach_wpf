using QuanLySach.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using QuanLySach.Commands;
using QuanLySach.DAL.WindowRegistry.Interfaces;
using QuanLySach.Settings;

namespace QuanLySach.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        private ApplicationSetting _applicationSetting;
        private readonly IApplicationSettingDataService _applicationSettingDataService;
        public SettingViewModel(ApplicationSetting applicationSetting, IApplicationSettingDataService applicationSettingDataService)
        {
            _applicationSetting = applicationSetting;
            _applicationSettingDataService = applicationSettingDataService;
            SaveSettingCommand = new ReplayCommand(SaveSetting);
            LoadSettings();
        }

        public int NumberOfProductsPerPage { get; set; }
        public int NumberOfOrdersPerPage { get; set; }
        public ICommand SaveSettingCommand { get; set; }
        public delegate void CloseCommand();
        public event CloseCommand OnClose;

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadSettings()
        {
            NumberOfProductsPerPage = _applicationSetting.NumberOfProductsPerPage;
            NumberOfOrdersPerPage = _applicationSetting.NumberOfOrdersPerPage;
        }

        private void SaveSetting()
        {
            _applicationSetting.NumberOfOrdersPerPage = NumberOfOrdersPerPage;
            _applicationSetting.NumberOfProductsPerPage = NumberOfProductsPerPage;
            _applicationSettingDataService.Save(new DomainModels.ApplicationSetting
            {
                NumberOfOrdersPerPage = _applicationSetting.NumberOfOrdersPerPage,
                NumberOfProductsPerPage = _applicationSetting.NumberOfProductsPerPage
            });
            OnClose.Invoke();
        }
    }
}
