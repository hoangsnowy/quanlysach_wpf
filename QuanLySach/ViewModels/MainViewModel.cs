using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuanLySach.Commands;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using LiveCharts;
using QuanLySach.Business.Interfaces;
using QuanLySach.DomainModels;
using System.Linq;
using System.Runtime.CompilerServices;
using QuanLySach.Annotations;

namespace QuanLySach.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IStatisticLogic _statisticLogic;
        private System.Timers.Timer timer;

        public MainViewModel(ImportProductCommand importProductCommand,
            OpenProductCommand openProductCommand,
            OpenShoppingCommand openShoppingCommand,
            OpenOrderListCommand openOrderListCommand,
            OpenSettingCommand openSettingCommand,
            IStatisticLogic statisticLogic)
        {
            ImportProductCommand = importProductCommand;
            OpenProductCommand = openProductCommand;
            OpenShoppingCommand = openShoppingCommand;
            OpenOrderListCommand = openOrderListCommand;
            OpenSettingCommand = openSettingCommand;
            _statisticLogic = statisticLogic;

            LoadDashBoard();
            LoadRevenueChart(ReportTerm.Day);
            LoadTopFiveSaleProduct();
            SetInterval();
        }

        public ICommand ImportProductCommand { get; set; }
        public ICommand OpenProductCommand { get; set; }
        public ICommand OpenShoppingCommand { get; set; }
        public ICommand OpenOrderListCommand { get; set; }
        public ICommand OpenSettingCommand { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalOrdersInWeek { get; set; }
        public RevenueChart RevenueChart { get; set; }
        public List<Book> Books { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetInterval()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 10000;

            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            timer.AutoReset = true;

            // Start the timer
            timer.Enabled = true;
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            LoadDashBoard();
        }

        private void LoadDashBoard()
        {
            DashboardStatistic statistic = _statisticLogic.GetDashboardStatistic();
            TotalOrders = statistic.TotalOrders;
            TotalCustomers = statistic.TotalCustomers;
            TotalOrdersInWeek = statistic.TotalOrdersInWeek;
        }

        private async void LoadRevenueChart(ReportTerm reportTerm)
        {
            RevenueChart = new RevenueChart();
            var revenueValues = await _statisticLogic.GetRevenueValues(reportTerm);
            RevenueChart.Results = new ChartValues<KeyValue>(revenueValues);
            RevenueChart.Labels = new ObservableCollection<string>(revenueValues.Select(x => x.Key));
            OnPropertyChanged("RevenueChart");
        }

        private void  LoadTopFiveSaleProduct()
        {
            var books =  _statisticLogic.GetTopFiveProducts();
            Books = books;
            OnPropertyChanged("Books");
        }

        public void ChangeRevenueChart(string revenueComboboxText)
        {
            var reportTerm = Convert(revenueComboboxText);
            LoadRevenueChart(reportTerm);
        }

        private ReportTerm Convert(string comboboxText)
        {
            switch (comboboxText)
            {
                case "Ngày": return ReportTerm.Day;
                case "Tuần": return ReportTerm.Week;
                case "Tháng": return ReportTerm.Month;
                case "Năm": return ReportTerm.Year;
                default: return ReportTerm.Day;
            }
        }
    }
}
