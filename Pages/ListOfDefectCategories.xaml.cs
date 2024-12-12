using Defective_Cards.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Defective_Cards.Pages
{
    public partial class ListOfDefectCategories : Page
    {
        int selectedRowIndex;
        bool isRowSelected = false, causeCodeEntered = false, causeDescriptionEntred = false;
        public ListOfDefectCategories()
        {
            InitializeComponent();
            SessionData.Causes = SessionData.Causes;
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

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            CausePanel.Visibility = Visibility.Visible;

            AddDefectButton.Content = "Редактировать";

            DefectNumberTextBox.Text = SessionData.Causes[selectedRowIndex].Code.ToString();
            RejectCodeTextBox.Text = SessionData.Causes[selectedRowIndex].Description;

            ButtonAdd.IsEnabled = ButtonDelete.IsEnabled = ButtonEdit.IsEnabled = false;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            CausePanel.Visibility = Visibility.Visible;

            AddDefectButton.Content = "Добавить";

            ButtonAdd.IsEnabled = ButtonDelete.IsEnabled = ButtonEdit.IsEnabled = false;

        }

        private void AddDefectButton_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                if (causeCodeEntered && causeDescriptionEntred)
                {
                    if (AddDefectButton.Content.Equals("Добавить")) SessionData.Causes.Add(new Cause(Int32.Parse(DefectNumberTextBox.Text), RejectCodeTextBox.Text));
                    else SessionData.Causes[selectedRowIndex] = new Cause(Int32.Parse(DefectNumberTextBox.Text), RejectCodeTextBox.Text);

                    causeCodeEntered = causeDescriptionEntred = false;

                    DefectNumberTextBox.Text = RejectCodeTextBox.Text = "";

                    CausePanel.Visibility = Visibility.Collapsed;

                    ButtonAdd.IsEnabled = ButtonDelete.IsEnabled = ButtonEdit.IsEnabled = true;

                    break;
                }

                if (!causeCodeEntered)
                {
                    causeCodeEntered = AddDefectButton.Content.Equals("Добавить") ? Cause.CodeCheck_Add(DefectNumberTextBox.Text) : Cause.CodeCheck_Edit(DefectNumberTextBox.Text, selectedRowIndex);

                    if (!causeCodeEntered) break;
                }

                if (!causeDescriptionEntred)
                {
                    causeDescriptionEntred = RejectCodeTextBox.Text != "";

                    if (!causeDescriptionEntred) break;
                }
            }
        }

        private void RejectCodeTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Enter:
                    if (RejectCodeTextBox.Text == "") MessageBox.Show("Введите описание брака");
                    else causeDescriptionEntred = true;

                    break;
            }
        }

        private void DefectNumberTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
                case System.Windows.Input.Key.Enter:
                    if (DefectNumberTextBox.Text == "") MessageBox.Show("Введите код брака");
                    else causeCodeEntered = AddDefectButton.Content.Equals("Добавить") ? Cause.CodeCheck_Add(DefectNumberTextBox.Text) : Cause.CodeCheck_Edit(DefectNumberTextBox.Text, selectedRowIndex);

                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
    }
}
