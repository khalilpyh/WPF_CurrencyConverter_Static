/*
 * Name: Yuhao Peng
 * Title: Currency Converter using Static Data
 * Date: 2022-07-19
 * */

using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CurrencyConverterStatic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCurrencyData();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            //declare a variable to hold the converted value
            decimal convertedValue;

            if(txtCurrency.Text == null || txtCurrency.Text.Trim() == "")       //check user input in Currency TextBox, display message if it is empty or null
            {
                MessageBox.Show("Please Enter Currency Amount", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrency.Focus();
                return;
            }
            else if(cboFromCurrency.SelectedValue == null || cboFromCurrency.SelectedIndex == 0)   //check user selection in FromCurrency ComboBox, display message if there is nothing selected or it is default selection
            {
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cboFromCurrency.Focus();
                return;
            }
            else if (cboToCurrency.SelectedValue == null || cboToCurrency.SelectedIndex == 0)      //check user selection in ToCurrency ComboBox, display message if there is nothing selected or it is default selection
            {
                MessageBox.Show("Please Select Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cboToCurrency.Focus();
                return;
            }

            if (cboFromCurrency.Text == cboToCurrency.Text)     //if FromCurrency and ToCurrency ComboBox selected values are the same, display exactly the value that user entered in the Label
            {
                convertedValue = decimal.Parse(txtCurrency.Text);
                lblCurrency.Content = $"{cboToCurrency.Text} {convertedValue.ToString("C2")}";
            }
            else                                                //execute the calculation of both ComboBox selections are not the same
            {
                convertedValue = (decimal.Parse(cboFromCurrency.SelectedValue.ToString()) 
                                 * decimal.Parse(txtCurrency.Text)) 
                                 / decimal.Parse(cboToCurrency.SelectedValue.ToString());
                lblCurrency.Content = $"{cboToCurrency.Text} {convertedValue.ToString("C2")}";
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //reset all combobox selections, empty textbox and label 
            ClearAllValues();
        }

        private void txtCurrency_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //validate user input to only allow int value to be entered
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Create hard coded currency exchange rate and bind the data to the combo boxes on UI.
        /// </summary>
        private void LoadCurrencyData()
        {
            //create a datatable object to store currency information
            DataTable dtCurrency = new DataTable();
            
            //add columns to datatable
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");
            //add rows to datatable
            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("SAR", 20);
            dtCurrency.Rows.Add("POUND", 5);
            dtCurrency.Rows.Add("DEM", 43);
            dtCurrency.Rows.Add("RMB", 12);
            dtCurrency.Rows.Add("CAD", 62);


            //set data to be displayed in currency combobox
            cboFromCurrency.DisplayMemberPath = "Text";
            //set value behind currency combobox selection
            cboFromCurrency.SelectedValuePath = "Value";
            //binding currency data to "from currency“ combobox
            cboFromCurrency.ItemsSource = dtCurrency.DefaultView;

            //set currency combobox default selection
            cboFromCurrency.SelectedIndex = 0;

            //set data to be displayed in currency combobox
            cboToCurrency.DisplayMemberPath = "Text";
            //set value behind currency combobox selection
            cboToCurrency.SelectedValuePath = "Value";
            //binding currency data to "from currency“ combobox
            cboToCurrency.ItemsSource = dtCurrency.DefaultView;

            //set currency combobox default selection
            cboToCurrency.SelectedIndex = 0;
        }

        /// <summary>
        /// Reset all controls on UI. (clear Textbox, set ComboBox selected item to default selection...)
        /// </summary>
        private void ClearAllValues()
        {
            //empty the textbox and the result label
            txtCurrency.Text = "";
            lblCurrency.Content = "";

            //set both combobox selection to default, validate to make sure there are items loaded to the combobox
            if (cboFromCurrency.Items.Count > 0)           
                cboFromCurrency.SelectedIndex = 0;
            if (cboToCurrency.Items.Count > 0)
                cboToCurrency.SelectedIndex = 0;

            //put focus on the textbox
            txtCurrency.Focus();
        }
    }
}
