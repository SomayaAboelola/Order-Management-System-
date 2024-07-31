using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Orders.Core.Dtos;
using Orders.Core.Entities;
using Orders.Repository._Data;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Orders.Services
{

    public class EmailService
    {

        private readonly IConfiguration _configuration;
        private readonly OrderDbContext _context;

        public EmailService(IConfiguration configuration ,OrderDbContext context)
        {

            _configuration = configuration;
            _context = context;
        }

        public void MailSetting(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_configuration["EmailService:FromEmailAddress"], _configuration["EmailService:ApiKey"]);
            client.Send("somayamagdyaboelola@gmail.com", email.To, email.Subject, email.Body);

        }

   
        private string GenerateEmailContent(Order order)
        {
            var customer =  _context.Customers.Find(order.CustomerId);
            var orderDetailsStringBuilder = new StringBuilder();

            // Order details 
            orderDetailsStringBuilder.AppendLine($@"
        Order #{order.Id} has been {order.orderStatus}.
        Customer: {order.Customer.Name} ({order.Customer.Email})
         ");

            // Total amount
            orderDetailsStringBuilder.AppendLine($"Total: {order.TotalAmount:C}");

            return orderDetailsStringBuilder.ToString();
        } 
        
      public void SendEmail(Order order)
        {

            var email = new Email()
            {
                Subject = $"Order Update: Your Order #{order.Id} Has {order.orderStatus}",
                Body = GenerateEmailContent(order),
                To = order.Customer.Email

            };
             MailSetting(email);
        }

    }
}