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
using System.Windows.Threading;

namespace HelpdeskMain.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoggedIn.xaml
    /// </summary>
    public partial class LoggedIn : Page
    {
        private DispatcherTimer timer;
        public ContextMenu UserMenuDisplayed;

        public LoggedIn()
        {
            InitializeComponent();
            UserMenuDisplayed = (ContextMenu)this.Resources["MenuDisplayed"];
        
            MainFrame.NavigationService.Navigated += MainFrame_Navigated;
        }

    private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        // Close the menu when navigation occurs
        UserMenuDisplayed.IsOpen = false;
    }

    private void MenuOpen(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {


                // Create Timer
                timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };

                //Subscribe Timer to tick event/function
                timer.Tick += Timer_Tick;

                //Start Timer
                timer.Start();




            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            // Check if timer is not null before attempting to stop it
            if (timer != null)
            {
                // Stop Timer
                timer.Stop();

                // Print Menu/Pop-Up Menu
                UserMenuDisplayed.IsOpen = true;

                // Unsubscribe from event/function
                timer.Tick -= Timer_Tick;

                // Set Timer to null
                timer = null;
            }

        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            TicketPage ticketPage = new TicketPage();
            MainFrame.Content = ticketPage;
        }

        private void PageNotYetSet(object sender, RoutedEventArgs e)
        {
            ErrorPage errorpage = new ErrorPage();
            MainFrame.Content = errorpage;
        }

        private void ProfileHeadClick(object sender, RoutedEventArgs e)
        {
            ProfileHead.Visibility = Visibility.Collapsed;
            PHPanel.Visibility = Visibility.Visible;

            // Check if the user is logged in
            if (UserSession.LoggedInUserEmail != null)
            {
                // User is logged in, show LogoutButton
                LogoutButton.Visibility = Visibility.Visible;
                LoginButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                // User is not logged in, show LoginButton
                LogoutButton.Visibility = Visibility.Collapsed;
                LoginButton.Visibility = Visibility.Visible;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle login logic
            // For example, navigate to the login page
            LoginPage loginPage = new LoginPage();
            MainFrame.NavigationService.Navigate(loginPage);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            UserSession.ClearLoggedInUser();
            MessageBox.Show("Successfully logged out");

            // Create an instance of the page you want to navigate to
            LoginPage loginPage = new LoginPage();
            MainFrame.NavigationService.Navigate(loginPage);



        }

    }
}
