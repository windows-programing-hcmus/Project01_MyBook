using Project01_MyBook.DTO;
using Project01_MyBook.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.Win32;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Diagnostics;
using Project01_MyBook.Helpers;
using Project01_MyBook.Config;

namespace Project01_MyBook.GUI
{
    /// <summary>
    /// Interaction logic for manageOrdersScreen.xaml
    /// </summary>
    public partial class manageOrders: Window
    {
        public manageOrders()
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;
        }

        public class manageOrderContext : INotifyPropertyChanged
        {
            public Icons_DTO _icons { get; set; } = new Icons_DTO();
            public int countOrder { get; set; } = 0;

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        manageOrderContext Context = new manageOrderContext();

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            AppConfig.SetValue("LastScreen", "GUI/manageOrders.xaml");
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


        List<OrderDTO> listOrders = OrderBUS.findAllOrder();

        int _totalItems = 0;
        int _currentPage = 1;
        int _totalPages = 0;
        int _rowsPerPage = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userName.Content = App.Username;
       

            this.DataContext = Context;

            _rowsPerPage = settingsScreen.getRowPerPageManageOrderScreen();
            _totalItems = listOrders.Count;
            Context.countOrder = _totalItems;
            _totalPages = _totalItems / _rowsPerPage +
                    (_totalItems % _rowsPerPage == 0 ? 0 : 1);

            currentPagingText.Content = $"{_currentPage}/{_totalPages}";

            orderList.ItemsSource = listOrders.Skip((_currentPage - 1) * _rowsPerPage)
                                    .Take(_rowsPerPage)
                                    .ToList();
        }

        private void signOut_MouseDown(object sender, MouseButtonEventArgs e)
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

       
       
        private void addNewOrder(object sender, RoutedEventArgs e)
        {
            var screen = new addNewOrder();

            if (screen.ShowDialog() == true)
            {
                listOrders = OrderBUS.findAllOrder();
                _rowsPerPage = settingsScreen.getRowPerPageManageOrderScreen();
                _totalItems = listOrders.Count;
                Context.countOrder = _totalItems;
                _totalPages = _totalItems / _rowsPerPage +
                        (_totalItems % _rowsPerPage == 0 ? 0 : 1);

                currentPagingText.Content = $"{_currentPage}/{_totalPages}";

                orderList.ItemsSource = listOrders.Skip((_currentPage - 1) * _rowsPerPage)
                                        .Take(_rowsPerPage)
                                        .ToList();
            }
        }

        private void exportData_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listOrders.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Tệp PDF (*.pdf)|*.pdf";
                save.FileName = "Danh sách đơn hàng";

                bool errMessage = false;
                if (save.ShowDialog() == true)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            errMessage = true;
                            MessageBox.Show("Không thể lưu tập tin vào ổ đĩa " + ex.Message);
                        }
                    }
                    else
                    {
                        //do nothing
                    }

                    if (!errMessage)
                    {
                        try
                        {
                            //define base font for PDF document
                            string vifontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\times.ttf";
                            BaseFont viFont = BaseFont.CreateFont(vifontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                            Font title = new Font(viFont, 20f, Font.NORMAL, BaseColor.BLUE);
                            Font header = new Font(viFont, 12f, Font.BOLD, BaseColor.BLACK);
                            Font normal = new Font(viFont, 10f, Font.NORMAL, BaseColor.BLACK);
                            Font underline = new Font(viFont, 10f, Font.UNDERLINE, BaseColor.BLACK);

                            //creating iTextSharp Table from the DataTable data
                            PdfPTable pdfPTable = new PdfPTable(6);
                            int[] intTblWidth = { 7, 15, 18, 20, 25, 15 };
                            pdfPTable.SetWidths(intTblWidth);
                            pdfPTable.WidthPercentage = 100;
                            pdfPTable.DefaultCell.BorderWidth = 1;
                            pdfPTable.HorizontalAlignment = Element.ALIGN_CENTER;

                            //adding header columns
                            PdfPCell pdfPCell = new PdfPCell(new Phrase("STT", header));
                            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell);

                            PdfPCell pdfPCell2 = new PdfPCell(new Phrase("Mã đơn hàng", header));
                            pdfPCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell2);

                            PdfPCell pdfPCell3 = new PdfPCell(new Phrase("Mã đơn hàng", header));
                            pdfPCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell3);

                            PdfPCell pdfPCell4 = new PdfPCell(new Phrase("Tổng tiền", header));
                            pdfPCell4.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell4);

                            PdfPCell pdfPCell5 = new PdfPCell(new Phrase("Ngày tạo", header));
                            pdfPCell5.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell5);

                            PdfPCell pdfPCell6 = new PdfPCell(new Phrase("Người lập HĐ", header));
                            pdfPCell6.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell6);

                            //pdfPTable.AddCell(new PdfPCell(new Phrase("Mã đơn hàng", header)));

                            // adding all rows
                            int i = 1;
                            foreach (var item in listOrders)
                            {
                                OrderDTO order = (OrderDTO)item;

                                PdfPCell pdfPCell7 = new PdfPCell(new Phrase((i++).ToString(), normal));
                                pdfPCell7.HorizontalAlignment = Element.ALIGN_CENTER;
                                pdfPTable.AddCell(pdfPCell7);

                                PdfPCell pdfPCell8 = new PdfPCell(new Phrase(order.ordersID, normal));
                                pdfPCell8.HorizontalAlignment = Element.ALIGN_CENTER;
                                pdfPTable.AddCell(pdfPCell8);

                                PdfPCell pdfPCell9 = new PdfPCell(new Phrase(order.cusPhoneNumber, normal));
                                pdfPCell9.HorizontalAlignment = Element.ALIGN_CENTER;
                                pdfPTable.AddCell(pdfPCell9);

                                PdfPCell pdfPCell10 = new PdfPCell(new Phrase(order.ordersPrices.ToString(), normal));
                                pdfPCell10.HorizontalAlignment = Element.ALIGN_CENTER;
                                pdfPTable.AddCell(pdfPCell10);

                                PdfPCell pdfPCell11 = new PdfPCell(new Phrase(order.ordersTime.ToString(), normal));
                                pdfPCell11.HorizontalAlignment = Element.ALIGN_CENTER;
                                pdfPTable.AddCell(pdfPCell11);

                                PdfPCell pdfPCell12 = new PdfPCell(new Phrase(order.accUsername, normal));
                                pdfPCell12.HorizontalAlignment = Element.ALIGN_CENTER;
                                pdfPTable.AddCell(pdfPCell12);
                            }

                            //Exporting to PDF
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                                PdfWriter.GetInstance(document, fileStream);

                                document.Open();
                                document.AddHeader("Title", "Danh sách đơn hàng");
                                document.AddLanguage("vi-VN");
                                document.AddTitle("Danh sách đơn hàng");
                                document.AddCreationDate();

                                var titleDoc = new Phrase("DANH SÁCH ĐƠN HÀNG", title);

                                document.Add(titleDoc);

                                document.Add(pdfPTable);
                                document.Close();
                                fileStream.Close();
                            }

                            MessageBox.Show("Xuất dữ liệu thành công!",
                                            "Xuất dữ liệu",
                                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xuất dữ liệu!",
                                            "Xuất dữ liệu",
                                            MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    //do nothing
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy dữ liệu!",
                                "Xuất dữ liệu",
                                MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void nextPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _currentPage++;
            if (_currentPage <= _totalPages)
            {
                currentPagingText.Content = $"{_currentPage}/{_totalPages}";

                orderList.ItemsSource = listOrders.Skip((_currentPage - 1) * _rowsPerPage)
                                        .Take(_rowsPerPage)
                                        .ToList();
            }
            else
            {
                _currentPage--;
            }
        }
        private void previousPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _currentPage--;
            if (_currentPage > 0)
            {
                currentPagingText.Content = $"{_currentPage}/{_totalPages}";

                orderList.ItemsSource = listOrders.Skip((_currentPage - 1) * _rowsPerPage)
                                        .Take(_rowsPerPage)
                                        .ToList();
            }
            else
            {
                _currentPage++;
            }
        }

        private void infoOrder(object sender, MouseButtonEventArgs e)
        {
            if (orderList.SelectedIndex < 0)
                orderList.SelectedIndex = 0;

            var order = orderList.SelectedItem as OrderDTO;
            var screen = new DetailOrder();
            screen._detail = order;

            if (screen.ShowDialog() == true)
            {
                OrderDTO item = (OrderDTO)orderList.SelectedItem;

                listOrders.Remove(item);

                Context.countOrder = listOrders.Count;
                _totalPages = _totalItems / _rowsPerPage +
                    (_totalItems % _rowsPerPage == 0 ? 0 : 1);

                currentPagingText.Content = $"{_currentPage}/{_totalPages}";
                orderList.ItemsSource = listOrders.Skip((_currentPage - 1) * _rowsPerPage)
                                    .Take(_rowsPerPage)
                                    .ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}