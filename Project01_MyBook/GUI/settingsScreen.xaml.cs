using Project01_MyBook.Config;
using Project01_MyBook.GUI;
using Project01_MyBook;
using Project01_MyBook.DTO;
using Project01_MyBook.Helpers;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for settingsScreen.xaml
    /// </summary>
    public partial class settingsScreen : Window
    {
        public settingsScreen() 
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            AppConfig.SetValue("LastScreen", "GUI/settingsScreen.xaml");
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
        List<int> numOfBookList = new List<int>(new int[] { 5, 10, 15, 25, 50, 100 });
        List<int> numOfList = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 25, 50, 100 });

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _icons;

            numOfBook.ItemsSource = numOfBookList;
            numOfBook.SelectedItem = settingsScreen.getRowPerPageManageBookScreen();

            numOfOrder.ItemsSource = numOfList;
            numOfOrder.SelectedItem = settingsScreen.getRowPerPageManageOrderScreen();

            numOfPromotion.ItemsSource = numOfList;
            numOfPromotion.SelectedItem = settingsScreen.getRowPerPageManageCouponScreen();

   
        }

     
        private static int RowPerPageManageBookScreen = Int32.Parse(AppConfig.GetValue("RowPerPageManageBookScreen"));
        private static int RowPerPageManageCouponScreen = Int32.Parse(AppConfig.GetValue("RowPerPageManageCouponScreen"));
        private static int RowPerPageManageOrderScreen = Int32.Parse(AppConfig.GetValue("RowPerPageManageOrderScreen"));

        public static int getRowPerPageManageBookScreen()
        {
            return RowPerPageManageBookScreen;
        }
        public static int getRowPerPageManageCouponScreen()
        {
            return RowPerPageManageCouponScreen;
        }
        public static int getRowPerPageManageOrderScreen()
        {
            return RowPerPageManageOrderScreen;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (dontRememberMe.IsChecked == true)
                AppConfig.SetValue("OnStart", "login");

            if (StartAtDashboard.IsChecked == true)
                AppConfig.SetValue("OnStart", "main");

            if (StartAtLastScreen.IsChecked == true)
                AppConfig.SetValue("OnStart", "last");

            RowPerPageManageBookScreen = (int)numOfBook.SelectedItem;
            AppConfig.SetValue("RowPerPageManageBookScreen", $"{RowPerPageManageBookScreen}");

            RowPerPageManageOrderScreen = (int)numOfOrder.SelectedItem;
            AppConfig.SetValue("RowPerPageManageOrderScreen", $"{RowPerPageManageOrderScreen}");

            RowPerPageManageCouponScreen = (int)numOfPromotion.SelectedItem;
            AppConfig.SetValue("RowPerPageManageCouponScreen", $"{RowPerPageManageCouponScreen}");

            MessageBox.Show("Cấu hình thành công!!!", "Thông báo",
                MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void statisticRevenue_Click(object sender, RoutedEventArgs e)
        {
            var screen = new analysicRevenue();
            screen.Show();
            this.Close();
        }

        private void managePromotion_Click(object sender, RoutedEventArgs e)
        {
            var screen = new manageCoupon();
            screen.Show();
            this.Close();
        }

        private void manageOrder_Click(object sender, RoutedEventArgs e)
        {
            var screen = new manageOrders();
            screen.Show();
            this.Close();
        }

        private void manageProduct_Click(object sender, RoutedEventArgs e)
        {
            var screen = new manageProducts();
            screen.Show();
            this.Close();
        }

        private void dashboard_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow();
            screen.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}