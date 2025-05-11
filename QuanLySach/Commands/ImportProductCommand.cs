using Microsoft.WindowsAPICodePack.Dialogs;
using QuanLySach.Business.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace QuanLySach.Commands
{
    public class ImportProductCommand : ICommand
    {
        private readonly IUploadProductLogic _uploadProductLogic;
        public ImportProductCommand(IUploadProductLogic uploadProductLogic)
        {
            _uploadProductLogic = uploadProductLogic;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var diaglog = new CommonOpenFileDialog();
            diaglog.IsFolderPicker = false;

            if (diaglog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    string fileName = diaglog.FileName;
                    _uploadProductLogic.Upload(fileName);
                    MessageBox.Show("Import data thành công", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Import data thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}
