namespace PinesExecutiveTravelApi.Models.Domain
{
    public class User
    {


        public string Id { get; set; }
        public string Username { get; set; } = string.Empty; // Unique username
        public string Email { get; set; } = string.Empty; // Email address
        public string Password { get; set; } = string.Empty; // Hashed password

        // Navigation property to the UserProfile (1:1 relationship)
        public UserProfile UserProfile { get; set; }
        public ICollection<TaxiOrder> Reservations { get; set; }

        // Navigation property for BlogImages
        public ICollection<BlogImage> BlogImages { get; set; }

    }
}
