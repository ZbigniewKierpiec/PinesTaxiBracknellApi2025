using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinesExecutiveTravelApi.Data;
using PinesExecutiveTravelApi.Models.Domain;
using PinesExecutiveTravelApi.Models.DTO;
using PinesExecutiveTravelApi.Repositories.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace PinesExecutiveTravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly UserContext userContext;

        public ProfileImageController(IImageRepository imageRepository , UserContext userContext )
        {
            this.imageRepository = imageRepository;
            this.userContext = userContext;
        }


        /*   [HttpGet]
           [Authorize]
           public async Task<IActionResult> GetAllImages()
           {
               // Call image repository to get all images
               var images = await imageRepository.GetAll();
               // Convert Domain model to DTO

               var response = new List<BlogImageDto>();
               foreach (var image in images)
               {
                   response.Add(new BlogImageDto
                   { 
                     Id = image.Id,
                      Title = image.Title,
                       DateCreated = image.DateCreated,
                        FileExtension = image.FileExtension,
                         FileName = image.FileName,
                          Url = image.Url,

                   });
               }

               return Ok(response);

           }


           */




        //[HttpPost("UploadImage")]
        //public async Task<IActionResult> UploadImage([FromForm]IFormFile file , [FromForm]string fileName , [FromForm] string title )
        //{

        //    ValidateFileUpload(file);
        //    if (ModelState.IsValid) 
        //    {

        //        // Get the currently logged-in user's ID from the authentication token
        //        var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


        //        //File Upload
        //        var blogImage = new BlogImage
        //        {
        //            FileExtension = Path.GetExtension(file.FileName).ToLower(),
        //            FileName = fileName,
        //            Title = title,
        //            DateCreated = DateTime.Now,
        //        };
        //      blogImage =  await imageRepository.Upload(file, blogImage);
        //        //Convert Domain Model to Dto

        //        var response = new BlogImageDto
        //        {
        //            Id = blogImage.Id,
        //            Title = blogImage.Title,
        //            DateCreated = blogImage.DateCreated,
        //            FileExtension = blogImage.FileExtension,
        //            FileName = blogImage.FileName,
        //            Url = blogImage.Url,
        //        };

        //        return Ok(response);

        //    }

        //     return BadRequest(ModelState);


        //}

        //private void ValidateFileUpload(IFormFile file)
        //{
        //    var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };


        //    if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
        //    {
        //        ModelState.AddModelError("file","Unsuported file format");
        //    }

        //    if (file.Length >10485760) 
        //    {

        //        ModelState.AddModelError("file", "File size cannot be more than 10MB");

        //    }

        //}






        /*  [HttpPost("SaveSingleProfileImage")]
          [Authorize]
          public async Task<IActionResult> SaveSingleProfileImage([FromBody] SingleProfileImageRequest request)
          {
              if (request == null)
              {
                  return BadRequest("Invalid image data.");
              }

              try
              {
                  var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                  // Upewnij się, że użytkownik jest zalogowany
                  if (string.IsNullOrEmpty(loggedInUserId))
                  {
                      return Unauthorized("User is not authenticated.");
                  }

                  // Sprawdzenie, czy użytkownik ma już przypisane zdjęcie profilowe
                  var existingImage = await userContext.SingleProfileImages
                      .FirstOrDefaultAsync(img => img.UserId == loggedInUserId);

                  if (existingImage != null)
                  {
                      // Jeśli zdjęcie już istnieje, nadpisz je
                      existingImage.FileName = request.FileName;
                      existingImage.FileExtension = request.FileExtension;
                      existingImage.Title = request.Title;
                      existingImage.ImageUrl = request.Url;
                      existingImage.DateCreated = DateTime.Now;

                      // Zaktualizuj istniejący rekord w bazie danych
                      userContext.SingleProfileImages.Update(existingImage);
                  }
                  else
                  {
                      // Jeśli użytkownik nie ma przypisanego zdjęcia, dodaj nowe
                      var profileImage = new SingleProfileImage
                      {
                          UserId = loggedInUserId,
                          FileName = request.FileName,
                          FileExtension = request.FileExtension,
                          Title = request.Title,
                          ImageUrl = request.Url,
                          DateCreated = DateTime.Now
                      };

                      // Dodaj nowe zdjęcie do bazy danych
                      await userContext.SingleProfileImages.AddAsync(profileImage);
                  }

                  // Zapisz zmiany w bazie danych
                  await userContext.SaveChangesAsync();

                  return Ok(new { message = "Single Profile image saved successfully" });
              }
              catch (Exception ex)
              {
                  return StatusCode(500, new { Message = ex.Message });
              }
          }



          */









        /// /////////////////////////



        [HttpPost("SaveSingleProfileImage")]
        [Authorize]
        public async Task<IActionResult> SaveSingleProfileImage([FromBody] SingleProfileImageRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid image data.");
            }

            try
            {
                // Pobierz ID zalogowanego użytkownika
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Upewnij się, że użytkownik jest zalogowany
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return Unauthorized("User is not authenticated.");
                }

                // Walidacja danych wejściowych
                if (string.IsNullOrEmpty(request.FileName) ||
                    string.IsNullOrEmpty(request.FileExtension) ||
                    string.IsNullOrEmpty(request.Title) ||
                    string.IsNullOrEmpty(request.Url))
                {
                    return BadRequest("Missing required fields.");
                }

                // Walidacja rozszerzeń plików
                var allowedExtensions = new List<string> { ".jpeg", ".jpg", ".png" };
                if (!allowedExtensions.Contains(request.FileExtension.ToLower()))
                {
                    return BadRequest("Invalid file extension.");
                }

                // Sprawdzenie, czy użytkownik ma już przypisane zdjęcie profilowe
                var existingImage = await userContext.SingleProfileImages
                    .FirstOrDefaultAsync(img => img.UserId == loggedInUserId);

                if (existingImage != null)
                {
                    // Jeśli zdjęcie już istnieje, nadpisz je
                    existingImage.FileName = request.FileName;
                    existingImage.FileExtension = request.FileExtension;
                    existingImage.Title = request.Title;
                    existingImage.ImageUrl = request.Url;
                    existingImage.DateCreated = DateTime.Now;

                    // Zaktualizuj istniejący rekord w bazie danych
                    userContext.SingleProfileImages.Update(existingImage);
                }
                else
                {
                    // Jeśli użytkownik nie ma przypisanego zdjęcia, dodaj nowe
                    var profileImage = new SingleProfileImage
                    {
                        Id = Guid.NewGuid().ToString(),  // Generowanie unikalnego ID dla zdjęcia
                        UserId = loggedInUserId,
                        FileName = request.FileName,
                        FileExtension = request.FileExtension,
                        Title = request.Title,
                        ImageUrl = request.Url,
                        DateCreated = DateTime.Now
                    };

                    // Dodaj nowe zdjęcie do bazy danych
                    await userContext.SingleProfileImages.AddAsync(profileImage);
                }

                // Zapisz zmiany w bazie danych
                await userContext.SaveChangesAsync();

                // Zwróć sukces
                return Ok(new { message = "Single Profile image saved successfully" });
            }
            catch (Exception ex)
            {
                // W przypadku błędu, zwróć odpowiedź 500
                return StatusCode(500, new { Message = "An error occurred while saving the profile image.", Detail = ex.Message });
            }
        }





        //////////////////////////////////


        [HttpGet("GetSingleProfileImage")]
        [Authorize]
        public async Task<IActionResult> GetSingleProfileImage()
        {
            try
            {
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Ensure the user is logged in
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return Unauthorized("User is not authenticated.");
                }

                // Query the database for the user's profile image
                var profileImage = await userContext.SingleProfileImages
                                                   .Where(img => img.UserId == loggedInUserId)
                                                   .FirstOrDefaultAsync(); // Make sure you're using the async method

                if (profileImage == null)
                {
                    return NotFound("Profile image not found.");
                }

                // Return the image URL or other relevant details
                return Ok(new
                {
                    FileName = profileImage.FileName,
                    FileExtension = profileImage.FileExtension,
                    Title = profileImage.Title,
                    ImageUrl = profileImage.ImageUrl // The path to the image or its URL
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }








































        ////////////////////////


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllImages()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var response = await imageRepository.GetAllImagesAsync(userId);
            return Ok(response);
        }


        [HttpPost("UploadImage")]
        [Authorize] // Ensure the user is authenticated
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            // Validate file input
            ValidateFileUpload(file);

            // Check if there are any validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                // Get the currently logged-in user's ID from the authentication token
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Check if the user is authenticated
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return Unauthorized("User is not authenticated.");
                }

                // File upload logic
                var blogImage = new BlogImage
                {
                    UserId = loggedInUserId, // Save with the logged-in user's ID
                    FileExtension = Path.GetExtension(file.FileName).ToLower(), // Get the file extension
                    FileName = fileName, // Assign file name
                    Title = title, // Assign title
                    DateCreated = DateTime.Now, // Assign creation date
                };

                // Save the image via the repository (assuming Upload is an async method for saving the file)
                blogImage = await imageRepository.Upload(file, blogImage);

                // Convert the saved domain model to a DTO for response
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url, // Assuming Upload returns a URL or path for the image
                };

                // Return the DTO as a response
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log and return any error encountered during the file upload
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            // Check for valid file extension
            if (file == null || !allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format. Only JPG, JPEG, and PNG are allowed.");
            }

            // Check for file size limit (10MB)
            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot exceed 10MB.");
            }
        }













    }
}
