using Microsoft.EntityFrameworkCore;
using PinesExecutiveTravelApi.Data;
using PinesExecutiveTravelApi.Models.Domain;
using PinesExecutiveTravelApi.Models.DTO;
using PinesExecutiveTravelApi.Repositories.Interface;

namespace PinesExecutiveTravelApi.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserContext userContext;
      

        public ImageRepository(IWebHostEnvironment webHostEnvironment , IHttpContextAccessor httpContextAccessor , UserContext userContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.userContext = userContext;
           
        }

        public async Task AddAsync(SingleProfileImage profileImage)
        {
          
            await userContext.SingleProfileImages.AddAsync(profileImage);
            await userContext.SaveChangesAsync();





        }





        public async Task<List<BlogImage>> GetAllImagesAsync(string userId)
        {
            return await userContext.BlogImages
                        .Where(img => img.UserId == userId)
                        .ToListAsync();
        }







        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {

            // 1-Upload the Image to Api/Images
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // 2-Update the database
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

            blogImage.Url = urlPath;
           await  userContext.BlogImages.AddAsync(blogImage);
            await userContext.SaveChangesAsync();
            return blogImage;


        }
    }
}
