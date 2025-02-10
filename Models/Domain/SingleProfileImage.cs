namespace PinesExecutiveTravelApi.Models.Domain
{
    public class SingleProfileImage
    {

        public string Id { get; set; }
        public string UserId { get; set; }  // ID użytkownika
        public string FileName { get; set; }  // Nazwa pliku
        public string FileExtension { get; set; }  // Rozszerzenie pliku
        public string Title { get; set; }  // Tytuł
        public string ImageUrl { get; set; }  // URL zdjęcia
        public DateTime DateCreated { get; set; }  // Data utworzenia
        public virtual User User { get; set; }

    }
}
