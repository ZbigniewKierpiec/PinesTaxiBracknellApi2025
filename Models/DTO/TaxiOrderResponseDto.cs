using PinesExecutiveTravelApi.Models.Enums;

namespace PinesExecutiveTravelApi.Models.DTO
{
    public class TaxiOrderResponseDto
    {

        public Guid Id { get; set; } // Unique identifier for the taxi order

        // Pickup and Dropoff information
        public string PickupLocation { get; set; } = string.Empty; // Pickup location (e.g., address)
        public string DropoffLocation { get; set; } = string.Empty; // Drop-off location (e.g., address)

        // The time when the taxi is scheduled to pick up
        public string PickupTime { get; set; } = string.Empty;

        // Additional trip-related information
        public string Via { get; set; } = string.Empty; // Optional via location (optional stop)
        public string Passengers { get; set; } = string.Empty; // Number of passengers
        public string Louggages { get; set; } = string.Empty; // Number of luggages
        public bool Greet { get; set; } // Whether the user wants a greet at the pickup

        // Pricing and car-related details
        public decimal Price { get; set; } // Estimated price for the taxi ride
        public string CarType { get; set; } = string.Empty; // Preferred car type (e.g., sedan, SUV)
        public string CarImage { get; set; } = string.Empty; // Image of the car (optional)

        // Customer information
        public string Name { get; set; } = string.Empty; // Customer's name
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; // Customer's email address

        // Special instructions for the driver
        public string DriverInstruction { get; set; } = string.Empty; // Any specific instructions for the driver

        // Optionally, you can include the timestamp when the order was created if needed
        public DateTime CreatedAt { get; set; } // Timestamp for when the order was created

        // User ID associated with the order
        public string UserId { get; set; } // The UserId associated with the order

        public BookingStatus Status { get; set; }

    }
}
