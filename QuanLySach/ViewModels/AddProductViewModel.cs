using System;
using System.Collections.Generic;
using QuanLySach.DomainModels;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using QuanLySach.Annotations;
using QuanLySach.Business;
using QuanLySach.Business.Interfaces;
using QuanLySach.ViewModels;

namespace QuanLySach.Commands
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryLogic _categoryLogic;
        private readonly IProductLogic _productLogic;
        private WindowMode _windowMode;

        public AddProductViewModel(ICategoryLogic categoryLogic, IProductLogic productLogic)
        {
            _categoryLogic = categoryLogic;
            _productLogic = productLogic;
            UploadImageCommand = new ReplayCommand(UploadCoverImage);
            SaveProductCommand = new ReplayCommand(CreateProduct);
        }

        public Book BookInfo { get; set; }
        public ICommand UploadImageCommand { get; set; }
        public ICommand SaveProductCommand { get; set; }
        public List<Category> Categories { get; set; }
        public delegate void CloseCommand();
        public event CloseCommand OnClose;

        public event PropertyChangedEventHandler? PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateProduct()
        {
            try
            {
                if (_windowMode == WindowMode.Add)
                {
                    _productLogic.AddNewProduct(BookInfo);
                }
                else
                {
                    _productLogic.UpdateProduct(BookInfo);
                }

                OnClose.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lưu sản phẩm thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UploadCoverImage()
        {
            try
            {
                var diaglog = new CommonOpenFileDialog();
                diaglog.IsFolderPicker = false;

                if (diaglog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string fileName = diaglog.FileName;
                    string newFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", System.IO.Path.GetFileName(fileName));
                    if (!File.Exists(newFile))
                    {
                        File.Copy(fileName, newFile);
                    }

                    BookInfo.CoverImagePath = "\\" + newFile.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                    OnPropertyChanged("BookInfo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upload hình  thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void InitData(WindowMode mode, Book? book)
        {
            _windowMode = mode;
            if (mode == WindowMode.Add)
            {
                BookInfo = new Book();
                List<Category> categories = await _categoryLogic.GetAllCategories();
                Categories = categories;
            }
            else
            {
                List<Category> categories = await _categoryLogic.GetAllCategories();
                Categories = categories;
                BookInfo = (Book)book.Clone();
                BookInfo.Category = GetCategory(book.Category.Id);
            }

            OnPropertyChanged("BookInfo");
        }

        private Category? GetCategory(int categoryId)
        {
            return Categories.FirstOrDefault(q => q.Id == categoryId);
        }
    }
}
