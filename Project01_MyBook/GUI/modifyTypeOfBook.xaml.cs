using Org.BouncyCastle.Crypto.Tls;
using Project01_MyBook.BUS;
using Project01_MyBook.DTO;
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
using System.Windows.Shapes;

namespace Project01_MyBook.GUI
{
    /// <summary>
    /// Interaction logic for modifyTypeOfBook.xaml
    /// </summary>
    public partial class modifyTypeOfBook : Window
    {
        public TypeOfBookDTO _tob { get; set; }
        public modifyTypeOfBook()
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        Icons_DTO _icons = new Icons_DTO();
        BindingList<TypeOfBookDTO> listTypeOfBook = new BindingList<TypeOfBookDTO>(TypeOfBookBUS.findAllTypeOfBook());

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _icons;
            typeOfBook.ItemsSource = listTypeOfBook;
        }

        private void modify(object sender, RoutedEventArgs e)
        {
            var screen = new modifyItem();
            screen._editedType = (TypeOfBookDTO)typeOfBook.SelectedItem;

            if (screen.ShowDialog() == true)
            {
                listTypeOfBook[typeOfBook.SelectedIndex] = screen._editedType;
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var selectedItem = (TypeOfBookDTO)typeOfBook.SelectedItem;

            var result = MessageBox.Show($"Bạn chắc chắn muốn xóa thể loại {selectedItem.tobID}- {selectedItem.tobName}?",
                                "Xác nhận xóa",
                                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (!TypeOfBookBUS.DeleteTypeOfBook(selectedItem.tobID))
                {
                    MessageBox.Show("Xóa không thành công!",
                                    "Xóa thể loại sách",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Đã xóa thành công!",
                                "Xóa thể loại sách",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                typeOfBook.ItemsSource = TypeOfBookBUS.findAllTypeOfBook();
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var newType = new TypeOfBookDTO()
            {
                tobID = idType.Text,
                tobName = nameType.Text
            };

            _tob = newType;

            if (!TypeOfBookBUS.InsertTypeOfBook(newType))
            {
                MessageBox.Show("Vui lòng nhập kỹ thông tin, mã thể loại không được trùng!!!",
                                "Thêm thể loại sách",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Thêm mới thành công!",
                            "Thêm thể loại sách",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            typeOfBook.ItemsSource = TypeOfBookBUS.findAllTypeOfBook();
        }
    }
}