using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.Users
{
    public class GetUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
