using QuanLySach.DomainModels;

namespace QuanLySach.DAL.WindowRegistry.Interfaces
{
    public interface IApplicationSettingDataService
    {
        ApplicationSetting Get();
        void Save(ApplicationSetting applicationSetting);
    }
}
