using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuanLySach.DomainModels.Annotations;

namespace QuanLySach.DomainModels
{
    public class Book : INotifyPropertyChanged, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? CoverImagePath { get; set; }
        public string? Author { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        private Category _category;
        public Category Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return new Book
            {
                Id = this.Id,
                Name = this.Name,
                Author = this.Author,
                CoverImagePath = this.CoverImagePath,
                Price = this.Price,
                PublishedDate = this.PublishedDate,
                Quantity = this.Quantity
            };
        }
    }
}
