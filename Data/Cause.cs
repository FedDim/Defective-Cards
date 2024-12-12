using System;
using System.Linq;
using System.Windows;

namespace Defective_Cards.Data
{
    public class Cause
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public Cause(int code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        public static bool CodeCheck(string text)
        {
            if (Int32.TryParse(text, out int code))
            {
                if (SessionData.Causes.Any(cause => cause.Code == code)) return true;

                MessageBox.Show("Данный Код Брака отсутствует");
            }
            else MessageBox.Show("Неверный формат Кода Брака");

            return false;
        }
    }
}
