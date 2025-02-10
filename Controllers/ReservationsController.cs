using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinesExecutiveTravelApi.Data;
using PinesExecutiveTravelApi.Models.Domain;
using PinesExecutiveTravelApi.Models.DTO;
using PinesExecutiveTravelApi.Models.Enums;
using PinesExecutiveTravelApi.Repositories.Implementation;
using PinesExecutiveTravelApi.Repositories.Interface;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace PinesExecutiveTravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {

     
        private readonly UserContext _userContext;
        private readonly IBookingRepository bookingRepository;
        private readonly EmailService emailService;

        public ReservationsController(UserContext userContext, IBookingRepository bookingRepository , EmailService emailService)
        {
            _userContext = userContext;
            this.bookingRepository = bookingRepository;
            this.emailService = emailService;
        }


        // Get the logged-in user's Id from the JWT token
        private string GetLoggedInUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }





        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////





        [HttpPost("add")]
        [Authorize(Roles = "Reader,Writer,Admin")]
        public async Task<IActionResult> AddReservation([FromBody] TaxiOrderRequestDto request)
        {


            if (request == null)
            {
                return BadRequest("Invalid reservation data.");
            }

            // Get the currently logged-in user's ID from the authentication token

            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Check if the user is authenticated (logged in)
            if (string.IsNullOrEmpty(loggedInUserId))
            {
                return Unauthorized("User is not authenticated.");
            }

            // Fetch the user from the database using the logged-in user's ID
            var user = await _userContext.Users.FindAsync(loggedInUserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create a new TaxiOrder and link it to the logged-in user
            var reservation = new TaxiOrder
            {
                Id = Guid.NewGuid(),
                PickupLocation = request.PickupLocation,
                DropoffLocation = request.DropoffLocation,
                CarType = request.CarType,
                CarImage = request.CarImage,
                DriverInstruction = request.DriverInstruction,
                Email = request.Email,
                Greet = request.Greet,
                Louggages = request.Louggages,
                Name = request.Name,
                Passengers = request.Passengers,
                PhoneNumber = request.PhoneNumber,
                Price = request.Price,
                User = user,
                Via = request.Via,
                PickupTime = request.PickupTime,
                UserId = loggedInUserId,  // Associate the order with the logged-in user
                CreatedAt = DateTime.UtcNow,
                Status = request.Status,
            };

            // Add the new reservation to the database
            _userContext.Reservations.Add(reservation);
            await _userContext.SaveChangesAsync(); // Save changes to the database

            return Ok(new { Message = "Reservation added successfully!", ReservationId = reservation.Id });







        }













        ////////////////////////////////////////////////////////////////////



        [HttpGet("GetAllReservations")]
        [Authorize(Roles = "Writer")]
        // Zabezpieczenie - tylko zalogowani użytkownicy mogą zobaczyć rezerwacje
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                // Pobieramy wszystkie rezerwacje
                var reservations = await _userContext.Reservations
                   // .Include(r => r.User)  // Jeśli chcesz dołączyć dane użytkownika
                    .ToListAsync();

               

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                // Obsługa wyjątków
                return StatusCode(500, new { Message = ex.Message });
            }
        }



        ////////////////////////////////////////////////////////////////////////////////////////////


        [HttpGet("get-my-reservations")]
        [Authorize]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetMyReservations()
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

                // Fetch the reservations for the logged-in user from the database
                var userReservations = await _userContext.Reservations
                    .Where(r => r.UserId == loggedInUserId)  // Filter reservations by the logged-in user's ID
                    .ToListAsync();

                // Zamiast zwracać 404, zwróć pustą tablicę, jeśli brak rezerwacji
                if (userReservations == null || !userReservations.Any())
                {
                    return Ok(userReservations);  // Zwracamy pustą tablicę z kodem 200 OK
                }

                // Return the list of reservations
                return Ok(userReservations);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 500 error with the exception message
                return StatusCode(500, new { Message = ex.Message });
            }





        }

        ////////////////////////////////////////////////////////
        ///


        [HttpPut("cancel-reservation/{reservationId}")]
        [Authorize]
        public async Task<IActionResult> CancelReservation(Guid reservationId)
        {
            try
            {
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return Unauthorized(new { Error = "User is not authenticated." });
                }

                // Retrieve the reservation
                var reservation = await _userContext.Reservations
                    .Where(r => r.UserId == loggedInUserId && r.Id == reservationId)
                    .FirstOrDefaultAsync();

                if (reservation == null)
                {
                    return NotFound(new { Error = "Reservation not found or it doesn't belong to the logged-in user." });
                }

                // Handle invalid reservation statuses
                if (reservation.Status == BookingStatus.Cancelled || reservation.Status == BookingStatus.Completed)
                {
                    return BadRequest(new { Error = "The reservation has already been cancelled or completed." });
                }

                // Set reservation status to cancelled
                reservation.Status = BookingStatus.Cancelled;

                await _userContext.SaveChangesAsync();


                // Ensure the reservation has an associated email
                if (string.IsNullOrEmpty(reservation.Email))
                {
                    return BadRequest(new { Error = "No email address associated with the reservation." });
                }

                // Send a cancellation email to the user
                var emailSent = await TrySendCancellationEmailAsync(reservation.Email, reservation.Name, reservationId);
                if (!emailSent)
                {
                    // Log the error but do not block the cancellation flow
                    Console.WriteLine($"Failed to send cancellation email to {reservation.Email}.");
                }

                // Return success response
                return Ok(new { Message = "Reservation has been successfully cancelled." });
            }
            catch (Exception ex)
            {
                // Return a generic error with details for debugging
                return StatusCode(500, new { Error = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        // Helper method to send cancellation email
        private async Task<bool> TrySendCancellationEmailAsync(string email, string name, Guid reservationId)
        {
            try
            {
                var emailSubject = "Confirmation of Reservation Cancellation";


                var emailBody = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: Arial, sans-serif;
            color: #333;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }}
        .email-container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #fff;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
        }}


.box {{
            width: 200px;
            height: 160px;
         
           
           
            border-radius: 50%;
            margin: 0 auto;
            background-color: white;

       


        }}

        .logo {{
            width: 100px;
            height: 100px;
            border-radius: 50%;
     margin: 0 auto;
        }}

        img {{
            width: 100%;  /* Ensures image fills the container */
            height: auto; /* Keeps aspect ratio */
            object-fit: cover; /* Ensures image covers the space */
            display: block;  /* Centers the image */
            border-radius: 50%;

        }}



      


        h2 {{
            color: #2c3e50;
        }}
        p {{
            font-size: 16px;
            line-height: 1.6;
            color: #555;
            margin-bottom: 20px;
        }}
        .highlight {{
            color: #2c3e50;
            font-weight: bold;
        }}
        .footer {{
            font-size: 14px;
            color: #999;
            text-align: center;
        }}
        .footer a {{
            color: #3498db;
            text-decoration: none;
        }}

      
    </style>
</head>
<body>
    <div class='email-container'>
        <!-- Add Logo -->
        <div class='box'>
            <div class='logo'>
                <img src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTP3KefXU2T5mNuD_l8QJBkDya51ALGXd8_XA&s' alt=''>
            </div>
            <p>Pines Executive Travel</p>
        </div>

        <p>Dear <span class='highlight'>{name}</span>,</p>
        
        <p>We would like to confirm that your reservation with ID <span class='highlight'>{reservationId}</span> has been successfully canceled by you.</p>

        <p>We understand that plans can change, and we appreciate you using our system to manage your reservation independently. Should you have any questions or require further assistance, please do not hesitate to reach out to our team. We are here to help. </p>

        <p>Thank you for choosing our service, and we hope to have the opportunity to assist you again in the future.</p>

        <p class='footer'>Best regards,<br>Your Reservation Team</p>
    </div>
</body>
</html>
";







                await emailService.SendEmailAsync(email, emailSubject, emailBody);
                return true;
            }
            catch (Exception emailEx)
            {
                // Log error details for further investigation
                Console.WriteLine($"Error sending email: {emailEx.Message}");
                return false;
            }
        }









        ///

        [HttpGet]
        [Route("{id:Guid}")]
       // [Authorize(Roles = "Writer")]
        // Zabezpieczenie - tylko zalogowani użytkownicy mogą zobaczyć rezerwacje
        public async Task<IActionResult> GetReservationById([FromRoute]Guid id)


        {
            var existingBooking = await bookingRepository.GetById(id);

            if (existingBooking == null)
            {
                return NotFound();
            }

            var response = new TaxiOrderResponseDto
            {
                Id = existingBooking.Id,
                CarImage = existingBooking.CarImage,
                CarType = existingBooking.CarType,
                CreatedAt = existingBooking.CreatedAt,
                DriverInstruction = existingBooking.DriverInstruction,
                DropoffLocation = existingBooking.DropoffLocation,
                Email = existingBooking.Email,
                Greet = existingBooking.Greet,
                Louggages = existingBooking.Louggages,
                Name = existingBooking.Name,
                Passengers = existingBooking.Passengers,
                PhoneNumber = existingBooking.PhoneNumber,
                PickupLocation = existingBooking.PickupLocation,
                PickupTime = existingBooking.PickupTime,
                Price = existingBooking.Price,
                UserId = existingBooking.UserId,
                Via = existingBooking.Via,
                Status = existingBooking.Status,
            };


            return Ok(response);


        }



        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid id)
        { 
        
        var booking = await bookingRepository.DeleteAsync(id);
            if (booking == null) 
            
            {
                return NotFound();

            }

            var response = new TaxiOrderDto
            {
                Id = booking.Id,
                CarImage = booking.CarImage,
                CarType = booking.CarType,
                CreatedAt = booking.CreatedAt,
                DriverInstruction = booking.DriverInstruction,
                DropoffLocation = booking.DropoffLocation,
                Email = booking.Email,
                Greet = booking.Greet,
                Louggages = booking.Louggages,
                Name = booking.Name,
                Passengers = booking.Passengers,
                PhoneNumber = booking.PhoneNumber,
                PickupLocation = booking.PickupLocation,
                PickupTime = booking.PickupTime,
                Price = booking.Price,
                User = booking.User,
                UserId = booking.UserId,
                Via = booking.Via,
                Status = booking.Status,

            };
            return Ok(response);














        }








    }


























}