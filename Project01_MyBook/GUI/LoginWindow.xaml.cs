using Project01_MyBook.BUS;
using Project01_MyBook.Config;
using Project01_MyBook.DTO;
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
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Project01_MyBook.GUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public int countWrongPass { get; set; } = 0;

        public LoginWindow()
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
         
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            AppConfig.SetValue("LastScreen", "GUI/loginWindow.xaml");
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
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = (userTextBox.Text).Trim();

            var rawpassword = passTextBox.Password;

            var check = Account_BUS.findUser(username, rawpassword);

            if (check == false)
            {
                this.countWrongPass += 1;
                if (this.countWrongPass < 3)
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu sai!",
                        "Lỗi đăng nhập",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Bạn đã nhập sai {this.countWrongPass} lần !!!",
                        "Lỗi đăng nhập",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                this.countWrongPass = 0;

                App.Username = username;
                AppConfig.SetValue(AppConfig.Username, username);

                var screen = new MainWindow();
                screen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
               
               
                screen.Show();
                this.Close();
            }

        }

      

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var username = (userTextBox.Text).Trim();

                var rawpassword = passTextBox.Password;

                var check = Account_BUS.findUser(username, rawpassword);

                if (check == false)
                {
                    this.countWrongPass += 1;
                    if (this.countWrongPass < 3)
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu sai!",
                            "Lỗi đăng nhập",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Bạn đã nhập sai {this.countWrongPass} lần !!!",
                            "Lỗi đăng nhập",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                else
                {
                    this.countWrongPass = 0;

                    App.Username = username;
                    AppConfig.SetValue("Username", username);

                    var screen = new MainWindow();
                    screen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    screen.Show();
                    this.Close();
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void userTxb_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(userTextBox.Text) && userTextBox.Text.Length > 0)
                userTextBlock.Visibility = Visibility.Collapsed;
            else
                userTextBlock.Visibility = Visibility.Visible;
        }

        private void userTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            userTextBox.Focus();
        }

        private void passwordTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passTextBox.Focus();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passTextBox.Password) && passTextBox.Password.Length > 0)
               passwordTextBlock.Visibility = Visibility.Collapsed;
            else
               passwordTextBlock.Visibility = Visibility.Visible;
        }
    }
}
