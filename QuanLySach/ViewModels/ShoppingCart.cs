using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using QuanLySach.DomainModels.Annotations;

namespace QuanLySach.ViewModels
{
    public class ShoppingCart : INotifyPropertyChanged
    {
        public BindingList<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
        public int OrderId { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Calculate()
        {
            TotalPrice = Items.Sum(x => x.TotalPrice);
            TotalItems = Items.Count();
            OnPropertyChanged("TotalPrice");
            OnPropertyChanged("TotalItems");
        }
    }
}
