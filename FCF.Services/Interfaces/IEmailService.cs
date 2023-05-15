using FCF.Models.Requests.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCF.Services.Interfaces
{
    public interface IEmailService
    {
        public void sendEmail(UserDto email, string subject, string body);

        public void dispose();

    }
}
