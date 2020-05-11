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

namespace CalculatorAppWPF
{
    public enum Operation
    {
       Addition,
       Substraction,
       Multiplication,
       Division
    }

    public class SimpleMath
    {
        public static double Add(double x, double y) => x + y;
        public static double Substract(double x, double y) => x - y;
        public static double Multiply(double x, double y) => x * y;
        public static double Divide(double x, double y) => x / y;
    }
    public partial class MainWindow : Window
    {
        double result, lastnumber, currentnumber;
        Operation operation;
        
        public MainWindow()
        {
            InitializeComponent();

            negativeButton.Click += ChangingButton_Click;
            percentageButton.Click += ChangingButton_Click;
            acButton.Click += ChangingButton_Click;

            addButton.Click += OperatorButton_Click;
            multiplyButton.Click += OperatorButton_Click;
            substractButton.Click += OperatorButton_Click;
            divideButton.Click += OperatorButton_Click;

            equalsButton.Click += EqualsButton_Click;

            ResizeMode = ResizeMode.NoResize;
        }

        private void NumberButton_click(object sender, RoutedEventArgs e)
        {
            var value = (e.Source as Button).Content.ToString();
            if(resultLabel.Content.ToString().Equals("0"))
            {
                resultLabel.Content = value;
            }
            else
            {
                resultLabel.Content += value;
            }
            double.TryParse(resultLabel.Content.ToString(), out currentnumber);
        }

        private void ChangingButton_Click(object sender, RoutedEventArgs e)
        {
            Button source = e.Source as Button;
            if (double.TryParse(resultLabel.Content.ToString(), out currentnumber))
            {
                if (source == percentageButton) currentnumber /= 100;
                if (source == negativeButton) currentnumber *= -1;
                if (source == acButton)
                {
                    currentnumber = 0;
                    lastnumber = 0;
                    result = 0;
                }
                resultLabel.Content = currentnumber.ToString();
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button source = e.Source as Button;
            if (source == addButton) operation = Operation.Addition;
            if (source == substractButton) operation = Operation.Substraction;
            if (source == divideButton) operation = Operation.Division;
            if (source == multiplyButton) operation = Operation.Multiplication;
            if (double.TryParse(resultLabel.Content.ToString(), out lastnumber))
            {
                resultLabel.Content = "0";
                currentnumber = 0;
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if(operation.Equals(Operation.Addition))
            {
                result = currentnumber + lastnumber;
            }
            else if(operation.Equals(Operation.Substraction))
            {
                result = lastnumber - currentnumber;
            }
            else if(operation.Equals(Operation.Multiplication))
            {
                result = currentnumber * lastnumber;
            }
            else if(operation.Equals(Operation.Division))
            {
                if(currentnumber!=0)
                {
                    result = lastnumber / currentnumber;
                    resultLabel.Content = result.ToString();
                    currentnumber = lastnumber;
                    lastnumber = result;
                    return;
                }
                else
                {
                    MessageBox.Show("Division by zero(0) is prohibited.","Wrong operation",MessageBoxButton.OK, MessageBoxImage.Error);
                    currentnumber = 0;
                    lastnumber = 0;
                    result = 0;
                    resultLabel.Content = result;
                    return;
                }
            }
            resultLabel.Content = result.ToString();
            currentnumber = result;
        }
    }
}
