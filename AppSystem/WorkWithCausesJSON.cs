using Defective_Cards.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Defective_Cards.AppSystem
{
    public static class WorkWithCausesJSON
    {
        public static ObservableCollection<Cause> CausesLoad()
        {
            ObservableCollection<Cause> causesFromFile = new ObservableCollection<Cause>();

            List<Cause> causes = new List<Cause>();

            Deserialization_CauseData(ref causes, "CauseData.json");

            for (int i = 0; causes.Count > i; i++)
            {
                causesFromFile.Add(causes[i]);
            }

            return causesFromFile;
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

        public static void Delete()
        {

        }
    }
}
