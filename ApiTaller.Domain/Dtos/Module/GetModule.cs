using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.Module
{
    public class GetModule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
