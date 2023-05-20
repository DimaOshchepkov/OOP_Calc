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
using CalcLibrary;

namespace CalculatorGUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> infix = new List<string>();
        int countOpenBrackets = 0;
        int countClosedBrackets = 0;

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
            countOpenBrackets++;
            infix.Add((sender as Button).Content.ToString());
            infix.Add("(");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Erase(object sender, RoutedEventArgs e)
        {
            if (infix.Count == 0)
                return;

            if (infix[infix.Count - 1] == "(")
                countOpenBrackets--;
            else if (infix[infix.Count - 1] == ")")
                countClosedBrackets--;

            infix.RemoveAt(infix.Count - 1);
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string buttonText = (sender as Button).Content.ToString();
            if (buttonText == "(")
                countOpenBrackets++;
            if (buttonText == ")" && countOpenBrackets > countClosedBrackets)
            {
                countClosedBrackets++;
                infix.Add(buttonText);
            }
            else if (buttonText != ")")
                infix.Add(buttonText);

            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddFact(object sender, RoutedEventArgs e)
        {
            infix.Add("!");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                string token = Calc.DoOperation(TextBoxExpression.Text);
                TextBoxResult.Text = token;
            }
            catch
            {
                TextBoxResult.Text = "Неверный формат ввода";
            }
            
        }

        private void ChangeSign(object sender, RoutedEventArgs e)
        {
            if (infix.Count == 0)
                return;
            else if (infix[0] == "-")
                infix.RemoveAt(0);
            else
                infix.Insert(0, "-");

            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddPI(object sender, RoutedEventArgs e)
        {
            infix.Add("3,14");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void CE(object sender, RoutedEventArgs e)
        {
            infix.Clear();
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddComma(object sender, RoutedEventArgs e)
        {
            if (infix.Count == 0 || !char.IsDigit(char.Parse(infix[infix.Count - 1])))
                infix.Add("0");

            int i = infix.Count - 1;
            while (i >= 0 && char.IsDigit(char.Parse(infix[i]))) 
                i--;

            if (i < 0 || infix[i] != ",")
                infix.Add(",");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Reciprocal(object sender, RoutedEventArgs e)
        {
            infix.Add("1");
            infix.Add("/");
            TextBoxExpression.Text = ToExpression(infix);
        }


        private void TextBoxExpression_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (infix.Count == 0)
                TextBoxExpression.Text = "0";
            TextBoxResult.Clear();
        }

        private void TextBoxResult_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddE(object sender, RoutedEventArgs e)
        {
            infix.Add("2,71");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddSqr(object sender, RoutedEventArgs e)
        {
            infix.Add("^");
            infix.Add("2");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonBin_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ContextMenu contextMenu = button.ContextMenu;
            contextMenu.IsOpen = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            infix.Add(menuItem.Header.ToString());
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void MenuItemToSys_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string op = menuItem.Header.ToString();
            int targetBase = -1;
            if (op == "bin")
                targetBase = 2;
            else if (op == "oct")
                targetBase = 8;
            else if (op == "hex")
                targetBase = 16;

            Calculate(sender, e);
            TextBoxResult.Text = ConvertToSys.ConvertBaseWithFraction(TextBoxResult.Text, 10, targetBase);
        }

        private void ButtonSys_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ContextMenu contextMenu = button.ContextMenu;
            contextMenu.IsOpen = true;
        }
    }
}
