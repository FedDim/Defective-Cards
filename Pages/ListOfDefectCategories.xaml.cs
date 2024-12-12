using Defective_Cards.AppSystem;
using Defective_Cards.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Defective_Cards.Pages
{
    public partial class ListOfDefectCategories : Page
    {
        int selectedRowIndex;
        bool isRowSelected = false;
        public ListOfDefectCategories()
        {
            InitializeComponent();
            SessionData.Causes = WorkWithCausesJSON.CausesLoad();
            categoriesDataGrid.ItemsSource = SessionData.Causes;
        }

        private void GoMainPage(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CategoriesDataGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedItem != null)
            {
                selectedRowIndex = dataGrid.SelectedIndex;
                isRowSelected = true;
                MessageBox.Show("Строка выбрана");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isRowSelected)
            {
                MessageBoxResult MessageBoxResult = MessageBox.Show("Вы уверены, что хотите удалить данную строку?", "", MessageBoxButton.YesNo);
                if (MessageBoxResult == MessageBoxResult.Yes)
                {
                    SessionData.Causes.RemoveAt(selectedRowIndex);
                    isRowSelected = false;
                }

            }
            else
            {
                MessageBox.Show("Для удаления данных выберите одну из строк таблицы (Нажмите два раза по нужной Вам строке)");
            }
        }

    }
}
