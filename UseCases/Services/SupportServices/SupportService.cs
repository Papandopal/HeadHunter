using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using Domain.Enums;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UseCases.Services.SupportServices.DTOs;
using UseCases.Services.SupportServices.Interfaces;

namespace UseCases.Services.SupportServices
{
    public class SupportService(IConfiguration configuration) : ISupportService
    {
        AddSupportTicketDTO ISupportService.Deserialize(string json)
        {
            var doc = JsonDocument.Parse(json);
            return new AddSupportTicketDTO
            {
                AdministratorEmail = doc.RootElement.GetProperty(nameof(AddSupportTicketDTO.AdministratorEmail)).GetString(),
                FirstName = doc.RootElement.GetProperty(nameof(AddSupportTicketDTO.FirstName)).GetString(),
                LastName = doc.RootElement.GetProperty(nameof(AddSupportTicketDTO.LastName)).GetString(),
                Description = doc.RootElement.GetProperty(nameof(AddSupportTicketDTO.Description)).GetString(),
                Link = doc.RootElement.GetProperty(nameof(AddSupportTicketDTO.Link)).GetString(),
                Role = Enum.Parse<UserRoles>(doc.RootElement.GetProperty(nameof(AddSupportTicketDTO.Role)).GetString()),
                Priority = Enum.Parse<SupportTicketPriority>
                    (doc.RootElement.GetProperty(nameof(AddSupportTicketDTO.Priority)).GetString())
            };
        }

        async Task ISupportService.SendSupportTicket(AddSupportTicketDTO addSupportTicketDTO)
        {
            if (configuration["Support:Notifications:Google:Mail"] is null ||
                configuration["Support:Notifications:Google:Password"] is null)
                throw new Exception("Support not avaible in current time");
            var mail = configuration["Support:Notifications:Google:Mail"];
            var password = configuration["Support:Notifications:Google:Password"];
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", mail));
            emailMessage.To.Add(new MailboxAddress("", addSupportTicketDTO.AdministratorEmail));

            string mainTitle = string.Empty;

            switch (addSupportTicketDTO.Priority)
            {
                case SupportTicketPriority.Low:
                    mainTitle = "<h2 style=\"color: blue;\">Low Attention</h2>";
                    break;
                case SupportTicketPriority.Average:
                    mainTitle = "<h2 style=\"color: yellow;\">Average Attention</h2>";
                    break;
                case SupportTicketPriority.High:
                    mainTitle = "<h2 style=\"color: red;\">High Attention</h2>";
                    break;
                default:
                    break;
            }

            emailMessage.Body = new TextPart("html")
            {
                Text = $"""
                    {mainTitle}
                    <hr />
                    <div class="container">
                        <div class="row">
                            <div class="col">
                                <label>{addSupportTicketDTO.FirstName}  {addSupportTicketDTO.LastName}</label>
                            </div>
                        <div class="row">
                            <div class="col">
                                <label>User role: {addSupportTicketDTO.Role}</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Create on page: {addSupportTicketDTO.Link}</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>{addSupportTicketDTO.Description}</label>
                            </div>
                        </div>
                    </div>
                """
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(mail, password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
