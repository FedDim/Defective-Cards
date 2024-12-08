using Defective_Cards.Data;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;

namespace Defective_Cards.Pages
{
    public partial class DefectiveCardsPage : Page
    {
        public ObservableCollection<Card> Cards { get; set; }

        public DefectiveCardsPage()
        {
            InitializeComponent();
            LimitLoad();
            Cards = CardsLoad(@"..\..\Data\ListOfCards.txt");

            CardsDataGrid.ItemsSource = Cards;

            TotalСards.Text = $"Всего Карт {Cards.Count}";
        }

        void LimitLoad()
        {
            CardNumberTextBox.MaxLength = AppData.TEXTBOX_MAX_LENGTH;
            RejectCodeTextBox.MaxLength = AppData.TEXTBOX_MAX_LENGTH;
        }

        ObservableCollection<Card> CardsLoad(string filePath)
        {
            ObservableCollection<Card> cardsFromFile = new ObservableCollection<Card>();

            using (StreamReader reader = File.OpenText(filePath))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if(parts.Length == 2)
                    {
                        string number = parts[0].Trim();
                        int code = int.Parse(parts[1].Trim());

                        cardsFromFile.Add(new Card(number, code));
                    }
                }
            }

            return cardsFromFile;
        }

    }
}
