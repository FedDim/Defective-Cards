using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Defective_Cards.Data
{
    public class SessionData
    {
        public static ObservableCollection<Card> Cards { get; set; }

        public static List<Cause> Causes;

        public static int StacksCount { get; set; } = 0;
    }
}
