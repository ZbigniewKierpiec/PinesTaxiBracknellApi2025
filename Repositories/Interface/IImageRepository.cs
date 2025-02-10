using PinesExecutiveTravelApi.Models.Domain;
using PinesExecutiveTravelApi.Models.DTO;

namespace PinesExecutiveTravelApi.Repositories.Interface
{
    public interface IImageRepository
    {

      Task<BlogImage>  Upload(IFormFile file ,BlogImage blogImage);
      Task<List<BlogImage>> GetAllImagesAsync(string userId);

      Task AddAsync(SingleProfileImage profileImage);


       



    }
}
