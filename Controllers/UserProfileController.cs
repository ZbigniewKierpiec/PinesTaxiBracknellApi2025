using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PinesExecutiveTravelApi.Data;

using PinesExecutiveTravelApi.Models.Domain;
using PinesExecutiveTravelApi.Models.DTO;
using System.Security.Claims;

namespace PinesExecutiveTravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserContext context;

        public UserProfileController(UserContext context)
        {
            this.context = context;
        }




        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                // Get the currently logged-in user's ID from the authentication token
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Check if the user is authenticated
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return Unauthorized("User is not authenticated.");
                }

                // Fetch the user profile for the logged-in user from the database
                var userProfile = await context.UserProfiles
                    .FirstOrDefaultAsync(up => up.UserId == loggedInUserId);

                // Return 404 Not Found if the user profile does not exist
                if (userProfile == null)
                {
                    return NotFound("Profile not found.");
                }

                // Return the user profile with a 200 OK response
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 500 error with the exception message
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [HttpPost("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDto userProfileDto)
        {
            try
            {
                // Get the currently logged-in user's ID from the authentication token
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Check if the user is authenticated
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return Unauthorized("User is not authenticated.");
                }

                // Validate the incoming DTO
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Fetch the existing user profile from the database
                var userProfile = await context.UserProfiles
                    .FirstOrDefaultAsync(up => up.UserId == loggedInUserId);

                if (userProfile == null)
                {
                    // If the profile does not exist, create a new one
                    userProfile = new UserProfile
                    {
                        UserId = loggedInUserId,
                        FirstName = userProfileDto.FirstName,
                        Surname = userProfileDto.Surname,
                        Email = userProfileDto.Email,
                        Mobile = userProfileDto.Mobile,
                        Address = userProfileDto.Address,
                        Birthday = userProfileDto.Birthday,
                        Gender = userProfileDto.Gender,
                        Landline = userProfileDto.Landline,
                  
                    


                    };

                    await context.UserProfiles.AddAsync(userProfile);
                }
                else
                {
                    // If the profile exists, update the details
                    userProfile.FirstName = userProfileDto.FirstName;
                    userProfile.Surname = userProfileDto.Surname;
                    userProfile.Email = userProfileDto.Email;
                    userProfile.Mobile = userProfileDto.Mobile;
                    userProfile.Address = userProfileDto.Address;
                    userProfile.Birthday = userProfileDto.Birthday;
                    userProfile.Gender = userProfileDto.Gender;
                    userProfile.Landline = userProfileDto.Landline;
               

                }

                // Save changes to the database
                await context.SaveChangesAsync();

                // Return the updated profile with a 200 OK response
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 500 error with the exception message
                return StatusCode(500, new { Message = ex.Message });
            }
        }


        ///////////////////////////////////////////////////////////////////////////

       


    }
}
