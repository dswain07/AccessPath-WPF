using System.Windows;
using AccessPath.Data.Repositories;
using AccessPath.Data.Models;
using System.Windows.Controls;

namespace AccessPath.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BuildingRepository repository;
        public MainWindow()
        {
            InitializeComponent();

            repository = new BuildingRepository();

            BuildingDataGrid.ItemsSource = repository.GetAllBuildings();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text;

            BuildingDataGrid.ItemsSource = repository.SearchBuildings(searchTerm);
        }

        private void RoutesButton_Click(object sender, RoutedEventArgs e)
        {
            RouteWindow routeWindow = new RouteWindow();
            routeWindow.Show();
        }

        private void AccessibilityButton_Click(object sender, RoutedEventArgs e)
        {
            BuildingAccessibilityWindow accessibilityWindow = new BuildingAccessibilityWindow();

            accessibilityWindow.Show();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow settingsWindow = new SettingWindow();

            settingsWindow.Show();
        }

        private void AddBuildingButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(BuildingNameTextBox.Text))
            {
                MessageBox.Show("Please enter a building name.");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(BuildingTypeTextBox.Text))
            {
                MessageBox.Show("Please enter a building type.");
                return;
            }

            TimeSpan? openingHours = null;
            if (!string.IsNullOrWhiteSpace(OpeningHoursTextBox.Text))
            {
                if (!TimeSpan.TryParse(OpeningHoursTextBox.Text, out TimeSpan parsedOpeningHours))
                {
                    MessageBox.Show("Please enter opening hours in a valid format, such as 07:30.");
                    return;
                }

                openingHours = parsedOpeningHours;
            }

            TimeSpan? closingHours = null;
            if (!string.IsNullOrWhiteSpace(ClosingHoursTextBox.Text))
            {
                if (!TimeSpan.TryParse(ClosingHoursTextBox.Text, out TimeSpan parsedClosingHours))
                {
                    MessageBox.Show("Please enter closing hours in a valid format, such as 23:30.");
                    return;
                }

                closingHours = parsedClosingHours;
            }

            if (!decimal.TryParse(LatitudeTextBox.Text, out decimal latitude))
            {
                MessageBox.Show("Please enter a valid latitude.");
                return;
            }
            
            if (!decimal.TryParse(LongitudeTextBox.Text, out decimal longitude))
            {
                MessageBox.Show("Please enter a valid longitude.");
                return;
            }
            
            if (!int.TryParse(AddressIDTextBox.Text, out int addressID))
            {
                MessageBox.Show("Please enter a valid Address ID.");
                return;
            }

            Building building = new Building();
            building.BuildingName = BuildingNameTextBox.Text;
            building.BuildingType = BuildingTypeTextBox.Text;
            building.OpeningHours = openingHours;
            building.ClosingHours = closingHours;
            building.BuildingLatitude = latitude;
            building.BuildingLongitude = longitude;
            building.AddressID = addressID;

            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                building.BuildingDescription = null;
            }
            else
            {
                building.BuildingDescription = DescriptionTextBox.Text;
            }

            repository.AddBuilding(building);

            BuildingDataGrid.ItemsSource = repository.GetAllBuildings();

            MessageBox.Show("Building added successfully.");

            BuildingNameTextBox.Clear();
            BuildingTypeTextBox.Clear();
            OpeningHoursTextBox.Clear();
            ClosingHoursTextBox.Clear();
            LatitudeTextBox.Clear();
            LongitudeTextBox.Clear();
            DescriptionTextBox.Clear();
            AddressIDTextBox.Clear();
        }

        private void BuildingDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuildingDataGrid.SelectedItem is Building selectedBuilding)
            {
                BuildingNameTextBox.Text = selectedBuilding.BuildingName;

                BuildingTypeTextBox.Text = selectedBuilding.BuildingType;

                OpeningHoursTextBox.Text = selectedBuilding.OpeningHours?.ToString(@"hh\:mm") ?? string.Empty;

                ClosingHoursTextBox.Text = selectedBuilding.ClosingHours?.ToString(@"hh\:mm") ?? string.Empty;

                LatitudeTextBox.Text = selectedBuilding.BuildingLatitude.ToString();

                LongitudeTextBox.Text = selectedBuilding.BuildingLongitude.ToString();

                DescriptionTextBox.Text = selectedBuilding.BuildingDescription ?? string.Empty;

                AddressIDTextBox.Text = selectedBuilding.AddressID.ToString();

            }
        }

        private void UpdateBuildingButton_Click(object sender, RoutedEventArgs e)
        {
            if (BuildingDataGrid.SelectedItem is not Building selectedBuilding)
            {
                MessageBox.Show("Please select a building to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(BuildingNameTextBox.Text))
            {
                MessageBox.Show("Please enter a building name.");
                return;
            }

            if (string.IsNullOrWhiteSpace(BuildingTypeTextBox.Text))
            {
                MessageBox.Show("Please enter a building type.");
                return;
            }

            TimeSpan? openingHours = null;
            if (!string.IsNullOrWhiteSpace(OpeningHoursTextBox.Text))
            {
                if (!TimeSpan.TryParse(OpeningHoursTextBox.Text, out TimeSpan parsedOpeningHours))
                {
                    MessageBox.Show("Please enter opening hours in a valid format, such as 07:30.");
                    return;
                }

                openingHours = parsedOpeningHours;
            }

            TimeSpan? closingHours = null;
            if (!string.IsNullOrWhiteSpace(ClosingHoursTextBox.Text))
            {
                if (!TimeSpan.TryParse(ClosingHoursTextBox.Text, out TimeSpan parsedClosingHours))
                {
                    MessageBox.Show("Please enter closing hours in a valid format, such as 23:30.");
                    return;
                }

                closingHours = parsedClosingHours;
            }

            if (!decimal.TryParse(LatitudeTextBox.Text, out decimal latitude))
            {
                MessageBox.Show("Please enter a valid latitude.");
                return;
            }

            if (!decimal.TryParse(LongitudeTextBox.Text, out decimal longitude))
            {
                MessageBox.Show("Please enter a valid longitude.");
                return;
            }

            if (!int.TryParse(AddressIDTextBox.Text, out int addressID))
            {
                MessageBox.Show("Please enter a valid Address ID.");
                return;
            }

            Building building = new Building();
            building.BuildingID = selectedBuilding.BuildingID;
            building.BuildingName = BuildingNameTextBox.Text;
            building.BuildingType = BuildingTypeTextBox.Text;
            building.OpeningHours = openingHours;
            building.ClosingHours = closingHours;
            building.BuildingLatitude = latitude;
            building.BuildingLongitude = longitude;
            building.AddressID = addressID;

            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                building.BuildingDescription = null;
            }
            else
            {
                building.BuildingDescription = DescriptionTextBox.Text;
            }

            repository.UpdateBuilding(building);

            BuildingDataGrid.ItemsSource = repository.GetAllBuildings();

            MessageBox.Show("Building updated successfully.");
        }

        private void DeleteBuildingButton_Click(object sender, RoutedEventArgs e)
        {
            if (BuildingDataGrid.SelectedItem is not Building selectedBuilding)
            {
                MessageBox.Show("Please select a building to delete.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure you want to delete {selectedBuilding.BuildingName}?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                repository.DeleteBuilding(selectedBuilding.BuildingID);

                BuildingDataGrid.ItemsSource = repository.GetAllBuildings();

                MessageBox.Show("Building deleted successfully.");
            }
        }
    }
}