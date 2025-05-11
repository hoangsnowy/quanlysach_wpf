using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuanLySach.DomainModels.Annotations;

namespace QuanLySach.DomainModels
{
    public class Customer : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
