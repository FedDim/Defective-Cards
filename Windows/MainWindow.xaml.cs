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
            MainFrame.Navigate(new DefectiveCardsPage());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WorkWithCausesJSON.Serialization_CauseData(SessionData.Causes, "CauseData.json");
        }
    }
}
