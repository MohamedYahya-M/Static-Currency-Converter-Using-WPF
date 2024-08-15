using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FromDataBinder();
            ToDataBinder();
            ClearControls();
            
        }

        public void ToDataBinder()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("text");
            dataTable.Columns.Add("value");

            dataTable.Rows.Add("--SELECT--", 0);
            dataTable.Rows.Add("INR", 1);
            dataTable.Rows.Add("USD", 83);
            dataTable.Rows.Add("EUR", 90);
            dataTable.Rows.Add("POUND", 100);
            dataTable.Rows.Add("RIYAL", 22);

            cmbToCurrency.ItemsSource = dataTable.DefaultView;
            cmbToCurrency.DisplayMemberPath = "text";
            cmbToCurrency.SelectedValuePath = "value";
            cmbToCurrency.SelectedIndex = 0;
        }

        public void FromDataBinder()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("text");
            dataTable.Columns.Add("value");

            dataTable.Rows.Add("--SELECT--", 0);
            dataTable.Rows.Add("INR",1);
            dataTable.Rows.Add("USD", 83);
            dataTable.Rows.Add("EUR", 90);
            dataTable.Rows.Add("POUND", 100);
            dataTable.Rows.Add("RIYAL", 22);

            cmbFromCurrency.ItemsSource = dataTable.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "text";
            cmbFromCurrency.SelectedValuePath = "value";
            cmbFromCurrency.SelectedIndex = 0;
        }
        private void Convert_Clicked(object sender, RoutedEventArgs e)
        {
            double ConvertedValue;
            
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {                
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrency.Focus();
                return;
            }
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbFromCurrency.Focus();
                return;
            }
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbToCurrency.Focus();
                return;
            }
            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                ConvertedValue = double.Parse(txtCurrency.Text);
                cvtcurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
            else
            {
                ConvertedValue = (double.Parse(cmbFromCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text)) / double.Parse(cmbToCurrency.SelectedValue.ToString());
                cvtcurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }       
    }

        public void Clear_Clicked(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        public void NumberValidationText(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^(0-9)+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
                cmbFromCurrency.SelectedIndex = 0;
            if (cmbToCurrency.Items.Count > 0)
                cmbToCurrency.SelectedIndex = 0;
            cvtcurrency.Content = "";
            txtCurrency.Focus();
        }
    }
}
