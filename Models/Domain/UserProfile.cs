namespace PinesExecutiveTravelApi.Models.Domain
{
    public class UserProfile
    {

        // Klucz główny
        public Guid Id { get; set; }  // Unique ID for the profile (GUID)

        // Obcy klucz do tabeli użytkownika
        public string UserId { get; set; }  // Foreign key referencing the User

        // Dane osobiste
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public string Birthday { get; set; } = string.Empty;  // Optional: User's date of birth
      
        public string Gender { get; set; } = string.Empty;  // Optional: User's gender
        public string Mobile { get; set; } = string.Empty;  // Optional: User's mobile number
        public string Landline { get; set; } = string.Empty;  // Optional: User's landline number
        public string Address { get; set; } = string.Empty;  // Optional: User's address
        public string Email { get; set; } = string.Empty;  // Optional: User's email address

        // Pole na URL zdjęcia profilowego
        public string ProfileImageUrl { get; set; } = string.Empty;  // Optional: URL of the user's profile picture

        // Nawigacja do użytkownika
        public User User { get; set; }  // Navigation property back to the User



    }
}
