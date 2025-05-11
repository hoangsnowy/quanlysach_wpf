using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuanLySach.DomainModels.Annotations;

namespace QuanLySach.DomainModels
{
    public class Category : INotifyPropertyChanged, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return new Category
            {
                Id = Id,
                Name = Name
            };
        }
    }
}
