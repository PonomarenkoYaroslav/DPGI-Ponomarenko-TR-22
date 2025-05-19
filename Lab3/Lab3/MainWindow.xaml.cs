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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>()
        {
            {"USD", 36.5m},
            {"EUR", 39.8m},
            {"CHF", 41.1m},
            {"GBP", 46.2m}
        };

        public MainWindow()
        {
            InitializeComponent();
            CurrencyComboBox.ItemsSource = exchangeRates.Keys;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrencyComboBox.SelectedItem == null || !decimal.TryParse(AmountTextBox.Text, out decimal amount))
            {
                MessageBox.Show("Будь ласка, введіть коректні дані");
                return;
            }

            string selectedCurrency = CurrencyComboBox.SelectedItem.ToString();
            decimal rate = exchangeRates[selectedCurrency];
            decimal result;

            if (ToUahRadio.IsChecked == true)
            {
                result = amount * rate;
                ResultText.Text = $"{amount} {selectedCurrency} = {result:F2} UAH";
            }
            else
            {
                result = amount / rate;
                ResultText.Text = $"{amount} UAH = {result:F2} {selectedCurrency}";
            }
        }
    }
}
