using QuanLySach.DomainModels;
using QuanLySach.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLySach
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class ShoppingCartWindow : Window
    {
        public WindowMode Mode { get; set; }
        public int? OrderId { get; set; }
        private readonly ShoppingCartViewModel _viewModel;

        public ShoppingCartWindow(ShoppingCartViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnClose += Close;
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void SearchTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            _viewModel.Search(SearchTextBox.Text);
        }

        private void AddToOrder(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;
            var book = (Book)item.DataContext;
            _viewModel.AddItem(book);
        }

        private void IncreaseQuantity_OnClick(object sender, RoutedEventArgs e)
        {
            ShoppingCartItem shoppingCartItem = ((FrameworkElement)sender).DataContext as ShoppingCartItem;
            _viewModel.IncreaseQuantity(shoppingCartItem);
        }

        private void DecreaseQuantity_OnClick(object sender, RoutedEventArgs e)
        {
            ShoppingCartItem shoppingCartItem = ((FrameworkElement)sender).DataContext as ShoppingCartItem;
            _viewModel.DecreaseQuantity(shoppingCartItem);
        }

        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            ShoppingCartItem shoppingCartItem = ((FrameworkElement)sender).DataContext as ShoppingCartItem;
            _viewModel.RemoveItem(shoppingCartItem);
        }

        private void ShoppingCartWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Mode == WindowMode.Add)
            {
                Title = "Bán Hàng";
            }
            else
            {
                Title = "Sửa Đơn Hàng";
            }

            _viewModel.InitData(Mode, OrderId);
        }
    }
}
