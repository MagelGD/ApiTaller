using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class PasswordResetToken : GeneralEntity
    {
        public int UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
