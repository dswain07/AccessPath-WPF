using System.Windows;
using System.Windows.Controls;
using AccessPath.Data.Models;
using AccessPath.Data.Repositories;


namespace AccessPath.UI.Views
{
    /// <summary>
    /// Interaction logic for RoutesView.xaml
    /// </summary>
    public partial class RoutesView : UserControl
    {
        private readonly RouteRepository routeRepository;

        private readonly BuildingRepository buildingRepository;
        
        public RoutesView()
        {
            InitializeComponent();

            routeRepository = new RouteRepository();

            buildingRepository = new BuildingRepository();

            RouteDataGrid.ItemsSource = routeRepository.GetAllRoutes();

            var buildings = buildingRepository.GetAllBuildings();

            StartBuildingComboBox.ItemsSource = buildings;
            DestinationBuildingComboBox.ItemsSource = buildings;

            StartBuildingComboBox.DisplayMemberPath = "BuildingName";
            DestinationBuildingComboBox.DisplayMemberPath = "BuildingName";

            StartBuildingComboBox.SelectedValuePath = "BuildingID";
            DestinationBuildingComboBox.SelectedValuePath = "BuildingID";

            RouteStatusComboBox.Items.Add("Active");
            RouteStatusComboBox.Items.Add("Temporarily Closed");
            RouteStatusComboBox.Items.Add("Under Maintenance");
            RouteStatusComboBox.Items.Add("Permanently Closed");
        }

        private void AddRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartBuildingComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a starting building.");
                return;
            }

            if (DestinationBuildingComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a destination building.");
                return;
            }

            int startBuildingID = (int)StartBuildingComboBox.SelectedValue;
            int destinationBuildingID = (int)DestinationBuildingComboBox.SelectedValue;

            if (startBuildingID == destinationBuildingID)
            {
                MessageBox.Show("The starting and destination buildings cannot be the same.");
                return;
            }

            if (!int.TryParse(EstimatedMinutesTextBox.Text, out int estimatedMinutes))
            {
                MessageBox.Show("Please enter valid estimated minutes.");
                return;
            }

            if (estimatedMinutes <= 0)
            {
                MessageBox.Show("Estimated minutes must be greater than zero");
                return;
            }

            if (!decimal.TryParse(RouteDistanceTextBox.Text, out decimal routeDistance))
            {
                MessageBox.Show("Please enter valid route distance.");
                return;
            }

            if (routeDistance <= 0)
            {
                MessageBox.Show("Route distance must be greater than zero");
                return;
            }

            if (RouteStatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a route status.");
                return;
            }

            string routeStatus = RouteStatusComboBox.SelectedItem.ToString()!;

            Route route = new Route();

            route.EstimatedMinutes = estimatedMinutes;
            route.RouteDistance = routeDistance;
            route.RouteStatus = routeStatus;
            route.StartBuildingID = startBuildingID;
            route.DestinationBuildingID = destinationBuildingID;

            if (string.IsNullOrWhiteSpace(RouteDescriptionTextBox.Text))
            {
                route.RouteDescription = null;
            }
            else
            {
                route.RouteDescription = RouteDescriptionTextBox.Text;
            }

            routeRepository.AddRoute(route);

            RouteDataGrid.ItemsSource = routeRepository.GetAllRoutes();

            MessageBox.Show("Route added successfully.");

            StartBuildingComboBox.SelectedIndex = -1;
            DestinationBuildingComboBox.SelectedIndex = -1;
            EstimatedMinutesTextBox.Clear();
            RouteDistanceTextBox.Clear();
            RouteStatusComboBox.SelectedIndex = -1;
            RouteDescriptionTextBox.Clear();

        }

        private void UpdateRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RouteDataGrid.SelectedItem is not Route selectedRoute)
            {
                MessageBox.Show("Please select a route to update.");
                return;
            }

            if (StartBuildingComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a starting building.");
                return;
            }

            if (DestinationBuildingComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a destination building.");
                return;
            }

            int startBuildingID = (int)StartBuildingComboBox.SelectedValue;
            int destinationBuildingID = (int)DestinationBuildingComboBox.SelectedValue;

            if (startBuildingID == destinationBuildingID)
            {
                MessageBox.Show("The starting and destination buildings cannot be the same.");
                return;
            }

            if (!int.TryParse(EstimatedMinutesTextBox.Text, out int estimatedMinutes))
            {
                MessageBox.Show("Please enter valid estimated minutes.");
                return;
            }

            if (estimatedMinutes <= 0)
            {
                MessageBox.Show("Estimated minutes must be greater than zero");
                return;
            }

            if (!decimal.TryParse(RouteDistanceTextBox.Text, out decimal routeDistance))
            {
                MessageBox.Show("Please enter valid route distance.");
                return;
            }

            if (routeDistance <= 0)
            {
                MessageBox.Show("Route distance must be greater than zero");
                return;
            }

            if (RouteStatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a route status.");
                return;
            }

            Route route = new Route();

            route.RouteID = selectedRoute.RouteID;
            route.EstimatedMinutes = estimatedMinutes;
            route.RouteDistance = routeDistance;
            route.RouteStatus = RouteStatusComboBox.SelectedItem.ToString()!;
            route.StartBuildingID = startBuildingID;
            route.DestinationBuildingID = destinationBuildingID;

            if (string.IsNullOrWhiteSpace(RouteDescriptionTextBox.Text))
            {
                route.RouteDescription = null;
            }
            else
            {
                route.RouteDescription = RouteDescriptionTextBox.Text;
            }

            routeRepository.UpdateRoute(route);

            RouteDataGrid.ItemsSource = routeRepository.GetAllRoutes();

            MessageBox.Show("Route updated successfully.");

        }

        private void DeleteRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RouteDataGrid.SelectedItem is not Route selectedRoute)
            {
                MessageBox.Show("Please select a route to delete.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete the selected route?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                routeRepository.DeleteRoute(selectedRoute.RouteID);

                RouteDataGrid.ItemsSource = routeRepository.GetAllRoutes();

                MessageBox.Show("Route deleted successfully.");
            }
        }

        private void RouteDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RouteDataGrid.SelectedItem is Route selectedRoute)
            {
                StartBuildingComboBox.SelectedValue = selectedRoute.StartBuildingID;

                DestinationBuildingComboBox.SelectedValue = selectedRoute.DestinationBuildingID;

                EstimatedMinutesTextBox.Text = selectedRoute.EstimatedMinutes.ToString();

                RouteDistanceTextBox.Text = selectedRoute.RouteDistance.ToString();

                RouteStatusComboBox.SelectedItem = selectedRoute.RouteStatus;

                RouteDescriptionTextBox.Text = selectedRoute.RouteDescription ?? string.Empty;
            }
        }
    }
}
