using System;
using System.Data.Entity;
using System.Linq;

namespace Mycolog
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<MycologContext>
    {
        protected override void Seed(MycologContext context)
        {
            if (context.Cultures.Any()) return;

            context.Cultures.Add(
                new MushroomCulture
                {
                    Species = "Вешенка",
                    Strain = "Pink Oyster",
                    StartDate = DateTime.Now.AddDays(-14),
                    Substrate = "Солома",
                    Container = "Мешок 5л",
                    CurrentStage = "Мицелий",
                    Temperature = 22,
                    Humidity = 88,
                    Notes = "Первый эксперимент"
                });

            context.Cultures.Add(
                new MushroomCulture
                {
                    Species = "Рейши",
                    Strain = "Ganoderma lucidum",
                    StartDate = DateTime.Now.AddDays(-25),
                    Substrate = "Опилки дуба + зерно",
                    Container = "Банка 720мл",
                    CurrentStage = "Плодоношение",
                    Temperature = 24,
                    Humidity = 75
                });

            context.SaveChanges();
        }
    }
}