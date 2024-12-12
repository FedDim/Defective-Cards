using Defective_Cards.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Defective_Cards.AppSystem
{
    public static class WorkWithTXT
    {
        public static ObservableCollection<Card> CardsLoad()
        {
            if (!File.Exists(AppData.LISTOFCARDS_FILEPATH))
            {
                File.Create(AppData.LISTOFCARDS_FILEPATH).Dispose();
                return new ObservableCollection<Card>();
            }

            ObservableCollection<Card> cardsFromFile = new ObservableCollection<Card>();

            try
            {
                using (StreamReader reader = File.OpenText(AppData.LISTOFCARDS_FILEPATH))
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при работе с файлом: {ex.Message}");
            }


            return cardsFromFile;


        }

        public static void Add()
        {
            string newLine = $"{SessionData.Cards[SessionData.Cards.Count - 1].Number} | {SessionData.Cards[SessionData.Cards.Count - 1].CauseCode}";

            try
            {
                using (StreamWriter writer = new StreamWriter(Data.AppData.LISTOFCARDS_FILEPATH, true))
                {
                    writer.WriteLine(newLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
            }
        }

        public static void Edit(int index)
        {
            string newLine = $"{SessionData.Cards[index].Number} | {SessionData.Cards[index].CauseCode}";

            try
            {
                List<string> lines = new List<string>(File.ReadAllLines(AppData.LISTOFCARDS_FILEPATH));

                lines[index] = newLine;

                File.WriteAllLines(AppData.LISTOFCARDS_FILEPATH, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при работе с файлом: {ex.Message}");
            }
        }

        public static void Remove(int index)
        {
            try
            {
                List<string> lines = new List<string>(File.ReadAllLines(AppData.LISTOFCARDS_FILEPATH));

                lines.RemoveAt(index);

                File.WriteAllLines(AppData.LISTOFCARDS_FILEPATH, lines);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при работе с файлом: {ex.Message}");
            }
        }

        public static void ExportToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Выберите место для сохранения",
                Filter = "Текстовые документы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                FileName = "Бракованные Карты"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.Copy(AppData.LISTOFCARDS_FILEPATH, saveFileDialog.FileName, overwrite: true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при сохранении файла: {ex.Message}");
                }
            }
        }
    }
}
