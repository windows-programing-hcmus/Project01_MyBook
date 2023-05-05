using Project01_MyBook.BUS;
using Project01_MyBook.Config;
using Project01_MyBook.DTO;
using Project01_MyBook.Helpers;
using System;
using System.Collections.Generic;
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

namespace Project01_MyBook.GUI
{
    /// <summary>
    /// Interaction logic for BestSelling.xaml
    /// </summary>
    public partial class BestSelling : Window
    {
        public BestSelling()
        {
            InitializeComponent();
            statisticsDatePicker.SelectedDate = selectedDate;


            timeCombobox.ItemsSource = figureValues;
            timeCombobox.SelectedIndex = figureIndex;

            books = _bookBUS.getBestSellingInWeek(selectedDate);

            for (int i = 0; i < books.Count(); i++)
            {
                System.Diagnostics.Debug.WriteLine(books[i].bookName);
            }

            listDataDated.ItemsSource = books;

            DataContext = this;
        }


        public DateTime selectedDate { get; set; } = DateTime.Now;
        public int figureIndex { get; set; } = 0;
        public List<string> figureValues = new List<string>() { "In Week", "In Month", "In Year" };

        public List<BestSellingBook> books;
        BookBUS _bookBUS = new BookBUS();

        public void configureBestSellingListView()
        {
            switch (figureIndex)
            {
                case 0:
                    books = _bookBUS.getBestSellingInWeek(selectedDate);
                    listDataDated.ItemsSource = books;
                    break;
                case 1:
                    books = _bookBUS.getBestSellingInMonth(selectedDate);
                    listDataDated.ItemsSource = books;
                    break;
                case 2:
                    books = _bookBUS.getBestSellingInYear(selectedDate);
                    listDataDated.ItemsSource = books;
                    break;

                default:
                    books = _bookBUS.getBestSellingInWeek(selectedDate);
                    listDataDated.ItemsSource = books;
                    break;
            }
        }

        private void timeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            configureBestSellingListView();
        }

        private void statisticsDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            configureBestSellingListView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new analysicRevenue();
            screen.Show();
            this.Close();   
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var screen = new saleAnalysic();
            screen.Show();
            this.Close();
        }
       

        private void manageOrder_Click(object sender, RoutedEventArgs e)
        {
            var screen = new manageOrders();
            screen.Show();
            this.Close();
        }

        private void managePromotion_Click(object sender, RoutedEventArgs e)
        {
            var screen = new manageCoupon();
            screen.Show();
            this.Close();
        }

        private void statisticRevenue_Click(object sender, RoutedEventArgs e)
        {
            var screen = new analysicRevenue();
            screen.Show();
            this.Close();
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            var screen = new settingsScreen();
            screen.Show();
            this.Close();
        }

        private void signout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn muốn đăng xuất khỏi hệ thống?",
                    "Xác Nhận Đăng Xuất",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                var screen = new LoginWindow();
                screen.Show();
                this.Close();
            }
        }


        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            AppConfig.SetValue("LastScreen", "GUI/bestselling.xaml");
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

        
    }
}
