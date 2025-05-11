using Microsoft.Win32;
using QuanLySach.DAL.WindowRegistry.Interfaces;
using QuanLySach.DomainModels;

namespace QuanLySach.DAL.WindowRegistry
{
    public class ApplicationSettingDataService : IApplicationSettingDataService
    {
        private const string REGISTRY_KEY_SETTING = @"SOFTWARE\QuanLySachSettings";
        public ApplicationSetting Get()
        {
            #pragma warning disable CA1416 // Validate platform compatibility
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_SETTING);

            if (key == null)
            {
                return null;
            }

            bool isParseNumberOfProductsPerPage = int.TryParse(key.GetValue("numberOfProductsPerPage")?.ToString(), out var numberOfProductsPerPage);
            bool isParseNumberOfOrdersPerPage = int.TryParse(key.GetValue("numberOfOrdersPerPage")?.ToString(), out var numberOfOrdersPerPage);

            var setting = new ApplicationSetting()
            {
               NumberOfOrdersPerPage = isParseNumberOfOrdersPerPage ? numberOfOrdersPerPage : 8,
               NumberOfProductsPerPage = isParseNumberOfProductsPerPage ? numberOfProductsPerPage : 8,
            };
            key.Close();

            return setting;
            #pragma warning disable CA1416 
        }

        public void Save(ApplicationSetting applicationSetting)
        {
            #pragma warning disable CA1416 // Validate platform compatibility
            RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_SETTING);
            key.SetValue("numberOfProductsPerPage", applicationSetting.NumberOfProductsPerPage);
            key.SetValue("numberOfOrdersPerPage", applicationSetting.NumberOfOrdersPerPage);
            key.Close();
            #pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
