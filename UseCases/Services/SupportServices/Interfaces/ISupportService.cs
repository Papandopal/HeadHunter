using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Services.SupportServices.DTOs;

namespace UseCases.Services.SupportServices.Interfaces
{
    public interface ISupportService
    {
        public Task SendSupportTicket(AddSupportTicketDTO addSupportTicketDTO);
        public AddSupportTicketDTO Deserialize(string json);
    }
}
