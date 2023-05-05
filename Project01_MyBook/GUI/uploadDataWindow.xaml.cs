using Aspose.Cells;
using Microsoft.Win32;
using Project01_MyBook.BUS;
using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project01_MyBook.GUI
{
    /// <summary>
    /// Interaction logic for uploadDataScreen.xaml
    /// </summary>
    public partial class uploadDataWindow : Window
    {
        private List<BookDTO> _books = new List<BookDTO>();
        public uploadDataWindow()
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maxButton_Click(object sender, RoutedEventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Normal:
                    reDownButton.Visibility = Visibility.Visible;
                    maxButton.Visibility = Visibility.Collapsed;
                    this.WindowState = WindowState.Maximized;
                    break;
                case WindowState.Maximized:
                    maxButton.Visibility = Visibility.Visible;
                    reDownButton.Visibility = Visibility.Collapsed;
                    this.WindowState = WindowState.Normal;
                    break;
            }
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        Icons_DTO _icons = new Icons_DTO();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _icons;
            //this.WindowState = WindowState.Maximized;
        }

        private void loadDataFromExcel()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var screen = new OpenFileDialog();

            if (true == screen.ShowDialog())
            {
                string fileName = screen.FileName;
                Debug.WriteLine(fileName);
                Workbook wb = new Workbook(fileName);


                int row = 2;
                char column = 'B';
                Cell cell = wb.Worksheets[0].Cells[$"{column}{row}"];

                while (cell.Value != null)
                {
                    var tempBook = new BookDTO()
                    {
                        bookID = wb.Worksheets[0].Cells[$"B{row}"].StringValue,
                        bookName = wb.Worksheets[0].Cells[$"C{row}"].StringValue,
                        bookAuthor = wb.Worksheets[0].Cells[$"D{row}"].StringValue,
                        tobID = wb.Worksheets[0].Cells[$"E{row}"].StringValue,
                        bookPrice = int.Parse(wb.Worksheets[0].Cells[$"F{row}"].StringValue),
                        bookQuantity = int.Parse(wb.Worksheets[0].Cells[$"G{row}"].StringValue),
                        bookPublishedYear = int.Parse(wb.Worksheets[0].Cells[$"H{row}"].StringValue),
                    };

                    _books.Add(tempBook);
                    row++;
                    cell = wb.Worksheets[0].Cells[$"{column}{row}"];
                }

            }
        }
        private void uploadData(object sender, RoutedEventArgs e)
        {
            loadDataFromExcel();
            Debug.WriteLine(_books.Count);
            reviewData.ItemsSource = _books;
        }

        private void save(object sender, RoutedEventArgs e)
        {
            if (_books.Count == 0)
            {
                MessageBox.Show("Vui lòng tải dữ liệu lên!!!");
            }
            else
            {
                foreach (var book in _books)
                {
                    BookBUS.InsertBook(book);
                }
                reviewData.ItemsSource = null;
                _books.Clear();
                MessageBox.Show("Đã thêm thành công!");
            }
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            reviewData.ItemsSource = null;
            _books.Clear();
        }
    }
}
