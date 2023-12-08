using HelpdeskMain.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskMain
{
    public class Database
    {
        private const string ConnectionString = "Data Source=MyDatabase.db;Version=3;";

        public void CreateDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                // Create database if not exists
            }
        }

        public void CreateTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS User (Username TEXT PRIMARY KEY, Password TEXT);", connection))
                {
                    command.ExecuteNonQuery();
                }

                // Create Ticket table if not exists
                using (SQLiteCommand createTicketTableCommand = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Ticket (Id INTEGER PRIMARY KEY AUTOINCREMENT, TicketId TEXT, Name TEXT, Date TEXT, Email TEXT, Issue TEXT, IncidentCategory TEXT, Tags TEXT, Urgency TEXT, Symptoms TEXT, Impact TEXT, AdditionalInfo TEXT);", connection))
                {
                    createTicketTableCommand.ExecuteNonQuery();
                }
            }
        }

        public void InsertUser(string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO User (Username, Password) VALUES (@Username, @Password);", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public string VerifyLogin(string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT Username FROM User WHERE Username = @Username AND Password = @Password;", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string userEmail = result.ToString();

                        // Authentication successful, set the logged-in user
                        HelpdeskMain.Services.UserSession.SetLoggedInUser(userEmail);

                        Console.WriteLine($"Logged-in user's email: {userEmail}");

                        return userEmail; // Return the email (username) on successful login
                    }
                    else
                    {
                        return null; // Return null if login fails
                    }
                }
            }
        }




        public void InsertTicket(string ticketId, string name, DateTime date, string email, string issue, string incidentCategory, string tags, string urgency, string symptoms, string impact, string additionalInfo)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Ticket (TicketId, Name, Date, Issue, Email, IncidentCategory, Tags, Urgency,Symptoms, Impact, AdditionalInfo) VALUES (@TicketId, @Name, @Date, @Issue, @Email, @IncidentCategory, @Tags, @Urgency, @Symptoms, @Impact, @AdditionalInfo);", connection))
                {
                    command.Parameters.AddWithValue("@TicketId", ticketId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd HH:mm"));
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Issue", issue);
                    command.Parameters.AddWithValue("@IncidentCategory", incidentCategory);
                    command.Parameters.AddWithValue("@Tags", tags);
                    command.Parameters.AddWithValue("@Urgency", urgency);
                    command.Parameters.AddWithValue("@Symptoms", symptoms);
                    command.Parameters.AddWithValue("@Impact", impact);
                    command.Parameters.AddWithValue("@AdditionalInfo", additionalInfo);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Models.Ticket> GetAllTickets()
        {
            List<Models.Ticket> tickets = new List<Models.Ticket>();
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Ticket";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Ticket ticket = new Models.Ticket
                            {
                                Id = reader.GetOrdinal("Id") != -1 ? Convert.ToInt32(reader["Id"]) : 0,
                                TicketId = reader.GetOrdinal("TicketId") != -1 ? reader["TicketId"].ToString() : string.Empty,
                                Name = reader.GetOrdinal("Name") != -1 ? reader["Name"].ToString() : string.Empty,
                                Date = reader.GetOrdinal("Date") != -1 ? DateTime.Parse(reader["Date"].ToString()) : DateTime.MinValue,
                                Issue = reader.GetOrdinal("Issue") != -1 ? reader["Issue"].ToString() : string.Empty,
                                Email = reader.GetOrdinal("Email") != -1 ? reader["Email"].ToString() : string.Empty,
                                IncidentCategory = reader.GetOrdinal("IncidentCategory") != -1 ? reader["IncidentCategory"].ToString() : string.Empty,
                                Tags = reader.GetOrdinal("Tags") != -1 ? reader["Tags"].ToString() : string.Empty,
                                Urgency = reader.GetOrdinal("Urgency") != -1 ? reader["Urgency"].ToString() : string.Empty,
                                Priority = reader.GetOrdinal("Priority") != -1 ? Convert.ToInt32(reader["Priority"]) : 0,
                                Symptoms = reader.GetOrdinal("Symptoms") != -1 ? reader["Symptoms"].ToString() : string.Empty,
                                Impact = reader.GetOrdinal("Impact") != -1 ? reader["Impact"].ToString() : string.Empty,
                                AdditionalInfo = reader.GetOrdinal("AdditionalInfo") != -1 ? reader["AdditionalInfo"].ToString() : string.Empty,

                            };
                            tickets.Add(ticket);
                        }
                    }
                }

                connection.Close();
            }

            return tickets;
        }

        public List<Ticket> GetTicketsByEmail(string email)
        {
            List<Ticket> tickets = new List<Ticket>();
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Ticket WHERE Email = @Email";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Email", email);

                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Ticket ticket = new Models.Ticket
                            {
                                Id = reader.GetOrdinal("Id") != -1 ? Convert.ToInt32(reader["Id"]) : 0,
                                TicketId = reader.GetOrdinal("TicketId") != -1 ? reader["TicketId"].ToString() : string.Empty,
                                Name = reader.GetOrdinal("Name") != -1 ? reader["Name"].ToString() : string.Empty,
                                Date = reader.GetOrdinal("Date") != -1 ? DateTime.Parse(reader["Date"].ToString()) : DateTime.MinValue,
                                Issue = reader.GetOrdinal("Issue") != -1 ? reader["Issue"].ToString() : string.Empty,
                                Email = reader.GetOrdinal("Email") != -1 ? reader["Email"].ToString() : string.Empty,
                                IncidentCategory = reader.GetOrdinal("IncidentCategory") != -1 ? reader["IncidentCategory"].ToString() : string.Empty,
                                Tags = reader.GetOrdinal("Tags") != -1 ? reader["Tags"].ToString() : string.Empty,
                                Urgency = reader.GetOrdinal("Urgency") != -1 ? reader["Urgency"].ToString() : string.Empty,
                                Priority = reader.GetOrdinal("Priority") != -1 ? Convert.ToInt32(reader["Priority"]) : 0,
                                Symptoms = reader.GetOrdinal("Symptoms") != -1 ? reader["Symptoms"].ToString() : string.Empty,
                                Impact = reader.GetOrdinal("Impact") != -1 ? reader["Impact"].ToString() : string.Empty,
                                AdditionalInfo = reader.GetOrdinal("AdditionalInfo") != -1 ? reader["AdditionalInfo"].ToString() : string.Empty,
                            };
                            tickets.Add(ticket);
                        }
                    }
                }

                connection.Close();
            }

            return tickets;
        }

        public Ticket GetTicketById(string ticketId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Ticket WHERE TicketId = @TicketId;", connection))
                {
                    command.Parameters.AddWithValue("@TicketId", ticketId);
                    SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Create a Ticket object from the database data
                        Ticket ticket = new Ticket
                        {
                            TicketId = reader["TicketId"].ToString(),
                            Name = reader["Name"].ToString(),
                            Issue = reader["Issue"].ToString(),
                            Email = reader["Email"].ToString(),
                            IncidentCategory = reader["IncidentCategory"].ToString(),
                            Tags = reader["Tags"].ToString(),
                            Urgency = reader["Urgency"].ToString(),
                            Symptoms = reader["Symptoms"].ToString(),
                            Impact = reader["Impact"].ToString(),
                            AdditionalInfo = reader["AdditionalInfo"].ToString(),

                        };

                        return ticket;
                    }
                    else
                    {
                        return null; // Ticket not found
                    }
                }
            }
        }

    }
}
