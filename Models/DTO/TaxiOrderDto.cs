using PinesExecutiveTravelApi.Models.Domain;
using PinesExecutiveTravelApi.Models.Enums;

namespace PinesExecutiveTravelApi.Models.DTO
{
    public class TaxiOrderDto
    {

   

        //  public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for the taxi order
        public Guid Id { get; set; }
        public string UserId { get; set; } // Foreign key property, should match the User entity's Id type
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PickupLocation { get; set; } = string.Empty; // Starting location (e.g., pickup address)

        public string Via { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty; // Destination location (e.g., drop-off address)

        public string PickupTime { get; set; } = string.Empty;
        public string Passengers { get; set; } = string.Empty;
        public string Louggages { get; set; }
        public bool Greet { get; set; }

        public string CarType { get; set; } = string.Empty;
        public string CarImage { get; set; } = string.Empty;

     

        public string DriverInstruction { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the order was created
        public decimal Price { get; set; }
        // Foreign Key to User (the user who created the order)


        // Navigation property to the User entity (User who created the order)
        public User User { get; set; } // Navigation property for retrieving User details

        // Add BookingStatus Enum

        public BookingStatus Status { get; set; } = BookingStatus.Pending;




    }
}
