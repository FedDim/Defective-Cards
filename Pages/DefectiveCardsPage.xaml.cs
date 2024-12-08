using Defective_Cards.Data;
using System.Collections.ObjectModel;
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

namespace Defective_Cards.Pages
{
    public partial class DefectiveCardsPage : Page
    {
        public ObservableCollection<Card> Cards { get; set; }

        public DefectiveCardsPage()
        {
            InitializeComponent();
            LimitLoad();
            Cards = new ObservableCollection<Card>
            {
                new Card { Number = "96433078361268504899266604", RejectCode = 0 },
                new Card { Number = "96433078361268506226301483", RejectCode = 0 }
            };

            CardsDataGrid.ItemsSource = Cards;

            TotalСards.Text = $"Всего Карт {Cards.Count}";
        }

        void LimitLoad()
        {
            CardNumberTextBox.MaxLength = AppData.TEXTBOX_MAX_LENGTH;
            RejectCodeTextBox.MaxLength = AppData.TEXTBOX_MAX_LENGTH;
        }

    }
}
