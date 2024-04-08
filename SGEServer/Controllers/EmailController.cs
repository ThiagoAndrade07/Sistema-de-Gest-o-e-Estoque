using MailKit.Net.Smtp;
using MimeKit;


namespace SGEServer.Controllers
{
    public class EmailService
    {
        public void SendEmail(string toAddress, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Thiago Andrade", "thiagofba@hotmail.com"));
            message.To.Add(new MailboxAddress("", toAddress));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
               
                client.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

               
                client.Authenticate("thiagofba@hotmail.com", "");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }




}
