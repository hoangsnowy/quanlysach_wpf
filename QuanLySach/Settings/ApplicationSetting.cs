using QuanLySach.DAL.WindowRegistry.Interfaces;

namespace QuanLySach.Settings
{
    public class ApplicationSetting
    {
        private readonly IApplicationSettingDataService _applicationSettingDataService;
        public ApplicationSetting(IApplicationSettingDataService applicationSettingDataService)
        {
            _applicationSettingDataService = applicationSettingDataService;
            DomainModels.ApplicationSetting applicationSetting = _applicationSettingDataService.Get();
            NumberOfOrdersPerPage = applicationSetting.NumberOfOrdersPerPage;
            NumberOfProductsPerPage = applicationSetting.NumberOfProductsPerPage;
        }
        public int NumberOfProductsPerPage { get; set; }
        public int NumberOfOrdersPerPage { get; set; }
    }
}
