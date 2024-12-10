using Defective_Cards.Data;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Defective_Cards.Pages
{
    public partial class DefectiveCardsPage : Page
    {

        bool сardNumberEntered = false, causeCodeEntered = false;

        public DefectiveCardsPage()
        {
            InitializeComponent();
            LimitLoad();
            SessionData.Cards = CardsLoad(@"..\..\Data\ListOfCards.txt");

            CardsDataGrid.ItemsSource = SessionData.Cards;

            Cause.Deserialization_CauseData(ref SessionData.Causes, "CauseData.json");

            informationUpdate(true);
        }

        void LimitLoad()
        {
            CardNumberTextBox.MaxLength = AppData.TEXTBOX_MAX_LENGTH;
            CauseCodeTextBox.MaxLength = AppData.TEXTBOX_MAX_LENGTH;
        }

        void informationUpdate(bool isLoad)
        {
            TotalСards.Text = $"Всего Карт {SessionData.Cards.Count}";

            if (SessionData.Cards.Count / AppData.IN_STACK - SessionData.StacksCount == 1)
            {
                SessionData.StacksCount++;
                if (!isLoad) MessageBox.Show("Новая стопка готова");
            }

            NumberOfStacks.Text = $"Количество стопок {SessionData.StacksCount}";
        }

        ObservableCollection<Card> CardsLoad(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                return new ObservableCollection<Card>();
            }

            ObservableCollection<Card> cardsFromFile = new ObservableCollection<Card>();

            using (StreamReader reader = File.OpenText(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        string number = parts[0].Trim();
                        int code = int.Parse(parts[1].Trim());

                        cardsFromFile.Add(new Card(number, code));
                    }
                }
            }

            return cardsFromFile;


        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ButtonAdd.IsEnabled = false;

            while (true)
            {
                if (сardNumberEntered && causeCodeEntered)
                {
                    SessionData.Cards.Add(new Card(CardNumberTextBox.Text.Trim().Replace(" ", ""), Int32.Parse(CauseCodeTextBox.Text)));
                    CardsDataGrid.ItemsSource = SessionData.Cards;

                    CardNumberTextBox.Text = CauseCodeTextBox.Text = "";

                    informationUpdate(false);

                    сardNumberEntered = causeCodeEntered = false;
                    break;
                }

                if (!сardNumberEntered)
                {
                    сardNumberEntered = Card.NumberCheck(CardNumberTextBox.Text);

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
                        сardNumberEntered = Card.NumberCheck(textBox.Text);
                    }
                    else if (textBox.Name == "CauseCodeTextBox")
                    {
                        if (!сardNumberEntered)
                        {
                            if (CardNumberTextBox.Text == "") MessageBox.Show("Введите номер карты");
                            else сardNumberEntered = Card.NumberCheck(CardNumberTextBox.Text);
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
