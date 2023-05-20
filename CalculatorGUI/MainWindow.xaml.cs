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
        bool isCalulate = false;

        /// <summary>
        /// Переводит список токенов в строку
        /// </summary>
        /// <param name="infix">Сеписок токенов</param>
        /// <returns>Строка-выражение</returns>
        string ToExpression(List<string> infix)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var str in infix)
                sb.Append(str);

            return sb.ToString();
        }
        List<string> ToInfix(string expression)
        {
            List<string> infix = new List<string>();
            foreach (char ch in expression)
                infix.Add(ch.ToString());

            return infix;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddFunc(object sender, RoutedEventArgs e)
        {
            string buttonText = (sender as Button).Content.ToString();
            if (!IsPrevNotDigit() && infix.Count != 0)
                return;

            countOpenBrackets++;
            infix.Add(buttonText);
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

        private void MoveTextToResult()
        {
            if (isCalulate)
            {
                infix.Clear();
                if(TextBoxExpression.Text != "0")
                    infix = ToInfix(TextBoxResult.Text);
                countClosedBrackets = 0;
                countOpenBrackets = 0;
            }
        }
   
        private void Add(object sender, RoutedEventArgs e)
        {
            string buttonText = (sender as Button).Content.ToString();

            MoveTextToResult();

            switch(buttonText)
            {
                case "+":
                case "-":
                case "/":
                case "*":
                    if (infix.Count == 0)
                        infix.Add("0");

                    infix.Add(buttonText);
                    break;
                default:
                    infix.Add(buttonText);
                    break;
            }

            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddFact(object sender, RoutedEventArgs e)
        {
            MoveTextToResult();
            if (infix.Count == 0)
                infix.Add("0");
            infix.Add("!");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                string token = Calc.DoOperation(TextBoxExpression.Text);
                TextBoxResult.Text = token;
                isCalulate = true;
            }
            catch (Exception ex)
            {
                isCalulate = false;
                TextBoxResult.Text = "Неверный формат ввода";
            }
            
        }

        private void ChangeSign(object sender, RoutedEventArgs e)
        {
            MoveTextToResult();

            if (infix.Count == 0)
                return;
            MoveTextToResult();
            if (infix[0] == "-")
                infix.RemoveAt(0);
            else
                infix.Insert(0, "-");

            TextBoxExpression.Text = ToExpression(infix);
        }
        private bool PossibleAddComma()
        {
            if (infix.Count == 0 || !char.IsDigit(char.Parse(infix[infix.Count - 1])))
                return true;

            int i = infix.Count - 1;
            while (i >= 0 && char.IsDigit(char.Parse(infix[i])))
                i--;

            if (i < 0 || infix[i] != ",")
                return true;

            return false;
        }
        private void AddPI(object sender, RoutedEventArgs e)
        {   
            if (PossibleAddComma() && IsPrevNotDigit() || infix.Count == 0)
            {
                MoveTextToResult();
                infix.Add("3");
                infix.Add(",");
                infix.Add("1");
                infix.Add("4");
            }
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void CE(object sender, RoutedEventArgs e)
        {
            infix.Clear();
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddComma(object sender, RoutedEventArgs e)
        {
            if (infix.Count == 0)
                infix.Add("0");

            if (PossibleAddComma())
                infix.Add(",");
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void Reciprocal(object sender, RoutedEventArgs e)
        {
            //MoveTextToResult();
            try
            {
                string token = Calc.DoOperation(TextBoxExpression.Text);
                infix.Clear();
                infix.Add("1");
                infix.Add("/");
                infix.Add("(");
                infix.Add(token);
                infix.Add(")");
                TextBoxExpression.Text = ToExpression(infix);
            }
            catch (Exception ex)
            {
                TextBoxResult.Text = "Неверный формат ввода";
            }
        }


        private void TextBoxExpression_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (infix.Count == 0)
                TextBoxExpression.Text = "0";

            isCalulate = false;

            TextBoxResult.Clear();
        }

        private void TextBoxResult_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private bool IsPrevNotDigit()
        {
            if (infix.Count == 0)
                return false;

            if (double.TryParse(infix[infix.Count - 1], out double _))
                return false;

            return true;
        }

        private void AddE(object sender, RoutedEventArgs e)
        {
            
            if (PossibleAddComma() && IsPrevNotDigit() || infix.Count == 0)
            {
                MoveTextToResult();
                infix.Add("2");
                infix.Add(",");
                infix.Add("7");
                infix.Add("1");
            }
            TextBoxExpression.Text = ToExpression(infix);
        }

        private void AddSqr(object sender, RoutedEventArgs e)
        {
            MoveTextToResult();

            try
            {
                string token = Calc.DoOperation(TextBoxExpression.Text);
                infix.Clear();
                infix.Add(token);
                infix.Add("^");
                infix.Add("2");
                TextBoxExpression.Text = ToExpression(infix);
            }
            catch (Exception ex)
            {
                TextBoxResult.Text = "Неверный формат ввода";
            }
            
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

        private void AddSqrt(object sender, RoutedEventArgs e)
        {
            try
            {
                string token = Calc.DoOperation(TextBoxExpression.Text);
                infix.Clear();
                infix.Add("sqrt");
                infix.Add("(");
                infix.Add(token);
                infix.Add(")");
                TextBoxExpression.Text = ToExpression(infix);
            }
            catch (Exception ex)
            {
                TextBoxResult.Text = "Неверный формат ввода";
            }
            
        }

        private void AddOpenBracker(object sender, RoutedEventArgs e)
        {
            string buttonText = (sender as Button).Content.ToString();
            countOpenBrackets++;
            infix.Add(buttonText);
        }

        private void AddClosedBracket(object sender, RoutedEventArgs e)
        {
            string buttonText = (sender as Button).Content.ToString();
            if (countOpenBrackets > countClosedBrackets)
            {
                countClosedBrackets++;
                infix.Add(buttonText);
            }
        }

        private void AddExp(object sender, RoutedEventArgs e)
        {
            try
            {
                string token = Calc.DoOperation(TextBoxExpression.Text);
                infix.Clear();
                infix.Add("exp");
                infix.Add("(");
                infix.Add(token);
                infix.Add(")");
                TextBoxExpression.Text = ToExpression(infix);
            }
            catch (Exception ex)
            {
                TextBoxResult.Text = "Неверный формат ввода";
            }
        }
    }
}
