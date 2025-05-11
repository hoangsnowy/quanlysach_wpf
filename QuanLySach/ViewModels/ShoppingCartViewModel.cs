using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using QuanLySach.Annotations;
using QuanLySach.Business.Interfaces;
using QuanLySach.Commands;
using QuanLySach.Common.Exceptions;
using QuanLySach.DomainModels;

namespace QuanLySach.ViewModels
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        private readonly IProductLogic _productLogic;
        private readonly IOrderLogic _orderLogic;
        private WindowMode _windowMode;

        public delegate void CloseCommand();
        public event CloseCommand OnClose;

        public ShoppingCartViewModel(IProductLogic productLogic, IOrderLogic orderLogic)
        {
            _productLogic = productLogic;
            _orderLogic = orderLogic;
            SaveOrderCommand = new ReplayCommand(SaveOrder);
        }

        public List<Book> Products { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public Customer Customer { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand SaveOrderCommand { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void InitData(WindowMode mode, int? orderId)
        {
            _windowMode = mode;
            LoadProduct();

            if (mode == WindowMode.Add)
            {
                ShoppingCart = new ShoppingCart();
                ShoppingCart.Items = new BindingList<ShoppingCartItem>();
                Customer = new Customer();
            }
            else
            {
                Order order = await _orderLogic.GetOrder(orderId.Value);
                ShoppingCart = GetShoppingCartFromOrder(order);
                ShoppingCart.Calculate();
                Customer = new Customer()
                {
                    Address = order.Customer.Address,
                    DateOfBirth = order.Customer.DateOfBirth,
                    Email = order.Customer.Email,
                    FullName = order.Customer.FullName,
                    Id = order.Customer.Id,
                    PhoneNumber = order.Customer.PhoneNumber,
                    IdentityCardNumber = order.Customer.IdentityCardNumber
                };
            }

            OnPropertyChanged("ShoppingCart");
            OnPropertyChanged("Customer");
        }

        private async void LoadProduct()
        {
            List<Book> books = await _productLogic.Search(null, string.Empty);
            Products = books;
        }

        public async void Search(string keyword)
        {
            List<Book> books = await _productLogic.Search(null, keyword);
            Products = books;
        }

        public void IncreaseQuantity(ShoppingCartItem item)
        {
            item.Quantity += 1;
            item.Calculate();
            ShoppingCart.Calculate();
            OnPropertyChanged("ShoppingCart");
        }

        public void DecreaseQuantity(ShoppingCartItem item)
        {
            item.Quantity -= 1;
            if (item.Quantity == 0)
            {
                ShoppingCart.Items.Remove(item);
            }
            else
            {
                item.Calculate();
            }

            ShoppingCart.Calculate();
            OnPropertyChanged("ShoppingCart");
        }

        public void AddItem(Book book)
        {
            var item = ShoppingCart.Items.FirstOrDefault(q => q.Product?.Id == book.Id);
            if (item == null)
            {
                item = new ShoppingCartItem()
                {
                    Product = book,
                    Quantity = 1,
                    UnitPrice = book.Price
                };
                ShoppingCart.Items.Add(item);
            }
            else
            {
                item.Quantity += 1;
            }

            item.Calculate();
            ShoppingCart.Calculate();
            OnPropertyChanged("ShoppingCart");
        }

        public void RemoveItem(ShoppingCartItem item)
        {
            ShoppingCart.Items.Remove(item);
            ShoppingCart.Calculate();
            OnPropertyChanged("ShoppingCart");
        }

        private async void SaveOrder()
        {
            try
            {
                Order order = GetOrderFromShoppingCard();
                await _orderLogic.SaveOrder(order);
                OnClose.Invoke();
            }
            catch (OrderValidationException e)
            {
                MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Lưu đơn hàng thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ShoppingCart GetShoppingCartFromOrder(Order order)
        {
            var shoppingCart = new ShoppingCart()
            {
                OrderId = order.Id,
                TotalPrice = order.TotalPrice
            };

            shoppingCart.Items = new BindingList<ShoppingCartItem>();
            order.OrderDetails.ForEach(q =>
                shoppingCart.Items.Add(new ShoppingCartItem()
                {
                    TotalPrice = q.TotalPrice,
                    Quantity = q.Quantity,
                    UnitPrice = q.UnitPrice,
                    Product = q.Book
                }));
            return shoppingCart;
        }

        private Order GetOrderFromShoppingCard()
        {
            var order = new Order
            {
                Id = ShoppingCart.OrderId,
                OrderTime = DateTime.Now,
                Customer = Customer,
                TotalPrice = ShoppingCart.TotalPrice,
                OrderDetails = ShoppingCart.Items.Select(x => new OrderDetail
                {
                    TotalPrice = x.TotalPrice,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Book = x.Product
                }).ToList()
            };

            return order;
        }
    }
}
