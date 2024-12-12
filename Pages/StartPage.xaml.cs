using Defective_Cards.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Defective_Cards.Pages
{
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        bool numberIsEntered = false;

        private void StartFromNum_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.D0:
                case System.Windows.Input.Key.D1:
                case System.Windows.Input.Key.D2:
                case System.Windows.Input.Key.D3:
                case System.Windows.Input.Key.D4:
                case System.Windows.Input.Key.D5:
                case System.Windows.Input.Key.D6:
                case System.Windows.Input.Key.D7:
                case System.Windows.Input.Key.D8:
                case System.Windows.Input.Key.D9:
                case System.Windows.Input.Key.NumPad0:
                case System.Windows.Input.Key.NumPad1:
                case System.Windows.Input.Key.NumPad2:
                case System.Windows.Input.Key.NumPad3:
                case System.Windows.Input.Key.NumPad4:
                case System.Windows.Input.Key.NumPad5:
                case System.Windows.Input.Key.NumPad6:
                case System.Windows.Input.Key.NumPad7:
                case System.Windows.Input.Key.NumPad8:
                case System.Windows.Input.Key.NumPad9:
                case System.Windows.Input.Key.Back:
                    break;
                case Key.Enter:
                    numberIsEntered = NumberIsEntered();
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void ButtonStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            while (true)
            {
                if (numberIsEntered)
                {
                    SessionData.StacksCount = Int32.Parse(StartFromNum.Text);
                    NavigationService.Navigate(new DefectiveCardsPage());
                    break;
                }
                else
                {
                    numberIsEntered = NumberIsEntered();

                    if (!numberIsEntered) break;
                }
            }

        }

        bool NumberIsEntered()
        {
            if (StartFromNum.Text != "" && Int32.TryParse(StartFromNum.Text, out int i)) return true;
            else if (StartFromNum.Text == "")
            {
                MessageBox.Show("Введите число");
                return false;
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
                return false;
            }

        }
    }
}
