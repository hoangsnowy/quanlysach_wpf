using Microsoft.Win32;
using QuanLySach.DAL.Interfaces;
using QuanLySach.DomainModels;

namespace QuanLySach.DAL.WindowRegistry
{
    public class WindowRegistryDataService : ICredentialDataService
    {
        private const string REGISTRY_KEY_SETTING = @"SOFTWARE\QuanLySachSettings";

        public User GetUserCredential()
        {
            #pragma warning disable CA1416 // Validate platform compatibility
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_SETTING);

            if (key == null)
            {
                return null;
            }

            string? email = key.GetValue("email")?.ToString();
            string? hashedPassword = key.GetValue("hashedPassword")?.ToString();
            bool rememberedMe = bool.Parse(key.GetValue("rememberedMe").ToString());
            var user = new User()
            {
                Email = email,
                HashPassword = hashedPassword,
                RememberedMe = rememberedMe
            };
            key.Close();
            return user;
            #pragma warning disable CA1416 // Validate platform compatibility
        }

        public void AddUserCredential(string email, string hashedPassword, bool rememberedMe)
        {
            #pragma warning disable CA1416 // Validate platform compatibility
            RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_SETTING);
            key.SetValue("email", email);
            key.SetValue("hashedPassword", hashedPassword);
            key.SetValue("rememberedMe", rememberedMe);
            key.Close();
            #pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
