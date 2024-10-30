using Google.Cloud.Firestore;

namespace Finders.Models
{
    public class AppAndClients
    {
        public Appointments AppointmentDetails { get; set; }
        public ClientModel ClientDetails { get; set; }

    }
}
