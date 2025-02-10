using PinesExecutiveTravelApi.Models.Enums;

namespace PinesExecutiveTravelApi.Models.DTO
{
    public class TaxiOrderRequestDto
    {


        // The data needed to create a new taxi order or update an existing one
        public string PickupLocation { get; set; } = string.Empty; // Pickup location (e.g., address)
        public string DropoffLocation { get; set; } = string.Empty; // Drop-off location (e.g., address)
        public string PickupTime { get; set; } = string.Empty; // Time when the taxi is scheduled to pick up

        public string Via { get; set; } = string.Empty; // Optional via location (optional stop)
        public string Passengers { get; set; } = string.Empty; // Number of passengers
        public string Louggages { get; set; } = string.Empty; // Number of luggages
        public bool Greet { get; set; } // Whether the user wants a greet at the pickup
        public decimal Price { get; set; } // Estimated price for the taxi ride
        public string CarType { get; set; } = string.Empty; // Preferred car type (e.g., sedan, SUV)
        public string CarImage { get; set; } = string.Empty; // Image of the car (optional)
        public string Name { get; set; } = string.Empty; // Customer's name
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; // Customer's email address
        public string DriverInstruction { get; set; } = string.Empty; // Any specific instructions for the driver

        // Add BookingStatus Enum

        public BookingStatus Status { get; set; } = BookingStatus.Pending;


    }
}
