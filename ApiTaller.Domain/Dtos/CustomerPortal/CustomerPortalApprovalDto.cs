namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalApprovalDto
    {
        public int ItemId { get; set; }
        public string ItemType { get; set; } = null!; // "Part" o "Service"
        public bool IsApproved { get; set; }
    }
}
