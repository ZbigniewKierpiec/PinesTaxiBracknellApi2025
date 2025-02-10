namespace PinesExecutiveTravelApi.Models.DTO
{
    public class SingleProfileImageRequest
    {

        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }


    }
}
