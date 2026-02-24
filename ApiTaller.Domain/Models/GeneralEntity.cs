using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class GeneralEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ResponsibleUserId { get; set; }
        public virtual User ResponsibleUserIdNavigation { get; set; }
    }
}
