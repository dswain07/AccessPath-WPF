using System.Windows;
using System.Windows.Controls;
using AccessPath.Data.Models;
using AccessPath.Data.Repositories;

namespace AccessPath.UI.Views
{
    /// <summary>
    /// Interaction logic for BuildingsView.xaml
    /// </summary>
    public partial class BuildingsView : UserControl
    {
        private readonly BuildingRepository repository;
        private readonly AddressRepository addressRepository;
        public BuildingsView()
        {
            InitializeComponent();

            repository = new BuildingRepository();
            addressRepository = new AddressRepository();

            BuildingDataGrid.ItemsSource = repository.GetAllBuildings();

            var addresses = addressRepository.GetAllAddresses();

            AddressComboBox.ItemsSource = addresses;
            AddressComboBox.DisplayMemberPath = "DisplayAddress";
            AddressComboBox.SelectedValuePath = "AddressID";
        }

        private void ClearBuildingForm()
        {
            BuildingNameTextBox.Clear();
            BuildingTypeTextBox.Clear();
            OpeningHoursTextBox.Clear();
            ClosingHoursTextBox.Clear();
            LatitudeTextBox.Clear();
            LongitudeTextBox.Clear();
            DescriptionTextBox.Clear();

            AddressComboBox.SelectedIndex = -1;

            BuildingDataGrid.SelectedItem = null;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text;

            BuildingDataGrid.ItemsSource = repository.SearchBuildings(searchTerm);
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

            if (AddressComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an address.");
                return;
            }

            int addressID = (int)AddressComboBox.SelectedValue;

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

            ClearBuildingForm();
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

                AddressComboBox.SelectedValue = selectedBuilding.AddressID;

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

            if (AddressComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an address.");
                return;
            }

            int addressID = (int)AddressComboBox.SelectedValue;

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

                ClearBuildingForm();

                MessageBox.Show("Building deleted successfully.");
            }
        }
    }
}
