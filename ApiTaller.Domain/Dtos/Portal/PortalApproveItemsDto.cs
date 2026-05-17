using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Portal
{
    public class PortalApproveItemsDto
    {
        public List<ApprovedPartDto> Parts { get; set; } = new();
        public List<ApprovedServiceDto> Services { get; set; } = new();
    }
}
