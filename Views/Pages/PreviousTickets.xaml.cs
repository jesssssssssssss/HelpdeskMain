using HelpdeskMain.Models;
using HelpdeskMain.Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PreviousTickets.xaml
    /// </summary>
    public partial class PreviousTickets : Page
    {

        private readonly Database db = new Database();
        public PreviousTickets()
        {
            InitializeComponent();
            db = new Database();
            LoadPreviousTickets_Click();
        }
        private void LoadPreviousTickets_Click()
        {
            string loggedinuseremail = UserSession.LoggedInUserEmail;
            List<Ticket> tickets = db.GetTicketsByEmail(loggedinuseremail);

            // Set the DataContext of the ItemsControl to your list of tickets
            TicketItemsControl.DataContext = new { Tickets = tickets };

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


        private void TicketTile_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that was clicked
            Button clickedButton = (Button)sender;

            // Get the TicketId from the Tag property
            string ticketId = clickedButton.Tag.ToString();

            // Use the NavigationService to navigate to the detailed view
            NavigationService.Navigate(new TicketDetailsPage(ticketId));
        }

    }
}
