using System.Windows;
using System.Windows.Controls;
using AccessPath.Data.Models;
using AccessPath.Data.Repositories;

namespace AccessPath.UI.Views
{
    /// <summary>
    /// Interaction logic for AccessibilityView.xaml
    /// </summary>
    public partial class AccessibilityView : UserControl
    {
        private readonly BuildingAccessibilityRepository accessibilityRepository;
        private readonly BuildingRepository buildingRepository;
        public AccessibilityView()
        {
            InitializeComponent();

            accessibilityRepository = new BuildingAccessibilityRepository();

            buildingRepository = new BuildingRepository();

            AccessibilityDataGrid.ItemsSource = accessibilityRepository.GetAllAccessibility();

            var buildings = buildingRepository.GetAllBuildings();

            BuildingComboBox.ItemsSource = buildings;
            BuildingComboBox.DisplayMemberPath = "BuildingName";
            BuildingComboBox.SelectedValuePath = "BuildingID";
        }

        private void AddAccessibilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (BuildingComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a building.");
                return;
            }

            if (string.IsNullOrWhiteSpace(AccessibilityFeatureTextBox.Text))
            {
                MessageBox.Show("Please enter an accessibility feature.");
                return;
            }

            int buildingID = (int)BuildingComboBox.SelectedValue;

            BuildingAccessibility accessibility = new BuildingAccessibility();

            accessibility.BuildingID = buildingID;
            accessibility.AccessibilityFeature = AccessibilityFeatureTextBox.Text;

            if (string.IsNullOrWhiteSpace(AccessibilityDescriptionTextBox.Text))
            {
                accessibility.AccessibilityDescription = null;
            }
            else
            {
                accessibility.AccessibilityDescription = AccessibilityDescriptionTextBox.Text;
            }

            accessibilityRepository.AddAccessibility(accessibility);

            AccessibilityDataGrid.ItemsSource = accessibilityRepository.GetAllAccessibility();

            MessageBox.Show("Accessibility feature added successfully.");

            BuildingComboBox.SelectedIndex = -1;
            AccessibilityFeatureTextBox.Clear();
            AccessibilityDescriptionTextBox.Clear();
        }


        private void UpdateAccessibilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccessibilityDataGrid.SelectedItem is not BuildingAccessibility selectedAccessibility)
            {
                MessageBox.Show("Please select an accessibility feature to update.");
                return;
            }

            if (BuildingComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a building.");
                return;
            }

            if (string.IsNullOrWhiteSpace(AccessibilityFeatureTextBox.Text))
            {
                MessageBox.Show("Please enter an accessibility feature.");
                return;
            }

            int buildingID = (int)BuildingComboBox.SelectedValue;

            BuildingAccessibility accessibility = new BuildingAccessibility();

            accessibility.AccessibilityID = selectedAccessibility.AccessibilityID;
            accessibility.BuildingID = buildingID;
            accessibility.AccessibilityFeature = AccessibilityFeatureTextBox.Text;

            if (string.IsNullOrWhiteSpace(AccessibilityDescriptionTextBox.Text))
            {
                accessibility.AccessibilityDescription = null;
            }
            else
            {
                accessibility.AccessibilityDescription = AccessibilityDescriptionTextBox.Text;
            }

            accessibilityRepository.UpdateAccessibility(accessibility);

            AccessibilityDataGrid.ItemsSource = accessibilityRepository.GetAllAccessibility();

            MessageBox.Show("Accessibility feature updated successfully.");
        }


        private void DeleteAccessibilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccessibilityDataGrid.SelectedItem is not BuildingAccessibility selectedAccessibility)
            {
                MessageBox.Show("Please select an accessibility feature to delete.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"Are you sure you want to delete " +
                $"{selectedAccessibility.AccessibilityFeature}?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                accessibilityRepository.DeleteAccessibility(selectedAccessibility.AccessibilityID);

                AccessibilityDataGrid.ItemsSource = accessibilityRepository.GetAllAccessibility();

                MessageBox.Show("Accessibility feature deleted successfully.");
            }
        }

        private void AccessibilityDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccessibilityDataGrid.SelectedItem is BuildingAccessibility selectedAccessibility)
            {
                BuildingComboBox.SelectedValue = selectedAccessibility.BuildingID;

                AccessibilityFeatureTextBox.Text = selectedAccessibility.AccessibilityFeature;

                AccessibilityDescriptionTextBox.Text = selectedAccessibility.AccessibilityDescription ?? string.Empty;
            }
        }
    }
}
