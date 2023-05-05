using Project01_MyBook.BUS;
using Project01_MyBook.Config;
using Project01_MyBook.DTO;
using Project01_MyBook.GUI;
using Project01_MyBook.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project01_MyBook.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     

        public class MainWindowContext : INotifyPropertyChanged
        {
            public int soldBooks { get; set; } = 0;
            public int ongoingBooks { get; set; } = 0;
            public int newOrders { get; set; } = 0;

            public string username { get; set; }

            public Icons_DTO _icons { get; set; } = new Icons_DTO();

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        public MainWindow()
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
        }


        MainWindowContext itemMainWindow = new MainWindowContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listDataDated.ItemsSource = AddLinkImg.addLinkstoBook(BookBUS.findTop5());

            itemMainWindow.soldBooks = BookBUS.countBookSold();
            itemMainWindow.ongoingBooks = BookBUS.countBookOnSell();
            itemMainWindow.newOrders = OrderBUS.findAllOrder().Count;

            this.DataContext = itemMainWindow;

 
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void dashboardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var screen = new MainWindow();
            screen.Show();
            this.Close();
        }

      
        private void manageProduct_Click(object sender, RoutedEventArgs e)
        {
            var screen = new manageProducts();
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

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = datePicker.SelectedDate;
            string formatted = "";
            if (selectedDate.HasValue)
            {
                formatted = selectedDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            itemMainWindow.ongoingBooks = BookBUS.findAllBook().Count;
            itemMainWindow.soldBooks = BookBUS.countBookSoldInDate(formatted);
            itemMainWindow.newOrders = OrderBUS.countOrderInDate(formatted);

            selected.Content = selectedDate.Value.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }

        private void signout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn muốn đăng xuất khỏi hệ thống?",
                    "Xác Nhận Đăng Xuất",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                AppConfig.SetValue(AppConfig.Username, "null");

                var screen = new LoginWindow();
                screen.Show();
                this.Close();
            }
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            AppConfig.SetValue("LastScreen", "GUI/mainwindow.xaml");
            this.Close();
        }
    }
}
