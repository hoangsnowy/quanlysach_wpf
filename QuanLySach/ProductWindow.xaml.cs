using QuanLySach.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLySach.DomainModels;

namespace QuanLySach
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private ProductViewModel _viewModel;
        public ProductWindow(ProductViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void DeleteMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;
            var book = (Book)item.DataContext;
            _viewModel.DeleteProduct(book);
        }

        private void EditMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;
            var book = (Book)item.DataContext;
            _viewModel.EditProduct(book);
        }

        private void AddMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.AddProduct();
        }

        private void SearchTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            _viewModel.Search(SearchTextBox.Text);
        }
    }
}
