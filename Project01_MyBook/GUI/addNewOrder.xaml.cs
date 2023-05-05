using Project01_MyBook.BUS;
using Project01_MyBook.DTO;
using Project01_MyBook.Helpers;
using Project01_MyBook;
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
    /// Interaction logic for addNewOrder.xaml
    /// </summary>
    public partial class addNewOrder : Window
    {


        public addNewOrder()
        {
            InitializeComponent();
            reDownButton.Visibility = Visibility.Collapsed;

            nameCus_StackPanel.Visibility = Visibility.Collapsed;
            phone_StackPanel.Visibility = Visibility.Collapsed;

        }
        public class addOrderContext : INotifyPropertyChanged
        {
            public Icons_DTO _icons { get; set; } = new Icons_DTO();
            public decimal totalPrice { get; set; } = 0;

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        addOrderContext Context = new addOrderContext();
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

        private string orderID { get; set; }
        private List<BookDTO> allBooks = BookBUS.findAllBook();
        private List<CustomerDTO> allCustomers = CustomerBUS.findAllCustomer();

        public List<string> options { get; set; } = new List<string>() { "Khách ẩn danh", "Khách hàng mới" };


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allBooks = BookBUS.findAllBook();
            listOfBook.ItemsSource = allBooks;

            orderID = CreateOrderID.generatedID();
            orderIDText.Text = orderID;

            foreach (var customer in allCustomers)
                options.Add(customer.cusPhoneNumber);

            listOfCus.ItemsSource = options;

            this.DataContext = Context;
            //this.WindowState = WindowState.Maximized;
        }

        private void save(object sender, RoutedEventArgs e)
        {
            var i = listOfCus.SelectedIndex;
            //Không có thông tin khách hàng
            if (i == 0)
            {
                var newOrder = new OrderDTO()
                {
                    ordersID = this.orderID,
                    cusPhoneNumber = "null",
                    accUsername = App.Username,
                    ordersPrices = Context.totalPrice,
                    ordersTime = DateTime.Now
                };

                var listOrdersDetail = new List<OrderDetailDTO>();
                for (int j = 0; j < orders.Count; j++)
                {
                    listOrdersDetail.Add(new OrderDetailDTO()
                    {
                        ordersID = this.orderID,
                        bookID = orders[j].bookID,
                        odCurrentPrice = orders[j].priceCurrent,
                        odDiscountedPrice = orders[j].priceDiscount,
                        odTempPrice = orders[j].priceTemp,
                        odQuantity = orders[j].amount
                    });
                }
                if (OrderBUS.InsertOrder(newOrder))
                {
                    for (int j = 0; j < listOrdersDetail.Count; j++)
                    {
                        OrderDetailBUS.InsertOrderDetail(listOrdersDetail[j]);
                    }
                    MessageBox.Show("Thêm đơn hàng mới thành công!!!",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm đơn hàng mới không thành công!!!",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            // Khách hàng mới
            else if (i == 1)
            {
                var cusName = nameCustomer.Text;
                var cusPhone = phoneCustomer.Text;
                if (cusName == "" || cusPhone == "")
                {
                    MessageBox.Show("Chưa nhập đủ thông tin khách hàng mới!!!",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                var newCus = new CustomerDTO()
                {
                    cusName = cusName,
                    cusPhoneNumber = cusPhone
                };
                if (CustomerBUS.findCustomerByPhoneNumber(cusPhone) == null)
                {
                    CustomerBUS.InsertCustomer(newCus);
                }
                var newOrder = new OrderDTO()
                {
                    ordersID = this.orderID,
                    cusPhoneNumber = cusPhone,
                    accUsername = App.Username,
                    ordersPrices = Context.totalPrice,
                    ordersTime = DateTime.Now
                };

                var listOrdersDetail = new List<OrderDetailDTO>();
                for (int j = 0; j < orders.Count; j++)
                {
                    listOrdersDetail.Add(new OrderDetailDTO()
                    {
                        ordersID = this.orderID,
                        bookID = orders[j].bookID,
                        odCurrentPrice = orders[j].priceCurrent,
                        odDiscountedPrice = orders[j].priceDiscount,
                        odTempPrice = orders[j].priceTemp,
                        odQuantity = orders[j].amount
                    });
                }
                if (OrderBUS.InsertOrder(newOrder))
                {
                    for (int j = 0; j < listOrdersDetail.Count; j++)
                    {
                        OrderDetailBUS.InsertOrderDetail(listOrdersDetail[j]);
                    }
                    MessageBox.Show("Thêm đơn hàng mới thành công!!!",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm đơn hàng mới không thành công!!!",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            // Khách hàng trong hệ thống
            else
            {
                var newOrder = new OrderDTO()
                {
                    ordersID = this.orderID,
                    cusPhoneNumber = listOfCus.SelectedItem.ToString(),
                    accUsername = App.Username,
                    ordersPrices = Context.totalPrice,
                    ordersTime = DateTime.Now
                };

                var listOrdersDetail = new List<OrderDetailDTO>();
                for (int j = 0; j < orders.Count; j++)
                {
                    listOrdersDetail.Add(new OrderDetailDTO()
                    {
                        ordersID = this.orderID,
                        bookID = orders[j].bookID,
                        odCurrentPrice = orders[j].priceCurrent,
                        odDiscountedPrice = orders[j].priceDiscount,
                        odTempPrice = orders[j].priceTemp,
                        odQuantity = orders[j].amount
                    });
                }
                if (OrderBUS.InsertOrder(newOrder))
                {
                    for (int j = 0; j < listOrdersDetail.Count; j++)
                    {
                        OrderDetailBUS.InsertOrderDetail(listOrdersDetail[j]);
                    }
                    MessageBox.Show("Thêm đơn hàng mới thành công!!!",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm đơn hàng mới không thành công!!!",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            for (int j = 0; j < orders.Count; j++)
            {
                for (int k = 0; k < allBooks.Count; k++)
                {
                    if (allBooks[k].bookID.Equals(orders[j].bookID))
                    {
                        BookBUS.UpdateBook(allBooks[k]);
                    }
                }
            }
            DialogResult = true;
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        class Order : INotifyPropertyChanged
        {
            public string bookID { get; set; }
            public string bookName { get; set; }
            public string bookAuthor { get; set; }
            public decimal bookPrice { get; set; }
            public int amount { get; set; }
            public string promoName { get; set; }
            public decimal priceDiscount { get; set; }
            public decimal priceCurrent { get; set; }
            public decimal priceTemp { get; set; }
            public string linkImg { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        BindingList<Order> orders = new BindingList<Order>();

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            int index = listOfBook.SelectedIndex;

            var book = allBooks[index];
            var amountBook = Int32.Parse(quantityBook.Text);

            if (book.bookQuantity == 0)
            {
                MessageBox.Show("Sách đã hết!!!",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (amountBook <= 0)
            {
                MessageBox.Show("Số lượng sách phải lớn hơn 0!!!",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (book.bookQuantity - amountBook < 0)
            {
                MessageBox.Show("Không đủ số lượng sách!!!",
                        "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                quantityBook.Text = "0";
                return;
            }
            else
            {
                allBooks[index].bookQuantity -= amountBook;
            }

            var promoForBook = PromotionBUS.findBestPromotion(book.tobID);
            var order = new Order();
            if (promoForBook != null)
            {
                order = new Order()
                {
                    bookID = book.bookID,
                    bookName = book.bookName,
                    bookAuthor = book.bookAuthor,
                    bookPrice = book.bookPrice,
                    amount = Int32.Parse(quantityBook.Text),
                    promoName = promoForBook.promoName,
                    linkImg = $"/Resource/Images/BookCovers/{book.bookID}.jpg",
                    priceCurrent = book.bookPrice,
                    priceDiscount = -(decimal)promoForBook.promoDiscount / 100 * book.bookPrice,
                    priceTemp = book.bookPrice - (decimal)promoForBook.promoDiscount / 100 * book.bookPrice
                };
            }
            else
            {
                order = new Order()
                {
                    bookID = book.bookID,
                    bookName = book.bookName,
                    bookAuthor = book.bookAuthor,
                    bookPrice = book.bookPrice,
                    amount = Int32.Parse(quantityBook.Text),
                    promoName = "",
                    linkImg = $"/Resource/Images/BookCovers/{book.bookID}.jpg",
                    priceDiscount = 0,
                    priceCurrent = book.bookPrice,
                    priceTemp = book.bookPrice
                };
            }
            int indexOfOrder = orders.ToList().FindIndex(item => order.bookName == item.bookName);
            if (indexOfOrder != -1)
            {
                orders[indexOfOrder].amount += Int32.Parse(quantityBook.Text);
                if (promoForBook != null)
                {
                    orders[indexOfOrder].priceDiscount = ((decimal)promoForBook.promoDiscount / 100 * book.bookPrice) * orders[indexOfOrder].amount;
                    orders[indexOfOrder].priceCurrent = (book.bookPrice - (decimal)promoForBook.promoDiscount / 100 * book.bookPrice) * orders[indexOfOrder].amount;
                }
                else
                {
                    orders[indexOfOrder].priceTemp = book.bookPrice * orders[indexOfOrder].amount;
                }
            }
            else
            {
                if (promoForBook != null)
                {
                    order.priceDiscount = ((decimal)promoForBook.promoDiscount / 100 * book.bookPrice) * order.amount;
                    order.priceTemp = (book.bookPrice - (decimal)promoForBook.promoDiscount / 100 * book.bookPrice) * order.amount;
                }
                else
                {
                    order.priceTemp = book.bookPrice * order.amount;
                }
                orders.Add(order);
            }

            Context.totalPrice += order.priceTemp;

            listBookOrder.ItemsSource = orders;
        }

        private void deleteItem(object sender, RoutedEventArgs e)
        {
            var deleteBook = orders[listBookOrder.SelectedIndex];
            this.Context.totalPrice -= deleteBook.priceCurrent;

            int indexOfDeleteBook = allBooks.FindIndex(item => deleteBook.bookName == item.bookName);
            allBooks[indexOfDeleteBook].bookQuantity += deleteBook.amount;

            orders.RemoveAt(listBookOrder.SelectedIndex);
        }

        private void QuantityNumericHandler(object sender, TextChangedEventArgs e)
        {
            bool enteredLetter = false;
            Queue<char> text = new Queue<char>();
            foreach (var ch in this.quantityBook.Text)
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

                this.quantityBook.Text = sb.ToString();
                this.quantityBook.SelectionStart = this.quantityBook.Text.Length;
            }
        }

        private void QuantityNumericHandlerPhoneNumber(object sender, TextChangedEventArgs e)
        {
            bool enteredLetter = false;
            Queue<char> text = new Queue<char>();
            foreach (var ch in this.phoneCustomer.Text)
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

                this.phoneCustomer.Text = sb.ToString();
                this.phoneCustomer.SelectionStart = this.phoneCustomer.Text.Length;
            }
        }

        private void listOfCus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listOfCus.SelectedIndex == 1)
            {
                nameCustomer.Text = "";
                nameCus_StackPanel.Visibility = Visibility.Visible;
                phone_StackPanel.Visibility = Visibility.Visible;
            }
            else if (listOfCus.SelectedIndex == 0)
            {
                nameCus_StackPanel.Visibility = Visibility.Collapsed;
                phone_StackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                phone_StackPanel.Visibility = Visibility.Collapsed;

                nameCustomer.Text = allCustomers[listOfCus.SelectedIndex - 2].cusName;
                nameCus_StackPanel.Visibility = Visibility.Visible;
            }
        }

        private void listOfBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (allBooks[listOfBook.SelectedIndex].bookQuantity == 0)
            {
                quantityBook.Text = "0";
            }
            else
            {
                quantityBook.Text = "1";
            }
        }
    }
}
