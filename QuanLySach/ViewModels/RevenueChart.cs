using System;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Configurations;
using QuanLySach.DomainModels;

namespace QuanLySach.ViewModels
{
    public class RevenueChart
    {
        public RevenueChart()
        {
            Mapper = Mappers.Xy<KeyValue>()
                .X((keyValue, index) => index)
                .Y(keyValue => keyValue.Value);
            MillionFormatter = value => (value / 1000000).ToString("N") + "M";
        }

        public ChartValues<KeyValue> Results { get; set; }
        public ObservableCollection<string> Labels { get; set; }
        public Func<double, string> MillionFormatter { get; set; }

        public object Mapper { get; set; }
    }
}
