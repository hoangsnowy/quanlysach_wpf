using QuanLySach.DomainModels;
using QuanLySach.DomainModels.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuanLySach.ViewModels
{
    public class ShoppingCartItem : INotifyPropertyChanged
    {
        public Book Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderDetailId { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Calculate()
        {
            TotalPrice = Quantity * UnitPrice;
            OnAllPropertiesChanged();
        }

        private void OnAllPropertiesChanged()
        {
            OnPropertyChanged("Quantity");
            OnPropertyChanged("UnitPrice");
            OnPropertyChanged("TotalPrice");
        }
    }
}
