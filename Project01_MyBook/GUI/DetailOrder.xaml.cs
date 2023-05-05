using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using Project01_MyBook.BUS;
using Project01_MyBook.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for DetailOrderScreen.xaml
    /// </summary>
    public partial class DetailOrder : Window
    {
        public OrderDTO _detail { get; set; }

        public DetailOrder()
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

        public class OrderDetail
        {
            public string linkImg { get; set; }
            public string bookID { get; set; }
            public string bookName { get; set; }
            public string bookAuthor { get; set; }
            public decimal odCurrentPrice { get; set; }
            public int odQuantity { get; set; }
            public decimal odDiscountedPrice { get; set; }
            public decimal odTempPrice { get; set; }
        }

        Icons_DTO _icons = new Icons_DTO();
        CustomerDTO _customer;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            idOrder.DataContext = _detail;
            total.DataContext = _detail;
            id.DataContext = _detail;

            _customer = CustomerBUS.findCustomerByPhoneNumber(_detail.cusPhoneNumber);
            customer.DataContext = _customer;

            infoOrderBasic.DataContext = _detail;

            var ordersDetail = OrderDetailBUS.findOrderDetailByOrderID(_detail.ordersID);
            var viewModel = new List<OrderDetail>();

            foreach (var order in ordersDetail)
            {
                var temp = new OrderDetail()
                {
                    linkImg = $"/Resource/Images/BookCovers/{order.bookID}.jpg",
                    odCurrentPrice = order.odCurrentPrice,
                    odDiscountedPrice = order.odDiscountedPrice,
                    odQuantity = order.odQuantity,
                    odTempPrice = order.odTempPrice,
                    bookName = BookBUS.findBookByID(order.bookID).bookName,
                    bookAuthor = BookBUS.findBookByID(order.bookID).bookAuthor
                };

                viewModel.Add(temp);
            }

            GridContent.ItemsSource = viewModel;

            this.DataContext = _icons;
            //this.WindowState = WindowState.Maximized;
        }

        private void deleteOrder(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa đơn hàng {_detail.ordersID}", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                OrderBUS.DeleteOrder(_detail.ordersID);

                MessageBox.Show("Xóa thành công");
                DialogResult = true;
            }
            else
            {
            }
        }

        private void exportData_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Tệp PDF (*.pdf)|*.pdf";
            save.FileName = $"Đơn hàng {_detail.ordersID}";

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

                        Font title = new Font(viFont, 25f, Font.BOLD, BaseColor.BLUE);
                        Font header = new Font(viFont, 12f, Font.BOLD, BaseColor.BLACK);
                        Font normal = new Font(viFont, 10f, Font.NORMAL, BaseColor.BLACK);
                        Font underline = new Font(viFont, 10f, Font.UNDERLINE, BaseColor.BLACK);

                        //creating iTextSharp Table from the DataTable data
                        PdfPTable pdfPTable = new PdfPTable(6);
                        int[] intTblWidth = { 7, 25, 8, 20, 20, 20 };

                        pdfPTable.SetWidths(intTblWidth);
                        pdfPTable.WidthPercentage = 100;
                        pdfPTable.DefaultCell.BorderWidth = 1;
                        pdfPTable.HorizontalAlignment = Element.ALIGN_CENTER;

                        //adding header columns
                        PdfPCell pdfPCell = new PdfPCell(new Phrase("STT", header));
                        pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfPTable.AddCell(pdfPCell);

                        PdfPCell pdfPCell2 = new PdfPCell(new Phrase("Sản phẩm", header));
                        pdfPCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfPTable.AddCell(pdfPCell2);

                        PdfPCell pdfPCell3 = new PdfPCell(new Phrase("Số lượng", header));
                        pdfPCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfPTable.AddCell(pdfPCell3);

                        PdfPCell pdfPCell4 = new PdfPCell(new Phrase("Giá tiền", header));
                        pdfPCell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfPTable.AddCell(pdfPCell4);

                        PdfPCell pdfPCell5 = new PdfPCell(new Phrase("Giảm giá", header));
                        pdfPCell5.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfPTable.AddCell(pdfPCell5);

                        PdfPCell pdfPCell6 = new PdfPCell(new Phrase("Tạm tính", header));
                        pdfPCell6.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfPTable.AddCell(pdfPCell6);


                        //adding all rows
                        int i = 1;
                        foreach (var item in GridContent.Items)
                        {
                            OrderDetail order = (OrderDetail)item;

                            PdfPCell pdfPCell7 = new PdfPCell(new Phrase((i++).ToString(), normal));
                            pdfPCell7.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell7);

                            var pdfPCell8 = new iTextSharp.text.Paragraph($"{order.bookName}\n - \t{order.bookAuthor}", normal);
                            pdfPTable.AddCell(pdfPCell8);

                            PdfPCell pdfPCell9 = new PdfPCell(new Phrase(order.odQuantity.ToString(), normal));
                            pdfPCell9.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell9);

                            var currPrice = String.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:c}", (decimal)order.odCurrentPrice);
                            PdfPCell pdfPCell10 = new PdfPCell(new Phrase(currPrice, normal));
                            pdfPCell10.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell10);

                            var disPrice = String.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:c}", (decimal)order.odDiscountedPrice);
                            PdfPCell pdfPCell11 = new PdfPCell(new Phrase(disPrice, normal));
                            pdfPCell11.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell11);

                            var tempPrice = String.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:c}", (decimal)order.odTempPrice);
                            PdfPCell pdfPCell12 = new PdfPCell(new Phrase(tempPrice, normal));
                            pdfPCell12.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfPTable.AddCell(pdfPCell12);
                        }

                        //Exporting to PDF
                        using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                        {
                            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                            PdfWriter.GetInstance(document, fileStream);
                            document.Open();
                            document.AddHeader("Title", $"Chi tiết đơn hàng {_detail.ordersID}");
                            document.AddLanguage("vi-VN");
                            document.AddTitle($"Đơn hàng {_detail.ordersID}");
                            document.AddCreationDate();

                            // write Title
                            document.Add(Chunk.TABBING);
                            document.Add(Chunk.TABBING);
                            document.Add(Chunk.TABBING);
                            document.Add(Chunk.TABBING);

                            document.Add(new Phrase($"Chi tiết đơn hàng {_detail.ordersID}", title));
                            document.Add(Chunk.NEWLINE);
                            document.Add(Chunk.NEWLINE);

                            // write Customer Info
                            document.Add(new iTextSharp.text.Paragraph("Khách hàng:  " +
                                                                       $"{_customer.cusName}", normal));

                            document.Add(new iTextSharp.text.Paragraph("Số điện thoại:  " +
                                                                      $"{_customer.cusPhoneNumber}", normal));

                            document.Add(new iTextSharp.text.Paragraph("Ngày mua hàng:  " +
                                                                      $"{_detail.ordersTime}", normal));

                            document.Add(new iTextSharp.text.Paragraph("Nhân viên:  " +
                                                                      $"{_detail.accUsername}", normal));

                            document.Add(Chunk.NEWLINE);
                            document.Add(new Phrase($"Chi tiết sản phẩm", header));
                            document.Add(Chunk.NEWLINE);


                            // write Order Detail
                            document.Add(pdfPTable);

                            // write Authour
                            document.AddAuthor($"@{_detail.accUsername}");
                            document.AddCreator("@username");

                            // write Created Date
                            document.AddCreationDate();

                            // write number page

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
    }
}