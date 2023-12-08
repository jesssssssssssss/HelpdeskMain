using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskMain.Models
{
    public class Ticket : INotifyPropertyChanged
    {

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }


        private string ticketid;
        public string TicketId
        {
            get { return ticketid; }
            set
            {
                ticketid = value;
                OnPropertyChanged(nameof(TicketId));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }


        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string issue;
        public string Issue
        {
            get { return issue; }
            set
            {
                issue = value;
                OnPropertyChanged(nameof(Issue));
            }
        }


        private string incidentCategory;
        public string IncidentCategory
        {
            get { return incidentCategory; }
            set
            {
                incidentCategory = value;
                OnPropertyChanged(nameof(IncidentCategory));
            }
        }

        private string tags;
        public string Tags
        {
            get { return tags; }
            set
            {
                tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }

        private string urgency;
        public string Urgency
        {
            get { return urgency; }
            set
            {
                urgency = value;
                OnPropertyChanged(nameof(Urgency));
            }
        }

        public int Priority { get; set; }

        private string symptoms;
        public string Symptoms
        {
            get { return symptoms; }
            set
            {
                symptoms = value;
                OnPropertyChanged(nameof(Symptoms));
            }
        }

        private string impact;
        public string Impact
        {
            get { return impact; }
            set
            {
                impact = value;
                OnPropertyChanged(nameof(Impact));
            }
        }

        private string additionalinfo;
        public string AdditionalInfo
        {
            get { return additionalinfo; }
            set
            {
                additionalinfo = value;
                OnPropertyChanged(nameof(AdditionalInfo));
            }
        }

        private int attachments;
        public int Attachments
        {
            get { return attachments; }
            set
            {
                attachments = value;
                OnPropertyChanged(nameof(Attachments));
            }

        } // Probably change datatype of attachments later


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
