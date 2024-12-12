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
                    MainFrame.Navigate(new StartPage());
                    break;
                case MessageBoxResult.No:
                    MainFrame.Navigate(new DefectiveCardsPage());
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
