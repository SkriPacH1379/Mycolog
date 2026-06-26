using System.Collections.Generic;

namespace Mycolog
{
    public static class DataDictionaries
    {
        public static List<string> Species { get; } = new List<string>
        {
            "Вешенка", "Рейши", "Шампиньон", "Львиная грива", "Гериций", "Шиитаке", "Опёнок"
        };

        public static List<string> Strains { get; } = new List<string>
        {
            "Pink Oyster", "Ganoderma lucidum", "Hericium erinaceus", "Белый", "Голубая"
        };

        public static List<string> Substrates { get; } = new List<string>
        {
            "Солома", "Опилки дуба", "Опилки бука", "Зерно", "Компост", "Кофейный жмых"
        };

        public static List<string> Containers { get; } = new List<string>
        {
            "Мешок 5л", "Мешок 10л", "Банка 720мл", "Банка 1л", "Ящик"
        };

        public static List<string> Stages { get; } = new List<string>
        {
            "Мицелий", "Плодоношение", "Сбор", "Завершено"
        };

        // Методы добавления
        public static void AddSpecies(string item) => AddToList(Species, item);
        public static void AddStrain(string item) => AddToList(Strains, item);
        public static void AddSubstrate(string item) => AddToList(Substrates, item);
        public static void AddContainer(string item) => AddToList(Containers, item);
        public static void AddStage(string item) => AddToList(Stages, item);

        private static void AddToList(List<string> list, string item)
        {
            if (!string.IsNullOrWhiteSpace(item) && !list.Contains(item))
                list.Add(item);
        }

        // Методы удаления
        public static bool RemoveSpecies(string item) => Species.Remove(item);
        public static bool RemoveStrain(string item) => Strains.Remove(item);
        public static bool RemoveSubstrate(string item) => Substrates.Remove(item);
        public static bool RemoveContainer(string item) => Containers.Remove(item);
        public static bool RemoveStage(string item) => Stages.Remove(item);
    }
}