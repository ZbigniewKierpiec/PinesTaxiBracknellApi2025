namespace PinesExecutiveTravelApi.Models.DTO
{
    public class UserProfileDto
    {


      //  public Guid Id { get; set; }  // Profile ID
       // public string UserId { get; set; }  // Foreign key referencing the User

        // Personal data
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Birthday { get; set; } = string.Empty;  // Optional: User's date of birth
      
        public string Gender { get; set; } = string.Empty;  // Optional: User's gender
        public string Mobile { get; set; } = string.Empty;  // Optional: User's mobile number
        public string Landline { get; set; } = string.Empty;  // Optional: User's landline number
        public string Address { get; set; } = string.Empty;  // Optional: User's address
        public string Email { get; set; } = string.Empty;  // Optional: User's email address


       


    }
}
