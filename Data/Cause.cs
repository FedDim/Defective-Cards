using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static void Deserialization_CauseData(ref List<Cause> causes, string name)
        {
            try
            {
                causes = JsonConvert.DeserializeObject<List<Cause>>(File.ReadAllText(name));
            }
            catch
            {
                MessageBox.Show($"Файл {name} не обнаружен");
                //Возможность создания файла
            }
        }

        public static void Serialization_CauseData(List<Cause> causes, string name)
        {
            try
            {
                File.WriteAllText(name, JsonConvert.SerializeObject(causes));
                MessageBox.Show("Данные сохранены");
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
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
