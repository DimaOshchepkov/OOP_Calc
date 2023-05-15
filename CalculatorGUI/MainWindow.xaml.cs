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

namespace CalculatorGUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddFunc(object sender, RoutedEventArgs e)
        {
            if (TextBoxExpression.Text == "0")
                TextBoxExpression.Text = String.Empty;

            TextBoxExpression.Text += (sender as Button).Content.ToString() + "(";
        }

        private void Erase(object sender, RoutedEventArgs e)
        {
            if (TextBoxExpression.Text != String.Empty)
                TextBoxExpression.Text = TextBoxExpression.Text.Substring(0,
                    TextBoxExpression.Text.Length - 1);

            if (TextBoxExpression.Text == String.Empty)
                TextBoxExpression.Text = "0";
        }

        private void AddBinOp(object sender, RoutedEventArgs e)
        {

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (TextBoxExpression.Text == "0")
                TextBoxExpression.Text = String.Empty;

            TextBoxExpression.Text += (sender as Button).Content.ToString();
        }

        private void AddFact(object sender, RoutedEventArgs e)
        {
            TextBoxExpression.Text += "!";
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeSign(object sender, RoutedEventArgs e)
        {

        }

        private void AddPI(object sender, RoutedEventArgs e)
        {

        }

        private void CE(object sender, RoutedEventArgs e)
        {

        }

        private void AddComma(object sender, RoutedEventArgs e)
        {
            TextBoxExpression.Text += ",";
        }

        private void Reciprocal(object sender, RoutedEventArgs e)
        {
            TextBoxExpression.Text += "1/";
        }
    }
}
