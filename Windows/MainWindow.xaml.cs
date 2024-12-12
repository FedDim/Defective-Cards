using Defective_Cards.AppSystem;
using Defective_Cards.Data;
using Defective_Cards.Pages;
using System.Windows;

namespace Defective_Cards
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageBoxResult messageBoxResult = MessageBox.Show("Начать с определённого номера стопки? (Нет — начнём с 0)", "", MessageBoxButton.YesNo);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    Left = (SystemParameters.PrimaryScreenWidth / 2) - (Width / 2);
                    Top = (SystemParameters.PrimaryScreenHeight / 2) - (Height / 2);
                    MainFrame.Navigate(new StartPage());
                    break;
                case MessageBoxResult.No:
                    var window = Application.Current.MainWindow as MainWindow;

                    if (window != null)
                    {
                        window.WindowState = WindowState.Maximized;

                        MainFrame.Navigate(new DefectiveCardsPage());
                    }

                    break;
                default:
                    MainFrame.Navigate(new DefectiveCardsPage());
                    break;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WorkWithCausesJSON.Serialization_CauseData(SessionData.Causes, "CauseData.json");
        }
    }
}
