using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using QuanLySach.Annotations;
using QuanLySach.Business.Interfaces;
using QuanLySach.Commands;
using QuanLySach.DomainModels;

namespace QuanLySach.ViewModels
{
    public class OrderListViewModel : INotifyPropertyChanged
    {
        private readonly IOrderLogic _orderLogic;
        private readonly IServiceProvider _provider;
        private readonly QuanLySach.Settings.ApplicationSetting _applicationSetting;
        public OrderListViewModel(IOrderLogic orderLogic, IServiceProvider provider, QuanLySach.Settings.ApplicationSetting applicationSetting)
        {
            _orderLogic = orderLogic;
            _provider = provider;
            _applicationSetting = applicationSetting;
            PreviousButtonClickCommand = new ReplayCommand(PreviousButtonClick);
            NextButtonClickCommand = new ReplayCommand(NextButtonClick);
            InitData();
        }

        private DateTime? from;
        private DateTime? to;
        public DateTime? From
        {
            get { return from; }
            set
            {
                from = value;
                LoadOrders();
            }
        }

        public DateTime? To
        {
            get { return to; }
            set
            {
                to = value;
                LoadOrders();
            }
        }

        public PagingCollectionView View { get; set; }
        public ICommand PreviousButtonClickCommand { get; set; }
        public ICommand NextButtonClickCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void InitData()
        {
            LoadOrders();
        }

        private async void LoadOrders()
        {
            List<Order> orders = await _orderLogic.GetOrders(From, To);
            View = new PagingCollectionView(orders, _applicationSetting.NumberOfOrdersPerPage);
        }

        public async void RemoveOrder(Order order)
        {
            try
            {
                await _orderLogic.Remove(order);
                LoadOrders();
                MessageBox.Show($"Xóa đơn hàng thành công", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Xóa  đơn hàng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PreviousButtonClick()
        {
            View.MoveToPreviousPage();
        }

        private void NextButtonClick()
        {
            View.MoveToNextPage();
        }

        public void EditOrder(Order order)
        {
            var shoppingCartWindow = _provider.GetRequiredService<ShoppingCartWindow>();
            shoppingCartWindow.Mode = WindowMode.Edit;
            shoppingCartWindow.OrderId = order.Id;
            shoppingCartWindow.ShowDialog();
        }
    }
}
