using QuanLySach.Commands;
using System.Windows;
using QuanLySach.DomainModels;
using QuanLySach.ViewModels;

namespace QuanLySach
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public WindowMode Mode { get; set; }
        public Book? Book { get; set; }
        private AddProductViewModel _viewModel;

        public AddProductWindow(AddProductViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnClose += Close;
            InitializeComponent();
            DataContext = viewModel;
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddProductWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Mode == WindowMode.Add)
            {
                Title = "Thêm Sản Phẩm";
            }
            else
            {
                Title = "Sửa Sản Phẩm";
            }

            _viewModel.InitData(Mode, Book);
        }
    }
}
