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
        List<string> infix = new List<string>();
        string ToExpression(List<string> infix)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var str in infix)
                sb.Append(str);
            return sb.ToString();
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddFunc(object sender, RoutedEventArgs e)
        {
            infix.Add((sender as Button).Content.ToString() + "(");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Erase(object sender, RoutedEventArgs e)
        {
            if (infix.Count == 0)
                return;

            infix.RemoveAt(infix.Count - 1);
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddBinOp(object sender, RoutedEventArgs e)
        {

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            infix.Add((sender as Button).Content.ToString());
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddFact(object sender, RoutedEventArgs e)
        {
            infix.Add("!");
            TextBoxExpression.Text = ToExpression(infix);
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
            infix.Clear();
        }

        private void AddComma(object sender, RoutedEventArgs e)
        {
            if (infix.Count != 0 && infix[infix.Count - 1] == ",")
                return;

            infix.Add(",");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Reciprocal(object sender, RoutedEventArgs e)
        {
            infix.Add("1/");
            TextBoxExpression.Text = ToExpression(infix);
        }


        private void TextBoxExpression_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (infix.Count == 0)
                TextBoxExpression.Text = "0";
        }
    }
}
