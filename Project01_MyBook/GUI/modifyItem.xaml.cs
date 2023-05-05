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
    /// Interaction logic for modifyItem.xaml
    /// </summary>
    public partial class modifyItem : Window
    {
        public TypeOfBookDTO _editedType { get; set; }
        public modifyItem()
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
        }

        class modifyItemContext : INotifyPropertyChanged
        {
            public Icons_DTO _icons { get; set; } = new Icons_DTO();
            public TypeOfBookDTO _tobDTO { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        modifyItemContext context = new modifyItemContext();

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
            context._tobDTO = this._editedType;
            this.DataContext = context;
            //this.WindowState = WindowState.Maximized; 
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var newType = new TypeOfBookDTO()
            {
                tobID = _editedType.tobID,
                tobName = nameType.Text
            };

            if (!TypeOfBookBUS.UpdateTypeOfBook(newType))
            {
                MessageBox.Show("Cập nhật không thành công!",
                                "Cập nhập thể loại sách",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _editedType = newType;

       

            DialogResult = true;
        }
    }
}
