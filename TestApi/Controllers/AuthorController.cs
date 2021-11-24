using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Test.ApplicationServices.AppServices.Manager;
using Test.Core;
using Test.Core.Model;
using Test.Repo.repo.AuthorRepo;
using Test.Repo.repo.mongo;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        static bool mailSent = false;
        private readonly ILogger<AuthorController> _logger;
        public string City => "Lagos";
        private readonly GuidService guidService;
       
       
        public readonly IAuthorRepo _authorRepo;
        private readonly IWeatherForecastManager _weatherForecastManager;
        public  NotificationMetadata _notificationMetadata = new NotificationMetadata();
        public AuthorController(IAuthorRepo authorRepo, GuidService guidService ,
            IWeatherForecastManager weatherForecastManager, ILogger<AuthorController> logger)
        {
            _logger = logger;
            this.guidService = guidService;
            _authorRepo = authorRepo;
            _weatherForecastManager = weatherForecastManager;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(typeof(string))]
        public async Task<IActionResult> Search(string search)
        {
            try
            {
                var result = await _authorRepo.Search(search);
                if (result.Count > 0)
                {
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(typeof(string))]
        public async Task<IActionResult> GetAllAuthorInfo()
        {
            var progress = new Progress<List<AuthorInfo>>();
            try
            {
                var logMessage = $"guidvalue: \"{guidService.GetGuidService()}\"";
                _logger.LogInformation(logMessage);
                progress.ProgressChanged += (_, result) =>
                {
                    Console.WriteLine($"{result.Count} - Authors Loaded");
                };
                var result = await  _authorRepo.GetAllAuthorInfo(progress);
                if (result.Count > 0)
                {
                    
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError("An Error Occured: {error}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An Internal Server error occured, please contact system admin");
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCityWeatherForecast(string cityName)
        {
            try
            {
                var result = await _weatherForecastManager.GetWeatherForecastProvider(cityName);
                var weather = result!= null  ? await result.GetWeatherForeCastForCity(cityName) : false;
                if (weather)
                {
                    return Ok();
                }
                else return NotFound();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(typeof(string))]
        public async Task<IActionResult> InsertAuthor([FromBody] AuthorDto model)
        {
            try
            {
                
                //else return res;
                (bool, string) res;
                if (ModelState.IsValid)
                {
                     res = await _authorRepo.Insert(model);
                    
                    if (res.Item1)
                    {
                        return Ok(res.Item2);
                    }
                    return Ok(res.Item2);
                    
                }
                return BadRequest("Invalide Model");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured");
                throw;
               
            }
        }

        [HttpPost("Email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(typeof(string))]
        public async Task<IActionResult> EmailGet(List<string> emails)
        {
            
            try
            {
                foreach (var item in emails)
                {
                    SmtpClient client = new SmtpClient();
                    client.Host = _notificationMetadata.SmtpServer;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(_notificationMetadata.UserName, _notificationMetadata.Password);
                    client.Port = _notificationMetadata.Port;
                    //client.EnableSsl = true;
                    //client.TargetName = "STARTTLS/smtp-mail.outlook.com";
                    //client.EnableSsl = "";

                    var message = new MailMessage();
                    //MailAddress from = new MailAddress("jerryokoduwa@hotmail.co.uk");
                    //    MailAddress from = new MailAddress("jane@contoso.com",
                    //   "Jane " + (char)0xD8 + " Clayton",
                    //System.Text.Encoding.UTF8);

                    var to = new MailAddress(item);
                    MailMessage messages = new MailMessage();
                    // MailMessage messages = new MailMessage(from, to);
                    messages.To.Add(to);
                    //messages.From = from;
                    messages.From = new MailAddress("Jerryokoduwa@hotmail.co.uk");
                    messages.Subject = "This is a Test Mail";
                    messages.Body = "<div > <body ><p style=color: blue;><i >Sorry guys, This is a test mail message using Exchange OnLine</i></p></body></div>";
                    messages.IsBodyHtml = true;
                    //string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
                    //message.Body += Environment.NewLine + someArrows;
                    //message.BodyEncoding = System.Text.Encoding.UTF8;
                    //message.SubjectEncoding = System.Text.Encoding.UTF8;
                    client.SendCompleted += new
                SendCompletedEventHandler(SendCompletedCallback);
                    string userState = "test message1";
                    client.Send(messages);

                    message.Dispose();

                   
                    //EmailMessage message = new EmailMessage();
                    // message.Sender = new MailboxAddress("Self", _notificationMetadata.Sender);
                    //message = new MailboxAddress("Self", _notificationMetadata.Reciever);
                    //message.Subject = "Welcome";
                    //message.Content = "!";
                    //var mimeMessage = _authorRepo.CreateMimeMessageFromEmailMessage(message);
                    //using (SmtpClient smtpClient = new SmtpClient())
                    //{
                    //    smtpClient.Connect(_notificationMetadata.SmtpServer,
                    //    _notificationMetadata.Port, true);
                    //    smtpClient.Authenticate(_notificationMetadata.UserName,
                    //    _notificationMetadata.Password);
                    //    smtpClient.Send();
                    //    smtpClient.Disconnect(true);
                    //}
                }
                return Ok("Email Sent!");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
    }
}
