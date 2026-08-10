using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalHistoryDto
    {
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        // Solo se expone el estado — no el nombre interno de quien lo cambió
    }
}
