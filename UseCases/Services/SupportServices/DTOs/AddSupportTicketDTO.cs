using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace UseCases.Services.SupportServices.DTOs
{
    public class AddSupportTicketDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public UserRoles Role { get; set; }
        public string Link { get; set; } 
        public SupportTicketPriority Priority { get; set; }
        public string AdministratorEmail { get; set; } 
        public string Description { get; set; } 
    }
}
