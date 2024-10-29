using Microsoft.AspNetCore.Mvc;
using SafetyApp.Service;

namespace SafetyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LocationController : ControllerBase
    {
        private readonly EmailService _emailService;

        public LocationController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LocationData location)
        {
            if (location == null)
            {
                return BadRequest("Location data is required.");
            }

            // Format the email body with the location data
            string emailBody = $"The user's location is: <br/>" +
                               $"Latitude: {location.Latitude} <br/>" +
                               $"Longitude: {location.Longitude}";

            // Send the email
             _emailService.SendEmailAsync("abhikrantsingh@gmail.com", "User Location", emailBody);

            // Optionally, process the location data further (e.g., save to database)

            return Ok(new { Message = "Location received and email sent successfully." });

        }

        public class LocationData
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
