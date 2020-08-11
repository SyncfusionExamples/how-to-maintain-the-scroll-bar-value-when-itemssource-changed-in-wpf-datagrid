using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using System.ComponentModel;
using System.Data;
using Syncfusion.Data;
using System.Threading.Tasks;

namespace SfDataGrid_Sample
{
    
    public partial class MainWindow : Window
    {
        List<Group> expandedGroups = null;
        List<string> columnName = null;
        ViewModel viewModel;

        private  double _scrollbarValue;
        public  double Scrollbarvalue
        {
            get { return _scrollbarValue; }
            set { _scrollbarValue = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
            expandedGroups = new List<Group>();
            columnName = new List<string>();
            viewModel = this.DataContext as ViewModel;
            this.dataGrid.ItemsSourceChanged += DataGrid_ItemsSourceChanged;
        }

        private void DataGrid_ItemsSourceChanged(object sender, GridItemsSourceChangedEventArgs e)
        {
            if (columnName.Count > 0)
            {
                foreach (var col in columnName)
                {
                    this.dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription() { ColumnName = col });
                }
                foreach (Group group in dataGrid.View.Groups)
                {
                    var isExpandGroup = group;
                    var key = expandedGroups.FirstOrDefault(colu => colu.Key.ToString() == isExpandGroup.Key.ToString());
                    do
                    {
                        if (key != null)
                            dataGrid.ExpandGroup(isExpandGroup);

                        if (isExpandGroup.Groups != null)
                        {
                            isExpandGroup = isExpandGroup.Groups[0];
                            key = expandedGroups.FirstOrDefault(col => col.Groups[0].Key.ToString() == group.Groups[0].Key.ToString());
                        }
                        else
                            isExpandGroup = null;
                    } while (isExpandGroup != null);
                }
            }
            VisualContainer container = this.dataGrid.GetVisualContainer();
            container.ScrollRows.ScrollBar.Value = this.Scrollbarvalue;
            container.InvalidateMeasureInfo();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var groups = dataGrid.View.Groups;
            foreach (Group group in groups)
            {
                if (group.IsExpanded)
                    expandedGroups.Add(group);
            }
            foreach (GroupColumnDescription groupColumnDescriptions in dataGrid.GroupColumnDescriptions)
                columnName.Add(groupColumnDescriptions.ColumnName);
            VisualContainer container = this.dataGrid.GetVisualContainer();
            double scrollValue = container.ScrollRows.ScrollBar.Value;
            this.Scrollbarvalue = scrollValue;
            //change Items source
            this.dataGrid.ItemsSource = viewModel.Ordersnew;
        }
    }

    public class OrderInfo : INotifyPropertyChanged
    {
        int orderID;
        string customerId;
        string country;
        string customerName;
        string shippingCity;
        bool isShipped;

        public OrderInfo()
        {

        }

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; this.OnPropertyChanged("OrderID"); }
        }

        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; this.OnPropertyChanged("CustomerID"); }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; this.OnPropertyChanged("CustomerName"); }
        }

        public string Country
        {
            get { return country; }
            set { country = value; this.OnPropertyChanged("Country"); }
        }

        public string ShipCity
        {
            get { return shippingCity; }
            set { shippingCity = value; this.OnPropertyChanged("ShipCity"); }
        }

        public bool IsShipped
        {
            get { return isShipped; }
            set { isShipped = value; this.OnPropertyChanged("IsShipped"); }
        }


        public OrderInfo(int orderId, string customerName, string country, string customerId, string shipCity, bool isShipped)
        {
            this.OrderID = orderId;
            this.CustomerName = customerName;
            this.Country = country;
            this.CustomerID = customerId;
            this.ShipCity = shipCity;
            this.IsShipped = isShipped;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ViewModel
    {
        private ObservableCollection<OrderInfo> orders;
        public ObservableCollection<OrderInfo> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        private ObservableCollection<OrderInfo> ordersNew;
        public ObservableCollection<OrderInfo> Ordersnew
        {
            get { return ordersNew; }
            set { ordersNew = value; }
        }

        public ViewModel()
        {
            orders = new ObservableCollection<OrderInfo>();

            ordersNew = new ObservableCollection<OrderInfo>();
            orders.Add(new OrderInfo(1001, null, "Germany", "ALFKI", "Berlin", true));
            orders.Add(new OrderInfo(1002, string.Empty, "Mexico", "ANATR", "Mexico D.F.", false));
            orders.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", "Mexico D.F.", true));
            orders.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", "London", true));
            orders.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", "Lula", false));
            orders.Add(new OrderInfo(1006, "Hanna Moos", "Germany", "BLAUS", "Mannheim", true));
            orders.Add(new OrderInfo(1007, "Frederique Citeaux", "France", "BLONP", "Strasbourg", true));
            orders.Add(new OrderInfo(1008, "Martin Sommer", "Spain", "BOLID", "Madrid", true));
            orders.Add(new OrderInfo(1009, "Laurence Lebihan", "France", "BONAP", "Marseille", false));
            orders.Add(new OrderInfo(1010, "Elizabeth Lincoln", "Canada", "BOTTM", "Tsawassen", true));
            orders.Add(new OrderInfo(1001, null, "Germany", "ALFKI", "Berlin", true));
            orders.Add(new OrderInfo(1002, string.Empty, "Mexico", "ANATR", "Mexico D.F.", false));
            orders.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", "Mexico D.F.", true));
            orders.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", "London", true));
            orders.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", "Lula", false));
            orders.Add(new OrderInfo(1006, "Hanna Moos", "Germany", "BLAUS", "Mannheim", true));
            orders.Add(new OrderInfo(1007, "Frederique Citeaux", "France", "BLONP", "Strasbourg", true));
            orders.Add(new OrderInfo(1008, "Martin Sommer", "Spain", "BOLID", "Madrid", true));
            orders.Add(new OrderInfo(1009, "Laurence Lebihan", "France", "BONAP", "Marseille", false));
            orders.Add(new OrderInfo(1010, "Elizabeth Lincoln", "Canada", "BOTTM", "Tsawassen", true));

            ordersNew = new ObservableCollection<OrderInfo>();
            ordersNew.Add(new OrderInfo(1001, null, "Germany", "ALFKI", "Berlin", true));
            ordersNew.Add(new OrderInfo(1002, string.Empty, "Mexico", "ANATR", "Mexico D.F.", false));
            ordersNew.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", "Mexico D.F.", true));
            ordersNew.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", "London", true));
            ordersNew.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", "Lula", false));
            ordersNew.Add(new OrderInfo(1006, "Hanna Moos", "Germany", "BLAUS", "Mannheim", true));
            ordersNew.Add(new OrderInfo(1007, "Frederique Citeaux", "France", "BLONP", "Strasbourg", true));
            ordersNew.Add(new OrderInfo(1008, "Martin Sommer", "Spain", "BOLID", "Madrid", true));
            ordersNew.Add(new OrderInfo(1009, "Laurence Lebihan", "France", "BONAP", "Marseille", false));
            ordersNew.Add(new OrderInfo(1010, "Elizabeth Lincoln", "Canada", "BOTTM", "Tsawassen", true));
            ordersNew.Add(new OrderInfo(1001, null, "Germany", "ALFKI", "Berlin", true));
            ordersNew.Add(new OrderInfo(1002, string.Empty, "Mexico", "ANATR", "Mexico D.F.", false));
            ordersNew.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", "Mexico D.F.", true));
            ordersNew.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", "London", true));
            ordersNew.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", "Lula", false));
            ordersNew.Add(new OrderInfo(1006, "Hanna Moos", "Germany", "BLAUS", "Mannheim", true));
            ordersNew.Add(new OrderInfo(1007, "Frederique Citeaux", "France", "BLONP", "Strasbourg", true));
            ordersNew.Add(new OrderInfo(1008, "Martin Sommer", "Spain", "BOLID", "Madrid", true));
            ordersNew.Add(new OrderInfo(1009, "Laurence Lebihan", "France", "BONAP", "Marseille", false));
            ordersNew.Add(new OrderInfo(1010, "Elizabeth Lincoln", "Canada", "BOTTM", "Tsawassen", true));
        }

    }

}
