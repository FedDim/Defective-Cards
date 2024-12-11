using System.Linq;
using System.Windows;

namespace Defective_Cards.Data
{
    public class Card
    {
        public string Number { get; set; }

        public int CauseCode { get; set; }

        public Card(string number, int code)
        {
            this.Number = number;
            this.CauseCode = code;
        }

        public static bool NumberCheck(string number)
        {
            if (number != "")
            {
                string digits = number.Trim().Replace(" ", "");

                if (SessionData.Cards.Any(card => card.Number.Equals(digits)))
                {
                    MessageBox.Show("Данный номер Карты уже имеется");
                    return false;
                }

                bool algorithmApplicable = digits.All(char.IsDigit) && digits.Reverse()
                    .Select(c => c - 48)
                    .Select((thisNum, i) => i % 2 == 0
                        ? thisNum
                        : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                    ).Sum() % 10 == 0;

                if (!algorithmApplicable)
                {
                    MessageBox.Show("Карта введена не верно. Проверьте правильность введенных данных");
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool NumberCheck(string number, int index)
        {
            if (number != "")
            {
                string digits = number.Trim().Replace(" ", "");

                if (SessionData.Cards.Any(card => card.Number.Equals(digits) && !card.Number.Equals(SessionData.Cards[index].Number)))
                {
                    MessageBox.Show("Данный номер Карты уже имеется");
                    return false;
                }

                bool algorithmApplicable = digits.All(char.IsDigit) && digits.Reverse()
                    .Select(c => c - 48)
                    .Select((thisNum, i) => i % 2 == 0
                        ? thisNum
                        : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                    ).Sum() % 10 == 0;

                if (!algorithmApplicable)
                {
                    MessageBox.Show("Карта введена не верно. Проверьте правильность введенных данных");
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
