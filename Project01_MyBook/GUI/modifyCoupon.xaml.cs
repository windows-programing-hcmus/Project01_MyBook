using Project01_MyBook.BUS;
using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for modifyCoupon.xaml
    /// </summary>
    public partial class modifyCoupon : Window
    {
        public PromotionDTO _promotion { get; set; }
        public modifyCoupon()
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
        }

        class modifyCounponContext : INotifyPropertyChanged
        {
            public Icons_DTO _icons { get; set; } = new Icons_DTO();
            public PromotionDTO _promotionDTO { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        modifyCounponContext context = new modifyCounponContext();

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
            context._promotionDTO = (PromotionDTO)this._promotion.Clone();
            context._promotionDTO.promoDiscount = (float)Math.Round(context._promotionDTO.promoDiscount * 100, 2);
            this.DataContext = context;
            //this.WindowState = WindowState.Maximized;
        }

        private void cancelBtn(object sender, RoutedEventArgs e)
        {
            // close dialog
            DialogResult = false;
            this.Close();
        }

        private bool check()
        {
            if (String.IsNullOrEmpty(nameCoupon.Text) || String.IsNullOrEmpty(coupon.Text)
                || String.IsNullOrEmpty(description.Text) || !dateStart.SelectedDate.HasValue
                || !dateEnd.SelectedDate.HasValue)
                return false;
            return true;
        }

        private void saveBtn(object sender, RoutedEventArgs e)
        {
            if (!check())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ vào ô cần sửa!!!");
                return;
            }

            DateTime? start = dateStart.SelectedDate;
            DateTime? end = dateEnd.SelectedDate;

            var newCoupon = new PromotionDTO()
            {
                promoID = _promotion.promoID,
                promoName = nameCoupon.Text,
                promoDiscount = (float)(1.0 * float.Parse(coupon.Text) / 100),
                promoDesciption = description.Text,
                promoStartTime = (DateTime)start,
                promoEndTime = (DateTime)end,
                promoStatus = true
            };

            PromotionBUS.UpdatePromotion(newCoupon);
            _promotion = (PromotionDTO)newCoupon.Clone();

            MessageBox.Show("Sửa thành công khuyến mãi!!!");
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
