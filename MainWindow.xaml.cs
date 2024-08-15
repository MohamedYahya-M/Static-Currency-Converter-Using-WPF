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

            //Check if the amount textbox is Null or Blank
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                //If amount textbox is Null or Blank it will show this message box
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                //After clicking on messagebox OK set focus on amount textbox
                txtCurrency.Focus();
                return;
            }
            //Else if currency From is not selected or select default text --SELECT--
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                //Show the message
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on the From Combobox
                cmbFromCurrency.Focus();
                return;
            }
            //Else if currency To is not selected or select default text --SELECT--
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                //Show the message
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on the To Combobox
                cmbToCurrency.Focus();
                return;
            }

            //Check if From and To Combobox selected values are same
            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                //Amount textbox value set in ConvertedValue.
                //double.parse is used for converting the datatype String To Double.
                //Textbox text have string and ConvertedValue is double Datatype
                ConvertedValue = double.Parse(txtCurrency.Text);

                //Show the label converted currency and converted currency name and ToString("N3") is used to place 000 after the dot(.)
                cvtcurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
            else
            {
                //Calculation for currency converter is From Currency value multiply(*) 
                //With the amount textbox value and then that total divided(/) with To Currency value
                ConvertedValue = (double.Parse(cmbFromCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text)) / double.Parse(cmbToCurrency.SelectedValue.ToString());

                //Show the label converted currency and converted currency name.
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