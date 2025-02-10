namespace PinesExecutiveTravelApi.Models.Domain
{
    public class BlogImage
    {


        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }

        // Add the UserId property to define the relationship
        public string UserId { get; set; }

        // Navigation property
        public User User { get; set; }

    }
}
