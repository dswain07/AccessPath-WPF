using System.Windows;
using System.Windows.Controls;
using AccessPath.Data.Models;
using AccessPath.Data.Repositories;

namespace AccessPath.UI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly RouteReportRepository routeReportRepository;
        public AdminWindow()
        {
            InitializeComponent();

            routeReportRepository = new RouteReportRepository();

            RouteReportsDataGrid.ItemsSource = routeReportRepository.GetAllRouteReports();

            ReportStatusComboBox.Items.Add("Pending");
            ReportStatusComboBox.Items.Add("Reviewed");
            ReportStatusComboBox.Items.Add("Resolved");
            ReportStatusComboBox.Items.Add("Rejected");
        }

        private void UpdateReportStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (RouteReportsDataGrid.SelectedItem is not RouteReport selectedReport)
            {
                MessageBox.Show("Please select a route report.");
                return;
            }

            if (ReportStatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a report status.");
                return;
            }

            string reportStatus = ReportStatusComboBox.SelectedItem.ToString() ?? string.Empty;

            routeReportRepository.UpdateReportStatus(selectedReport.RouteReportID, reportStatus);

            RouteReportsDataGrid.ItemsSource = routeReportRepository.GetAllRouteReports();

            MessageBox.Show("Report status updated successfully.");
        }

        private void RouteReportsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RouteReportsDataGrid.SelectedItem is RouteReport selectedReport)
            {
                ReportStatusComboBox.SelectedItem = selectedReport.RouteReportStatus;
            }


        }
    }
}
