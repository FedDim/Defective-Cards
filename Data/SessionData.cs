using System.Collections.ObjectModel;

namespace Defective_Cards.Data
{
    public class SessionData
    {
        public static ObservableCollection<Card> Cards { get; set; }

        public static ObservableCollection<Cause> Causes { get; set; }

        public static int StacksCount { get; set; } = 0;

        public static int NumberOfCardsPerSession { get; set; } = 0;
    }
}
