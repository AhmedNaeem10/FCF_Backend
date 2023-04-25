using FCF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCF.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(User user);
    }
}
