using LiveCharts.Wpf;
using LiveCharts;
using Project01_MyBook.BUS;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using Project01_MyBook.Config;
using Project01_MyBook.DTO;

namespace Project01_MyBook.GUI
{
    /// <summary>
    /// Interaction logic for analysicRevenue.xaml
    /// </summary>
    public partial class saleAnalysic : Window
    {
        public saleAnalysic()
        {
            InitializeComponent();

            _statisticsBUS = new Statistics_BUS();
            _bookBUS = new BookBUS();

            categories = TypeOfBookBUS.findAllTypeOfBook();
            categoriesCombobox.ItemsSource = categories;

            if (categories.Count() > 0)
                books = BookBUS.findBookByTypeOfBook(categories[categoriesFigureIndex].tobID);
            else
                books = new List<BookDTO> { };

            productCombobox.ItemsSource = books;

            bargraphCombobox.ItemsSource = figureValues;
            bargraphCombobox.SelectedIndex = bargraphFigureIndex;

            statisticsDatePicker.SelectedDate = selectedDate;

            chartTabControl.SelectedIndex = tabSelectedIndex;

            DataContext = this;
        }
        Icons_DTO _icons = new Icons_DTO();


        private Statistics_BUS _statisticsBUS;
        private TypeOfBookBUS _categoryBUS;
        private BookBUS _bookBUS;
        public int statisticsFigureIndex { get; set; } = 1;
        public int bargraphFigureIndex { get; set; } = 0;
        public int tabSelectedIndex { get; set; } = 0;
        public int categoriesFigureIndex { get; set; } = 0;
        public int productFigureIndex { get; set; } = 0;
        public DateTime selectedDate { get; set; } = DateTime.Now;
        public List<string> figureValues = new List<string>() { "Daily", "Weekly", "Monthly", "Yearly" };
        public List<string> statisticsFigureValues = new List<string>() { "General", "Specific", "Advanced" };
        public List<TypeOfBookDTO> categories;
        public List<BookDTO> books;
        Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);


        public void configureBarGraphs()
        {
            switch (bargraphFigureIndex)
            {
                case 0:
                    if (books.Count() > 0 && categories.Count() > 0)
                    {
                        var productResult = _statisticsBUS.getDailyQuantityOfSpecificProduct(books[productFigureIndex].bookID, categories[categoriesFigureIndex].tobID, selectedDate);

                        var quantity = new ChartValues<int>();
                        var dates = new List<string>();

                        foreach (var item in productResult)
                        {
                            quantity.Add((int)item.Item2);
                            dates.Add(item.Item1.ToString());
                        }

                        var quantityCollection = new SeriesCollection()
                    {
                    new RowSeries
                    {
                        Title = "Quantity: ",
                        Values = quantity,
                    }
                    };


                        productBarGraph.AxisY.Clear();
                        productBarGraph.AxisY.Add(new LiveCharts.Wpf.Axis
                        {
                            Title = "Date",
                            Labels = dates
                        });

                        productBarGraph.AxisX.Clear();
                        productBarGraph.AxisX.Add(new LiveCharts.Wpf.Axis
                        {
                            Title = "Quantity",
                            LabelFormatter = x => x.ToString("N0")

                        });

                        productBarGraph.Series = quantityCollection;
                    }
                    break;
                case 1:
                    if (books.Count() > 0 && categories.Count() > 0)
                    {
                        var weeklyProductResult = _statisticsBUS.getMonthlyQuantityOfSpecificProduct(books[productFigureIndex].bookID, categories[categoriesFigureIndex].tobID, selectedDate);

                        var weeklyQuantity = new ChartValues<int>();
                        var weeks = new List<string>();

                        foreach (var item in weeklyProductResult)
                        {
                            weeklyQuantity.Add((int)item.Item2);
                            weeks.Add(item.Item1.ToString());
                        }

                        var weeklyQuantityCollection = new SeriesCollection()
                        {
                            new ColumnSeries
                            {
                                Title = "Quantity: ",
                                Values = weeklyQuantity,
                            }
                        };

                        productBarGraph.AxisX.Clear();
                        productBarGraph.AxisX.Add(new LiveCharts.Wpf.Axis
                        {
                            Title = "Week",
                            Labels = weeks
                        });

                        productBarGraph.AxisY.Clear();
                        productBarGraph.AxisY.Add(new LiveCharts.Wpf.Axis
                        {
                            Title = "Quantity",
                            LabelFormatter = x => x.ToString("N0")

                        });

                        productBarGraph.Series = weeklyQuantityCollection;
                    }
                    break;
                case 2:
                    if (books.Count() > 0 && categories.Count() > 0)
                    {
                        var monthlyProductResult = _statisticsBUS.getMonthlyQuantityOfSpecificProduct(books[productFigureIndex].bookID, categories[categoriesFigureIndex].tobID, selectedDate);

                        var monthlyQuantity = new ChartValues<int>();
                        var months = new List<string>();

                        foreach (var item in monthlyProductResult)
                        {
                            monthlyQuantity.Add((int)item.Item2);
                            months.Add(item.Item1.ToString());
                        }

                        var monthlyQuantityCollection = new SeriesCollection()
                    {
                    new ColumnSeries
                    {
                        Title = "Quantity: ",
                        Values = monthlyQuantity,
                    }
                    };


                        productBarGraph.AxisX.Clear();
                        productBarGraph.AxisX.Add(new LiveCharts.Wpf.Axis
                        {
                            Title = "Month",
                            Labels = months
                        });

                        productBarGraph.AxisY.Clear();
                        productBarGraph.AxisY.Add(new LiveCharts.Wpf.Axis
                        {
                            Title = "Quantity",
                            LabelFormatter = x => x.ToString("N0")

                        });

                        productBarGraph.Series = monthlyQuantityCollection;
                    }
                    break;
                case 3:
                    if (books.Count() > 0 && categories.Count() > 0)
                    {
                        var yearlyProductResult = _statisticsBUS.getYearlyQuantityOfSpecificProduct(books[productFigureIndex].bookID, categories[categoriesFigureIndex].tobID);

                        var yearlyQuantity = new ChartValues<int>();
                        var years = new List<string>();

                        foreach (var item in yearlyProductResult)
                        {
                            yearlyQuantity.Add((int)item.Item2);
                            years.Add(item.Item1.ToString());
                        }

                        var yearlyQuantityCollection = new SeriesCollection()
                    {
                    new ColumnSeries
                    {
                        Title = "Quantity: ",
                        Values = yearlyQuantity,
                    }
                    };
                        productBarGraph.AxisX.Clear();
                        productBarGraph.AxisX.Add(new LiveCharts.Wpf.Axis
                        {
                            Title = "Month",
                            Labels = years
                        });

                        productBarGraph.AxisY.Clear();
                        productBarGraph.AxisY.Add(new LiveCharts.Wpf.Axis
                        {
                            Title = "Quantity",
                            LabelFormatter = x => x.ToString("N0")

                        });

                        productBarGraph.Series = yearlyQuantityCollection;
                    }
                    break;
            }
        }

        public void configurePieChart()
        {
            if (books.Count() > 0 && categories.Count() > 0)
            {
                var phoneResult = _statisticsBUS.getBookQuantityInCategory(categories[categoriesFigureIndex].tobID);

                var phoneQuantityCollection = new SeriesCollection();

                foreach (var item in phoneResult)
                {
                    phoneQuantityCollection.Add(new PieSeries
                    {
                        Title = item.Item1.ToString(),
                        Values = new ChartValues<double> { Convert.ToDouble((int)item.Item2) },
                        DataLabels = true,
                        //LabelPoint = labelPoint,
                    });
                }

                productPieChart.Series = phoneQuantityCollection;
            }
        }

        private void categoriesCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            books = BookBUS.findBookByTypeOfBook(categories[categoriesFigureIndex].tobID);
            productCombobox.ItemsSource = books;

            configurePieChart();

            if (books.Count > 0)
            {
                productFigureIndex = 0;
                productCombobox.SelectedIndex = productFigureIndex;
            }
        }

        private void productCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (productFigureIndex != -1)
            {
                configureBarGraphs();
            }
        }

        private void bargraphCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoriesFigureIndex != -1 && productFigureIndex != -1)
            {
                configureBarGraphs();
            }
        }

        private void statisticsDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            configureBarGraphs();
        }



        private void chartTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tabSelectedIndex)
            {
                case 0:
                    configureBarGraphs();
                    break;
                case 1:
                    configurePieChart();
                    break;
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow();
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


        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            AppConfig.SetValue("LastScreen", "GUI/saleanalysic.xaml");
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var screen = new analysicRevenue();
            screen.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var screen = new BestSelling();
            screen.Show();
            this.Close();
        }
    }

}