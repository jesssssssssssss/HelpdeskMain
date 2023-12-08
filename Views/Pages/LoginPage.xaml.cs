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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private Database db;

        public LoginPage()
        {
            InitializeComponent();
            db = new Database();
            db.CreateDatabase();
            db.CreateTable();

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Password;
            db.InsertUser(email, password);
            MessageBox.Show("Registration successful!");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Password;

            if (email == "Agent@ghub.com" && password == "Agent123")
            {
                AgentView agentView = new AgentView();
                NavigationService.Navigate(agentView);
            }
            else if (email == "Admin@ghub.com" && password == "Admin123")
            {
                AdminView adminView = new AdminView();
                NavigationService.Navigate(adminView);
            }
            else if (!string.IsNullOrEmpty(db.VerifyLogin(email, password)))

            {
                LoggedIn loggedin = new LoggedIn();
                NavigationService.Navigate(loggedin);
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }
    }
}
