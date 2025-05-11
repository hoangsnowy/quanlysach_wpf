using ClosedXML.Excel;
using QuanLySach.DAL.Excel.Interfaces;
using QuanLySach.DomainModels;
using System.Globalization;

namespace QuanLySach.DAL.Excel
{
    public class ExcelDataService : IExcelProductDataService
    {
        public Tuple<List<Category>, List<Book>> ProcessFile(string fileName)
        {
            using var workbook = new XLWorkbook(fileName);
            var wsCategories = workbook.Worksheet(1);
            var wsBooks = workbook.Worksheet(2);

            var categories = GetCategories(wsCategories);
            var books = GetBooks(wsBooks);

            return Tuple.Create(categories, books);
        }

        private List<Category> GetCategories(IXLWorksheet ws)
        {
            return ws.RowsUsed()
                     .Skip(1)
                     .Select(row => {
                         int id = row.Cell(1).GetValue<int>();
                         string name = row.Cell(2).GetValue<string>().Trim();
                         return new Category
                         {
                             Id = id,
                             Name = name
                         };
                     })
                     .ToList();
        }

        private List<Book> GetBooks(IXLWorksheet ws)
        {
            var books = new List<Book>();
            foreach (var row in ws.RowsUsed().Skip(1))
            {
                try
                {
                    string name = row.Cell(1).GetValue<string>().Trim();
                    int categoryId = row.Cell(2).GetValue<int>();
                    int quantity = row.Cell(3).GetValue<int>();
                    decimal price = row.Cell(4).GetValue<decimal>();
                    string author = row.Cell(5).GetValue<string>().Trim();
                    string coverImagePath = row.Cell(6).GetValue<string>().Trim();

                    // Parse published date safely
                    DateTime publishedDate;
                    var cell = row.Cell(7);
                    if (cell.DataType == XLDataType.DateTime)
                    {
                        publishedDate = cell.GetDateTime();
                    }
                    else
                    {
                        var dateString = cell.GetString();
                        if (!DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out publishedDate))
                        {
                            publishedDate = DateTime.MinValue; // or handle as needed
                        }
                    }

                    books.Add(new Book
                    {
                        Name = name,
                        Category = new Category { Id = categoryId },
                        Quantity = quantity,
                        Price = price,
                        Author = author,
                        CoverImagePath = coverImagePath,
                        PublishedDate = publishedDate
                    });
                }
                catch (Exception ex)
                {
                    // TODO: log or handle the specific row error
                    continue;
                }
            }
            return books;
        }
    }
}
