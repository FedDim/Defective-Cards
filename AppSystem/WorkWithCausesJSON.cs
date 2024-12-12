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
                MessageBoxResult messageBoxResult = MessageBox.Show($"Файл {name} не обнаружен, хотите использовать даннные по-умолчанию?", "", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    causes = new List<Cause>()
                    {
                        new Cause(1, "Механические повреждения"),
                        new Cause(2, "Электронный сбой"),
                        new Cause(3, "Ошибки при записи данных"),
                        new Cause(4, "Истечение срока годности"),
                        new Cause(5, "Проблемы с программным обеспечением")
                    };
                }
                else MessageBox.Show("Без этих данных программа будет работать не корректно!");
            }
        }

        public static void Serialization_CauseData(ObservableCollection<Cause> causes, string name)
        {
            try
            {
                File.WriteAllText(name, JsonConvert.SerializeObject(causes));
            }
            catch
            {
                MessageBox.Show("Ошибка сериализации");
            }
        }

        public static void Delete()
        {

        }
    }
}
