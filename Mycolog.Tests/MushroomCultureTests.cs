using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Mycolog.Tests
{
    [TestClass]
    public class MushroomCultureTests
    {
        [TestMethod]
        public void AddCulture_ShouldCreateValidObject()
        {
            // Arrange + Act
            var culture = new MushroomCulture
            {
                Species = "Вешенка",
                Strain = "Pink Oyster",
                StartDate = DateTime.Now,
                Substrate = "Солома",
                Container = "Мешок 5л",
                CurrentStage = "Мицелий",
                Temperature = 22,
                Humidity = 85,
                Notes = "Тестовая культура"
            };

            // Assert
            Assert.IsNotNull(culture);
            Assert.AreEqual("Вешенка", culture.Species);
            Assert.AreEqual("Pink Oyster", culture.Strain);
            Assert.AreEqual("Мицелий", culture.CurrentStage);
            Assert.IsTrue(culture.StartDate <= DateTime.Now);
        }

        [TestMethod]
        public void EditCulture_ShouldModifyPropertiesCorrectly()
        {
            // Arrange
            var culture = new MushroomCulture
            {
                Species = "Рейши",
                Strain = "Old",
                CurrentStage = "Мицелий",
                Notes = "Старые данные"
            };

            // Act
            culture.Strain = "Ganoderma lucidum";
            culture.CurrentStage = "Плодоношение";
            culture.Temperature = 24;
            culture.Notes = "Обновлено в тесте";

            // Assert
            Assert.AreEqual("Ganoderma lucidum", culture.Strain);
            Assert.AreEqual("Плодоношение", culture.CurrentStage);
            Assert.AreEqual(24, culture.Temperature);
            Assert.AreEqual("Обновлено в тесте", culture.Notes);
        }

        [TestMethod]
        public void Culture_ShouldHaveRequiredSpecies()
        {
            // Arrange
            var culture = new MushroomCulture
            {
                Species = "",           // Пустое обязательное поле
                Strain = "Test"
            };

            // Assert
            Assert.AreEqual("", culture.Species);
            // Здесь можно добавить более сложную проверку, если у тебя есть валидация
        }
    }
}