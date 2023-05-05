using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using Project01_MyBook.BUS;
using Project01_MyBook.DTO;
using Project01_MyBook.Helpers;
using System.Runtime.CompilerServices;

namespace Project01_MyBook.GUI
{
    /// <summary>
    /// Interaction logic for addNewBookScreen.xaml
    /// </summary>
    public partial class addNewBookWindow : Window
    {
        public BookDTO _newBook { get; set; }
        public string _fileName { get; set; } = "";
        public addNewBookWindow()
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
            typeOfBook.ItemsSource = TypeOfBookBUS.findAllTypeOfBook();
            this.DataContext = _icons;
            //this.WindowState = WindowState.Maximized;
        }

        private void save(object sender, RoutedEventArgs e)
        {
            if (!check())
            {
                MessageBox.Show("Vui lòng nhập kỹ thông tin!!!");
                return;
            }

            int sumOfBook = BookBUS.findAllBook().Count;
            string workingDirectory = Environment.CurrentDirectory;
            string prjUri = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string binPath = AppDomain.CurrentDomain.BaseDirectory;
            string des = $"{prjUri}\\Resource\\Images\\BookCovers\\";
            string desBinPath = $"{binPath}\\Resource\\Images\\BookCovers\\";
            var type = (TypeOfBookDTO)typeOfBook.SelectedItem;

            CopyImage.Copy(_fileName, des, "A00" + $"{sumOfBook + 1}" + ".jpg");
            CopyImage.Copy(_fileName, desBinPath, "A00" + $"{sumOfBook + 1}" + ".jpg");


            var book = new BookDTO()
            {
                bookID = $"A00{sumOfBook + 1}",
                bookName = nameBook.Text,
                bookAuthor = authorBook.Text,
                bookPrice = int.Parse(priceBook.Text),
                bookPublishedYear = int.Parse(yearPublishBook.Text),
                bookQuantity = int.Parse(quantityBook.Text),
                linkImg = $"/Resource/Images/BookCovers/A00{sumOfBook + 1}.jpg",
                tobID = type.tobID
            };

            _newBook = book;
            DialogResult = true;
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool check()
        {
            if (String.IsNullOrEmpty(nameBook.Text) || String.IsNullOrEmpty(authorBook.Text)
                || String.IsNullOrEmpty(quantityBook.Text) || String.IsNullOrEmpty(yearPublishBook.Text)
                || String.IsNullOrEmpty(_fileName))
                return false;
            return true;
        }



        private void uploadImg(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Chọn hình ảnh";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                string imgPath = op.FileName;
                _fileName = op.FileName;
                reviewImg.Source = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
            }
        }


        private void QuantityNumericHandler(object sender, TextChangedEventArgs e)
        {
            bool enteredLetter = false;
            Queue<char> text = new Queue<char>();
            foreach (var ch in this.quantityBook.Text)
            {
                if (char.IsDigit(ch))
                {
                    text.Enqueue(ch);
                }
                else
                {
                    enteredLetter = true;
                }
            }

            if (enteredLetter)
            {
                StringBuilder sb = new StringBuilder();
                while (text.Count > 0)
                {
                    sb.Append(text.Dequeue());
                }

                this.quantityBook.Text = sb.ToString();
                this.quantityBook.SelectionStart = this.quantityBook.Text.Length;
            }
        }

        private void priceBookNumericHandler(object sender, TextChangedEventArgs e)
        {
            bool enteredLetter = false;
            Queue<char> text = new Queue<char>();
            foreach (var ch in this.priceBook.Text)
            {
                if (char.IsDigit(ch))
                {
                    text.Enqueue(ch);
                }
                else
                {
                    enteredLetter = true;
                }
            }

            if (enteredLetter)
            {
                StringBuilder sb = new StringBuilder();
                while (text.Count > 0)
                {
                    sb.Append(text.Dequeue());
                }

                this.priceBook.Text = sb.ToString();
                this.priceBook.SelectionStart = this.priceBook.Text.Length;
            }
        }

        private void yearPublishBookNumericHandler(object sender, TextChangedEventArgs e)
        {
            bool enteredLetter = false;
            Queue<char> text = new Queue<char>();
            foreach (var ch in this.yearPublishBook.Text)
            {
                if (char.IsDigit(ch))
                {
                    text.Enqueue(ch);
                }
                else
                {
                    enteredLetter = true;
                }
            }

            if (enteredLetter)
            {
                StringBuilder sb = new StringBuilder();
                while (text.Count > 0)
                {
                    sb.Append(text.Dequeue());
                }

                this.yearPublishBook.Text = sb.ToString();
                this.yearPublishBook.SelectionStart = this.yearPublishBook.Text.Length;
            }
        }
    }
}

