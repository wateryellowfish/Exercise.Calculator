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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Operation? operation;
        decimal firstValue;
        decimal secondValue;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Number_Click(object sender, RoutedEventArgs e)
        {

            decimal value = Convert.ToDecimal(((Button)sender).Content);

            try
            {
                label_Result.Content = Convert.ToDecimal(label_Result.Content) * 10 + value;
            }
            catch (OverflowException) {/*do nothing*/ }
        }

        private void button_Clear_Click(object sender, RoutedEventArgs e)
        {
            label_Result.Content = 0;
        }

        private void button_Negative_Click(object sender, RoutedEventArgs e)
        {
            label_Result.Content = Convert.ToDecimal(label_Result.Content) * -1;
        }

        private void Percent_Button_Click(object sender, RoutedEventArgs e)
        {
            decimal value = Convert.ToDecimal(label_Result.Content);
            if (value % 1 == 0) label_Result.Content = value / 100;
            else label_Result.Content = value * 100;
        }

        public delegate decimal Operation(decimal firstNum, decimal secondNum);

        public decimal Addition(decimal firstNum, decimal secondNum)
        {
            decimal result = 0;
            try
            {
                result = firstNum + secondNum;
            }
            catch (OverflowException)
            {
                label_Result.Content = "Error: Overflow";
            }
            return result;
        }

        public decimal Subtraction(decimal firstNum, decimal secondNum)
        {
            return firstNum - secondNum;
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            operation = Addition;
            firstValue = Convert.ToDecimal(label_Result.Content);
            label_Result.Content = 0;
        }

        private void button_Subtract_Click(object sender, RoutedEventArgs e)
        {
            operation = Subtraction;
            firstValue = Convert.ToDecimal(label_Result.Content);
            label_Result.Content = 0;
        }

        private void button_Equals_Click(object sender, RoutedEventArgs e)
        {
            if (operation == null) return;

            secondValue = Convert.ToDecimal(label_Result.Content);
            label_Result.Content = operation(firstValue, secondValue);
        }
    }
}