using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinesExecutiveTravelApi.Data;
using PinesExecutiveTravelApi.Models.Domain;
using PinesExecutiveTravelApi.Models.DTO;
using PinesExecutiveTravelApi.Repositories.Interface;

namespace PinesExecutiveTravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly UserContext userContext;

        public AuthController(  UserManager<IdentityUser> userManager , ITokenRepository tokenRepository , UserContext userContext)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.userContext = userContext;
        }



        /*   [HttpPost("register")]
           public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
           {
               // Validate inputs
               if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
               {
                   return BadRequest("Email and password are required.");
               }

               // Check if email already exists
               var existingUser = await userManager.FindByEmailAsync(request.Email);
               if (existingUser != null)
               {
                   return BadRequest("User with this email already exists.");
               }

               // Create IdentityUser (standard user in ASP.NET Identity)
               var user = new IdentityUser
               {
                   UserName = request.Email?.Trim(),
                   Email = request.Email?.Trim(),
                   NormalizedEmail = request.Email?.Trim().ToUpper()
               };

               // Create the user in the Identity system
               var identityResult = await userManager.CreateAsync(user, request.Password);
               if (!identityResult.Succeeded)
               {
                   var errors = identityResult.Errors.Select(e => e.Description);
                   return BadRequest(new { Errors = errors });
               }

               // Add role to user (Reader)
               var roleResult = await userManager.AddToRoleAsync(user, "Reader");
               if (!roleResult.Succeeded)
               {
                   var errors = roleResult.Errors.Select(e => e.Description);
                   return BadRequest(new { Errors = errors });
               }

               // Create a custom User entity
               var newUser = new User
               {
                   Id = user.Id,
                   Email = request.Email,
                   Username = request.Email
               };

               // Add custom user information to the database
               userContext.Users.Add(newUser);
               await userContext.SaveChangesAsync();



               return Ok();
           }
        */

        /////////////////////////////////////////////////////////////////


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Check if email already exists
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest("User with this email already exists.");
            }

            // Create IdentityUser (standard user in ASP.NET Identity)
            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),
                NormalizedEmail = request.Email?.Trim().ToUpper()
            };

            // Create the user in the Identity system
            var identityResult = await userManager.CreateAsync(user, request.Password);
            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            // Add role to user (Reader)
            var roleResult = await userManager.AddToRoleAsync(user, "Reader");
            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            // Create a custom User entity
            var newUser = new User
            {
                Id = user.Id,
                Email = request.Email,
                Username = request.Email
            };

            // Add custom user information to the database
            userContext.Users.Add(newUser);
            await userContext.SaveChangesAsync();

            // Create an empty profile for the user
            var userProfile = new UserProfile
            {
                UserId = user.Id, // Link the profile to the user
                FirstName = string.Empty,
                Surname = string.Empty,
                Birthday = string.Empty,
               
                Gender = string.Empty,
                Mobile = string.Empty,
                Landline = string.Empty,
                Address = string.Empty,
          
             
              
               
            };

            // Add the empty profile to the database
            userContext.UserProfiles.Add(userProfile);
            await userContext.SaveChangesAsync();





            return Ok(new { Message = "User registered successfully, profile created." });
        }





        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {

            // check email
            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser is not null)
            {
                // check password
                var checkPasswordResoult = await userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPasswordResoult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    // Create a Token Response
                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());
                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };
                    // Create a Token and Response
                    return Ok(response);
                }

            }
            ModelState.AddModelError("", "Email or Password Incorrect");
            return ValidationProblem(ModelState);


        }




    }
}
