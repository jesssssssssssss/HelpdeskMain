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
    /// Interaction logic for TicketSubmission.xaml
    /// </summary>
    public partial class TicketSubmission : Page
    {
        private Database db;

        public TicketSubmission()
        {
            InitializeComponent();
            db = new Database();
        }

        private void SubmitTicket_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            DateTime date = DatePicker.SelectedDate ?? DateTime.Now; // Use selected date or current date if not selected 
            string issue = IssueTextBox.Text;
            string incidentCategory = ICTextBox.Text;
            string tags = TagsTextBox.Text;
            string urgency = UrgencyTextBox.Text;
            string symptoms = SymptomsTextBox.Text;
            string impact = ImpactTextBox.Text;
            string additionalInfo = AdditionalInfoTextBox.Text;
            string loggedInUserEmail = UserSession.LoggedInUserEmail;

            // Generate a ticket ID (you may use a more sophisticated method)
            string ticketId = "Ticket" + DateTime.Now.Ticks;

            try
            {
                // Save the ticket to the database
                db.InsertTicket(ticketId, name, date, loggedInUserEmail, issue, incidentCategory, tags, urgency, symptoms, impact, additionalInfo);

                MessageBox.Show("Ticket submitted successfully!");
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting ticket: {ex.Message}");
            }
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
