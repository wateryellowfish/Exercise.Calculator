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
        bool IsDecimal = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Number_Click(object sender, RoutedEventArgs e)
        {

            decimal value = Convert.ToDecimal(((Button)sender).Content);

            try
            {
                if (IsDecimal == false) label_Result.Content = Convert.ToDecimal(label_Result.Content) * 10 + value;
                else
                {
                    decimal number = Convert.ToDecimal(label_Result.Content);
                    double decimalPlaces = 1;
                    while (number % 1 != 0)
                    {
                        number *= 10;
                        decimalPlaces++;
                    }
                    number = number * 10 + value;
                    label_Result.Content = number / (decimal)(Math.Pow((double)10,decimalPlaces));
                }
            }
            catch (OverflowException) {/*do nothing*/ }
            catch (FormatException)
            {
                label_Result.Content = 0;
            }
        }

        private void button_Clear_Click(object sender, RoutedEventArgs e)
        {
            label_Result.Content = 0;
            firstValue = 0; secondValue = 0;
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
            return firstNum + secondNum;
        }

        public decimal Subtraction(decimal firstNum, decimal secondNum)
        {
            return firstNum - secondNum;
        }

        public decimal Multiplication(decimal firstNum, decimal secondNum)
        {
            return firstNum * secondNum;
        }

        public decimal Division(decimal firstNum, decimal secondNum)
        {
            return firstNum / secondNum;
        }

        private void button_Equals_Click(object sender, RoutedEventArgs e)
        {
            if (operation == null) return;

            secondValue = Convert.ToDecimal(label_Result.Content);
            try
            {
                label_Result.Content = operation(firstValue, secondValue);
            }
            catch (OverflowException)
            {
                label_Result.Content = "Error: Overflow";
            }
            catch (DivideByZeroException)
            {
                label_Result.Content = "Error: Can't divide by zero";
            }
            firstValue = 0; secondValue = 0;
            operation = null;
            IsDecimal = false;
            label_FirstValue.Visibility = Visibility.Collapsed;
        }

        private void button_Operator_Click(object sender, RoutedEventArgs e)
        {
            if (sender == button_Add) operation = Addition;
            else if (sender == button_Subtract) operation = Subtraction;
            else if (sender == button_Multiply) operation = Multiplication;
            else if (sender == button_Divide) operation = Division;

            firstValue = Convert.ToDecimal(label_Result.Content);
            label_Result.Content = 0;
            IsDecimal = false;
            label_FirstValue.Visibility = Visibility.Visible;
            label_FirstValue.Content = $"{firstValue} {((Button)sender).Content}";
        }

        private void button_Decimal_Click(object sender, RoutedEventArgs e)
        {
            IsDecimal = true;
        }
    }
}