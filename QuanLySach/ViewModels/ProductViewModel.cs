using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using QuanLySach.Business;
using QuanLySach.Business.Interfaces;
using QuanLySach.Commands;
using QuanLySach.DomainModels;

namespace QuanLySach.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly IProductLogic _productLogic;
        private readonly ICategoryLogic _categoryLogic;
        private readonly IServiceProvider _provider;
        private readonly QuanLySach.Settings.ApplicationSetting _applicationSetting;

        public ProductViewModel(IProductLogic productLogic, 
            ICategoryLogic categoryLogic, 
            IServiceProvider provider,
            QuanLySach.Settings.ApplicationSetting applicationSetting)
        {
            _productLogic = productLogic;
            _categoryLogic = categoryLogic;
            _provider = provider;
            _applicationSetting = applicationSetting;
            PreviousButtonClickCommand = new ReplayCommand(PreviousButtonClick);
            NextButtonClickCommand = new ReplayCommand(NextButtonClick);
            SelectionChangedCommand = new ReplayCommand(CategorySelectionChanged);
            InitData();
        }

        public PagingCollectionView View { get; set; }
        public ICommand PreviousButtonClickCommand { get; set; }
        public ICommand NextButtonClickCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public List<Category> Categories { get; set; }
        public Category? SelectedCategory { get; set; }

        public async void Search(string keyword)
        {
            List<Book> books = await _productLogic.Search(SelectedCategory?.Id, keyword);
            View = new PagingCollectionView(books, _applicationSetting.NumberOfProductsPerPage);
        }

        public async void DeleteProduct(Book book)
        {
            try
            {
                bool success = await _productLogic.Delete(book);
                if (success)
                {
                    LoadProduct();
                    MessageBox.Show($"Xóa sản phẩm '{book.Name}' thành công", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Xóa sản phẩm thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddProduct()
        {
            var productWindow = _provider.GetRequiredService<AddProductWindow>();
            productWindow.Mode = WindowMode.Add;
            productWindow.Book = new Book();
            productWindow.ShowDialog();
        }

        public void EditProduct(Book book)
        {
            var productWindow = _provider.GetRequiredService<AddProductWindow>();
            productWindow.Mode = WindowMode.Edit;
            productWindow.Book = book;
            productWindow.ShowDialog();
        }

        private async void InitData()
        {
            List<Category> categories = await _categoryLogic.GetAllCategories();
            Categories = categories;
            SelectedCategory = categories.FirstOrDefault();
            LoadProduct();
        }

        private void PreviousButtonClick()
        {
            View.MoveToPreviousPage();
        }

        private void NextButtonClick()
        {
            View.MoveToNextPage();
        }

        private void CategorySelectionChanged()
        {
            LoadProduct();
        }

        private async void LoadProduct()
        {
            List<Book> books = await _productLogic.GetProductsByCategoryId(SelectedCategory?.Id);
            View = new PagingCollectionView(books, _applicationSetting.NumberOfProductsPerPage);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
