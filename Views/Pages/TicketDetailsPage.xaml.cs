using HelpdeskMain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelpdeskMain.Views.Pages
{
    /// <summary>
    /// Interaction logic for TicketDetailsPage.xaml
    /// </summary>
    public partial class TicketDetailsPage : Page
    {

        private readonly Database db = new Database();

        public TicketDetailsPage(string ticketId)
        {
            InitializeComponent();

            // Fetch the ticket details based on the ticketId
            Ticket selectedTicket = db.GetTicketById(ticketId);

            // Set DataContext to the selected ticket
            DataContext = selectedTicket;

            Debug.WriteLine($"DataContext Type: {DataContext?.GetType().FullName}");
            Debug.WriteLine($"Issue Property Value: {selectedTicket?.Issue}");
            Debug.WriteLine($"Name Property Value: {selectedTicket?.Name}");
        }

        private void BackArrowSPClick(object sender, RoutedEventArgs e)
        {
            TicketPage ticketPage = new TicketPage();
            NavigationService.Navigate(ticketPage);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Successfully logged out");
            LoginPage loginPage = new LoginPage();
            NavigationService.Navigate(loginPage);
        }

        private void ProfileHeadClick(object sender, RoutedEventArgs e)
        {
            ProfileHead.Visibility = Visibility.Collapsed;
            PHPanel.Visibility = Visibility.Visible;
        }

    }
}
