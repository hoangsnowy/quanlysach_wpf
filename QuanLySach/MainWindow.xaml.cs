using QuanLySach.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLySach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)RevenueCombobox.SelectedItem;
            string value = typeItem.Content.ToString();
            _viewModel.ChangeRevenueChart(value);
        }
    }
}
