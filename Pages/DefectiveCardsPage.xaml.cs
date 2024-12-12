using Defective_Cards.AppSystem;
using Defective_Cards.Data;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Defective_Cards.Pages
{
    public partial class DefectiveCardsPage : Page
    {

        bool сardNumberEntered = false, causeCodeEntered = false, isRowSelected = false;
        int selectedRowIndex;


        public DefectiveCardsPage()
        {
            InitializeComponent();
            LimitLoad();
            SessionData.Cards = WorkWithTXT.CardsLoad();

            CardsDataGrid.ItemsSource = SessionData.Cards;

            Cause.Deserialization_CauseData(ref SessionData.Causes, "CauseData.json");

            InformationUpdate(true);
        }

        void LimitLoad()
        {
            CardNumberTextBox.MaxLength = AppData.TEXTBOX_MAX_LENGTH;
            CauseCodeTextBox.MaxLength = AppData.TEXTBOX_MAX_LENGTH;
        }

        void InformationUpdate(bool isLoad)
        {
            TotalСards.Text = $"Всего Карт {SessionData.Cards.Count}";

            if (SessionData.Cards.Count / AppData.IN_STACK - SessionData.StacksCount == 1)
            {
                SessionData.StacksCount++;
                if (!isLoad) MessageBox.Show("Новая стопка готова");
            }

            NumberOfStacks.Text = $"Количество стопок {SessionData.StacksCount}";
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ButtonAdd.IsEnabled = false;

            while (true)
            {
                if (сardNumberEntered && causeCodeEntered)
                {
                    if (isRowSelected)
                    {
                        SessionData.Cards[selectedRowIndex] = new Card(CardNumberTextBox.Text.Trim().Replace(" ", ""), Int32.Parse(CauseCodeTextBox.Text));
                        WorkWithTXT.Edit(selectedRowIndex);
                        ButtonAdd.Content = "Добавить";
                        ButtonDelete.IsEnabled = true;
                    }
                    else
                    {
                        SessionData.Cards.Add(new Card(CardNumberTextBox.Text.Trim().Replace(" ", ""), Int32.Parse(CauseCodeTextBox.Text)));
                        WorkWithTXT.Add();
                        InformationUpdate(false);
                    }

                    CardNumberTextBox.Text = CauseCodeTextBox.Text = "";
                    сardNumberEntered = causeCodeEntered = false;
                    break;
                }

                if (!сardNumberEntered)
                {
                    сardNumberEntered = isRowSelected ? Card.NumberCheck(CardNumberTextBox.Text, selectedRowIndex) : Card.NumberCheck(CardNumberTextBox.Text);

                    if (!сardNumberEntered) break;
                }

                if (!causeCodeEntered)
                {
                    causeCodeEntered = Cause.CodeCheck(CauseCodeTextBox.Text);

                    if (!causeCodeEntered) break;
                }
            }

            ButtonAdd.IsEnabled = true;
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (isRowSelected)
            {
                ButtonAdd.Content = "Редактировать";

                CardNumberTextBox.Text = SessionData.Cards[selectedRowIndex].Number;
                CauseCodeTextBox.Text = SessionData.Cards[selectedRowIndex].CauseCode.ToString();

                ButtonDelete.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Для редактирования данных выберите одну из строк таблицы (Нажмите два раза по нужной Вам строке)");
            }

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isRowSelected)
            {
                MessageBoxResult MessageBoxResult = MessageBox.Show("Вы уверены, что хотите удалить данную строку?", "", MessageBoxButton.YesNo);
                if (MessageBoxResult == MessageBoxResult.Yes)
                {
                    SessionData.Cards.RemoveAt(selectedRowIndex);
                    AppSystem.WorkWithTXT.Remove(selectedRowIndex);
                    InformationUpdate(false);
                    isRowSelected = false;
                }

            }
            else
            {
                MessageBox.Show("Для удаления данных выберите одну из строк таблицы (Нажмите два раза по нужной Вам строке)");
            }
        }

        private void ButtonExportToFile_Click(object sender, RoutedEventArgs e)
        {
            WorkWithTXT.ExportToFile();
        }

        private void CardsDataGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedItem != null)
            {
                selectedRowIndex = dataGrid.SelectedIndex;
                isRowSelected = true;
                MessageBox.Show("Строка выбрана");
            }
        }

        private void TextBoxDataEntry_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
                    TextBox textBox = (TextBox)sender;
                    if (textBox.Name == "CardNumberTextBox")
                    {
                        сardNumberEntered = isRowSelected ? Card.NumberCheck(textBox.Text, selectedRowIndex) : Card.NumberCheck(textBox.Text);

                    }
                    else if (textBox.Name == "CauseCodeTextBox")
                    {
                        if (!сardNumberEntered)
                        {
                            if (CardNumberTextBox.Text == "") MessageBox.Show("Введите номер карты");
                            else сardNumberEntered = isRowSelected ? Card.NumberCheck(CardNumberTextBox.Text, selectedRowIndex) : Card.NumberCheck(CardNumberTextBox.Text);
                        }

                        if (сardNumberEntered)
                        {
                            causeCodeEntered = Cause.CodeCheck(textBox.Text);
                        }
                    }

                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
    }
}
