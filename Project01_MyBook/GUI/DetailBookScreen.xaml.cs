using Project01_MyBook.BUS;
using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    /// Interaction logic for DetailBookScreen.xaml
    /// </summary>
    public partial class DetailBookScreen : Window
    {
        DetailBookScreenContext Context = new DetailBookScreenContext();

        public BookDTO _book { get; set; }

        public bool isDeleted { get; set; } = false;
        public DetailBookScreen(BookDTO book)
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
            Context._book = (BookDTO)book.Clone();
            this._book = (BookDTO)book.Clone();
            Context._book.linkImg = $"/Resource/Images/BookCovers/{Context._book.bookID}.jpg";
        }

        class DetailBookScreenContext
        {
            public Icons_DTO _icons { get; set; } = new Icons_DTO();
            public BookDTO _book { get; set; }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Context;
        }

        private bool check()
        {
            if (String.IsNullOrEmpty(bookIDText.Text) || String.IsNullOrEmpty(bookNameText.Text)
                || String.IsNullOrEmpty(bookAuthorText.Text) || String.IsNullOrEmpty(bookQuantityText.Text)
                || String.IsNullOrEmpty(bookPublishedYearText.Text) || String.IsNullOrEmpty(bookPriceText.Text))
                return false;
            return true;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!check())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ vào ô cần sửa!!!", "Thông báo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            var bookID = bookIDText.Text;
            var bookName = bookNameText.Text;
            var bookAuthor = bookAuthorText.Text;

            //lấy từ màn hình
            var tobID = this._book.tobID;
            decimal bookPrice = 0;
            var bookQuantity = 0;
            var bookPublishedYear = 0;

            try
            {
                bookPrice = Convert.ToDecimal(bookPriceText.Text, new CultureInfo("en-US"));
                bookQuantity = Int32.Parse(bookQuantityText.Text);
                bookPublishedYear = Int32.Parse(bookPublishedYearText.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số!!!", "Lỗi định dạng",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            if (bookPrice <= 0 || bookQuantity < 0 || bookPublishedYear < 0)
            {
                MessageBox.Show("Vui lòng nhập đúng logic!!!", "Lỗi Logic",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var updateBook = new BookDTO()
            {
                bookID = bookID,
                bookName = bookName,
                bookAuthor = bookAuthor,
                bookQuantity = bookQuantity,
                bookPublishedYear = bookPublishedYear,
                bookPrice = bookPrice,
                tobID = tobID,
                linkImg = this._book.linkImg
            };
            if (checkModified(updateBook))
            {
                if (BookBUS.UpdateBook(updateBook))
                {
                    MessageBox.Show("Cập nhật thành công thông tin!!!", "Thông báo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    Context._book = updateBook;
                    this._book = updateBook;
                    DialogResult = true;
                }

            }
            else
            {
                MessageBox.Show("Bạn chưa thay đổi thông tin!!!", "Thông báo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

        }

        private bool checkModified(BookDTO updateBook)
        {


            if (updateBook.bookName.Equals(_book.bookName) &&
                updateBook.bookAuthor.Equals(_book.bookAuthor) &&
                updateBook.bookPrice == _book.bookPrice &&
                updateBook.bookPublishedYear == _book.bookPublishedYear &&
                updateBook.bookQuantity == _book.bookQuantity &&
                updateBook.tobID.Equals(_book.tobID)
                )
            {
                return false;
            }
            return true;
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn muốn xóa sản phẩm này", "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                BookBUS.DeleteBook(this._book.bookID);
                MessageBox.Show("Xóa thành công !!!", "Thông báo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);


                this.isDeleted = true;
                DialogResult = true;
            }

        }
    }
}
