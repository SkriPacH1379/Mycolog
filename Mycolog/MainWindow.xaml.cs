using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic; 

namespace Mycolog
{
    public partial class MainWindow : Window
    {
        private readonly MycologContext _context = new MycologContext();
        private MushroomCulture _selectedCulture;

        public MainWindow()
        {
            Database.SetInitializer(new DatabaseInitializer());
            InitializeComponent();
            InitializeDictionaries();
            LoadCultures();
        }

        private void InitializeDictionaries()
        {
            cmbSpecies.ItemsSource = DataDictionaries.Species;
            cmbStrain.ItemsSource = DataDictionaries.Strains;
            cmbSubstrate.ItemsSource = DataDictionaries.Substrates;
            cmbContainer.ItemsSource = DataDictionaries.Containers;
            cmbStage.ItemsSource = DataDictionaries.Stages;

            cmbSpecies.SelectedIndex = 0;
            cmbStrain.SelectedIndex = 0;
            cmbSubstrate.SelectedIndex = 0;
            cmbContainer.SelectedIndex = 0;
            cmbStage.SelectedIndex = 0;
        }

        private void LoadCultures()
        {
            dgCultures.ItemsSource = _context.Cultures
                .OrderByDescending(c => c.StartDate)
                .ToList();
        }

        // ==================== Управление списками ====================

        private void ManageSpecies_Click(object sender, RoutedEventArgs e) => ManageList("Вид гриба", DataDictionaries.Species, DataDictionaries.AddSpecies, DataDictionaries.RemoveSpecies);
        private void ManageStrains_Click(object sender, RoutedEventArgs e) => ManageList("Штамм", DataDictionaries.Strains, DataDictionaries.AddStrain, DataDictionaries.RemoveStrain);
        private void ManageSubstrates_Click(object sender, RoutedEventArgs e) => ManageList("Субстрат", DataDictionaries.Substrates, DataDictionaries.AddSubstrate, DataDictionaries.RemoveSubstrate);
        private void ManageContainers_Click(object sender, RoutedEventArgs e) => ManageList("Ёмкость", DataDictionaries.Containers, DataDictionaries.AddContainer, DataDictionaries.RemoveContainer);
        private void ManageStages_Click(object sender, RoutedEventArgs e) => ManageList("Стадия", DataDictionaries.Stages, DataDictionaries.AddStage, DataDictionaries.RemoveStage);

        private void ManageList(string title, List<string> list, Action<string> addMethod, Func<string, bool> removeMethod)
        {
            string action = Interaction.InputBox($"Введите новое значение для {title} (или оставьте пустым для отмены):", $"Управление — {title}", "");

            if (!string.IsNullOrWhiteSpace(action))
            {
                addMethod(action);
                RefreshAllComboBoxes();
                MessageBox.Show($"Значение '{action}' добавлено!", "Успешно");
            }
        }

        private void RefreshAllComboBoxes()
        {
            cmbSpecies.ItemsSource = null;
            cmbStrain.ItemsSource = null;
            cmbSubstrate.ItemsSource = null;
            cmbContainer.ItemsSource = null;
            cmbStage.ItemsSource = null;

            InitializeDictionaries();
        }

        // ==================== Основные методы ====================

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSpecies.Text))
            {
                MessageBox.Show("Вид гриба обязателен!", "Ошибка");
                return;
            }

            string species = cmbSpecies.Text.Trim();
            string strain = cmbStrain.Text?.Trim() ?? "";
            string substrate = cmbSubstrate.Text?.Trim() ?? "";
            string container = cmbContainer.Text?.Trim() ?? "";

            int? temperature = null;
            if (int.TryParse(txtTemp.Text, out int t))
                temperature = t;

            int? humidity = null;
            if (int.TryParse(txtHumidity.Text, out int h))
                humidity = h;

            decimal? harvestWeight = null;
            if (decimal.TryParse(txtHarvestWeight.Text, out decimal w))
                harvestWeight = w;

            // Добавляем в словари
            DataDictionaries.AddSpecies(species);
            DataDictionaries.AddStrain(strain);
            DataDictionaries.AddSubstrate(substrate);
            DataDictionaries.AddContainer(container);
            DataDictionaries.AddStage(cmbStage.Text);

            if (_selectedCulture == null)
            {
                var culture = new MushroomCulture
                {
                    Species = species,
                    Strain = strain,
                    StartDate = dpStartDate.SelectedDate ?? DateTime.Now,
                    Substrate = substrate,
                    Container = container,
                    CurrentStage = cmbStage.Text ?? "Мицелий",
                    Temperature = temperature,
                    Humidity = humidity,
                    Notes = txtNotes.Text ?? "",
                    HarvestWeight = harvestWeight
                };
                _context.Cultures.Add(culture);
            }
            else
            {
                _selectedCulture.Species = species;
                _selectedCulture.Strain = strain;
                _selectedCulture.StartDate = dpStartDate.SelectedDate ?? _selectedCulture.StartDate;
                _selectedCulture.Substrate = substrate;
                _selectedCulture.Container = container;
                _selectedCulture.CurrentStage = cmbStage.Text ?? _selectedCulture.CurrentStage;
                _selectedCulture.Temperature = temperature;
                _selectedCulture.Humidity = humidity;
                _selectedCulture.Notes = txtNotes.Text ?? "";
                _selectedCulture.HarvestWeight = harvestWeight;
            }

            _context.SaveChanges();
            ClearForm();
            LoadCultures();
        }

        // Edit, Delete, ClearForm — оставил как раньше (с небольшими правками)
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgCultures.SelectedItem is MushroomCulture culture)
            {
                _selectedCulture = culture;
                cmbSpecies.Text = culture.Species;
                cmbStrain.Text = culture.Strain;
                dpStartDate.SelectedDate = culture.StartDate;
                cmbSubstrate.Text = culture.Substrate;
                cmbContainer.Text = culture.Container;
                cmbStage.Text = culture.CurrentStage;
                txtTemp.Text = culture.Temperature?.ToString() ?? "";
                txtHumidity.Text = culture.Humidity?.ToString() ?? "";
                txtHarvestWeight.Text = culture.HarvestWeight?.ToString() ?? "";
                txtNotes.Text = culture.Notes;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgCultures.SelectedItem is MushroomCulture culture)
            {
                if (MessageBox.Show($"Удалить культуру '{culture.Species}'?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Cultures.Remove(culture);
                    _context.SaveChanges();
                    LoadCultures();
                }
            }
        }

        private void ClearForm()
        {
            _selectedCulture = null;
            txtTemp.Clear();
            txtHumidity.Clear();
            txtHarvestWeight.Clear();
            txtNotes.Clear();

            cmbSpecies.SelectedIndex = 0;
            cmbStrain.SelectedIndex = 0;
            cmbSubstrate.SelectedIndex = 0;
            cmbContainer.SelectedIndex = 0;
            cmbStage.SelectedIndex = 0;
        }

        private void dgCultures_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}