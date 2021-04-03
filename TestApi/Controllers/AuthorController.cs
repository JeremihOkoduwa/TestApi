using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Test.Core;
using Test.Core.Model;
using Test.Repo.repo.AuthorRepo;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        static bool mailSent = false;
        public readonly IAuthorRepo _authorRepo;
        public  NotificationMetadata _notificationMetadata = new NotificationMetadata();
        public AuthorController(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }
        [HttpPost("InsertAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(typeof(string))]
        public async Task<IActionResult> InsertAuthor(AuthorDto model)
        {
            try
            {
                (bool, string) res;
                if (ModelState.IsValid)
                {
                    var result = await _authorRepo.Insert(model);
                    res = result;
                    if (result.Item1)
                    {
                        return Ok(result.Item2);
                    }
                }
                return BadRequest("Invalid Model");
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
