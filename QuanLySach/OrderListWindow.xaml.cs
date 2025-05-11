using QuanLySach.ViewModels;
using System.Windows;
using QuanLySach.DomainModels;

namespace QuanLySach
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        private OrderListViewModel _orderListViewModel;
        public OrderListWindow(OrderListViewModel orderListViewModel)
        {
            _orderListViewModel = orderListViewModel;
            InitializeComponent();
            DataContext = _orderListViewModel;
        }

        private void RemoveOrder_OnClick(object sender, RoutedEventArgs e)
        {
            Order order = ((FrameworkElement)sender).DataContext as Order;
            _orderListViewModel.RemoveOrder(order);
        }

        private void EditOrder_OnClick(object sender, RoutedEventArgs e)
        {
            Order order = ((FrameworkElement)sender).DataContext as Order;
            _orderListViewModel.EditOrder(order);
        }
    }
}
