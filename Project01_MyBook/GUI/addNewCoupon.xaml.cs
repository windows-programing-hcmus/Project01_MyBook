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
    /// Interaction logic for addNewCoupon.xaml
    /// </summary>
    public partial class addNewCoupon : Window
    {
        public PromotionDTO _promotion { get; set; }
        public addNewCoupon()
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
            typeOfBookCombobox.ItemsSource = TypeOfBookBUS.findAllTypeOfBook();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // close dialog
            this.Close();
        }

        private bool check()
        {
            if (String.IsNullOrEmpty(idCoupon.Text) || String.IsNullOrEmpty(nameCoupon.Text)
               || String.IsNullOrEmpty(coupon.Text) || String.IsNullOrEmpty(description.Text)
               || !dateStart.SelectedDate.HasValue || !dateEnd.SelectedDate.HasValue)
                return false;
            return true;
        }

        private void addNewBtn(object sender, RoutedEventArgs e)
        {
            if (!check())
            {
                MessageBox.Show("Vui lòng nhập kỹ thông tin!!!");
                return;
            }



            DateTime? start = dateStart.SelectedDate;
            DateTime? end = dateEnd.SelectedDate;

            var newCoupon = new PromotionDTO()
            {
                promoID = idCoupon.Text,
                promoName = nameCoupon.Text,
                promoDiscount = (float)(1.0 * float.Parse(coupon.Text) / 100),
                promoDesciption = description.Text,
                promoStartTime = (DateTime)start,
                promoEndTime = (DateTime)end,
                promoStatus = true
            };

            _promotion = newCoupon;

            if (!PromotionBUS.InsertPromotion(newCoupon))
            {
                MessageBox.Show("Vui lòng nhập kỹ thông tin!!!");
                return;
            }

            foreach (TypeOfBookDTO item in typeOfBookCombobox.SelectedItems)
            {
                var temp = new PromotionDetailDTO()
                {
                    promoID = idCoupon.Text,
                    tobID = item.tobID,
                };
                PromotionDetailBUS.InsertPromotionDetail(temp);
            }

            MessageBox.Show("Thêm thành công khuyến mãi mới!!!");
            DialogResult = true;
        }

        private void coupon_SelectionChanged(object sender, RoutedEventArgs e)
        {
            bool enteredLetter = false;
            Queue<char> text = new Queue<char>();
            foreach (var ch in this.coupon.Text)
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

                this.coupon.Text = sb.ToString();
                this.coupon.SelectionStart = this.coupon.Text.Length;
            }
        }
    }
}
